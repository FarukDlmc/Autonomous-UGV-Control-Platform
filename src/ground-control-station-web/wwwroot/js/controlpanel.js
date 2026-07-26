'use strict';

/* ── State ── */
let currentMode = 'autonomous';
let isConnected = false;
let pendingAction = null;
let uptimeSeconds = 0;
let uptimeInterval = null;
let logLines = ['Awaiting vehicle link...'];

/* ── DOM refs ── */
const speedSlider = document.getElementById('speedSlider');
const speedDisplay = document.getElementById('speedDisplay');
const speedWarning = document.getElementById('speedWarning');
const speedWarnText = document.getElementById('speedWarnText');
const eventLog = document.getElementById('eventLog');

/* ════════ YENİ EKLENEN: BACKEND (C#) HABERLEŞME FONKSİYONLARI ════════ */
function getVehicleIp() {
    return document.getElementById("vehicleIp").value || "10.140.82.176";
}

function sendControlCommand(type, detail) {
    // C# tarafındaki /Home/SendSystemCommand adresine fırlat
    fetch('/Home/SendSystemCommand', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            CommandType: type,
            CommandDetail: detail,
            TargetIp: getVehicleIp()
        })
    })
        .then(res => res.json())
        .then(data => {
            // Eğer C# tarafı "gönderemedim" derse arayüzde senin Toast mesajınla uyar
            if (!data.success) showToast("Araç Bağlantı Hatası: " + data.message, 'danger');
        })
        .catch(err => console.error("Komut gönderilirken ağ hatası:", err));
}
/* ═════════════════════════════════════════════════════════════════════ */

/* ─────────────────────────────────────────────
    SPEED SLIDER
───────────────────────────────────────────── */
function updateSpeed(val) {
    val = parseInt(val);
    speedDisplay.textContent = val + '%';

    /* gradient progress fill */
    speedSlider.style.setProperty('--progress', val + '%');

    if (val >= 80) {
        speedWarning.classList.add('danger');
        speedWarnText.textContent = '⚠ HIGH SPEED — REDUCE IN CONFINED AREAS';
    } else if (val >= 60) {
        speedWarning.classList.add('danger');
        speedWarning.style.color = 'var(--yellow)';
        speedWarnText.textContent = '⚡ ELEVATED SPEED — CAUTION';
    } else {
        speedWarning.classList.remove('danger');
        speedWarning.style.color = '';
        speedWarnText.textContent = '◈ NOMINAL SPEED RANGE';
    }

    // 🔥 JİLET HAMLE: Ekran güncellendiği an C#'a (ve dolayısıyla Pi 5'e) emri yolla
    sendControlCommand("SETTING", "SPEED_" + val);
}

// Sayfa ilk yüklendiğinde slider'ı 50'ye çeker (Pi 5'e de SPEED_50 gönderir)
updateSpeed(50);

/* ─────────────────────────────────────────────
    MODE CONTROL
───────────────────────────────────────────── */
function setMode(mode) {
    currentMode = mode;
    const btnA = document.getElementById('btnAutonomous');
    const btnM = document.getElementById('btnManual');
    const badA = document.getElementById('badgeAutonomous');
    const badM = document.getElementById('badgeManual');

    if (mode === 'autonomous') {
        btnA.className = 'mode-btn autonomous';
        btnM.className = 'mode-btn manual';
        badA.textContent = '◆ ACTIVE';
        badA.className = 'mode-badge active-badge';
        badM.textContent = '◇ STANDBY';
        badM.className = 'mode-badge inactive-badge';
        showToast('MODE → AUTONOMOUS', 'info');
        addLogLine('Mode switched to AUTONOMOUS');
    } else {
        btnA.className = 'mode-btn autonomous inactive';
        btnM.className = 'mode-btn manual active';
        badM.textContent = '◆ ACTIVE';
        badM.className = 'mode-badge active-badge';
        badA.textContent = '◇ STANDBY';
        badA.className = 'mode-badge inactive-badge';
        showToast('MODE → MANUAL OVERRIDE', 'warn');
        addLogLine('Mode switched to MANUAL OVERRIDE');
    }

    // 🔥 JİLET HAMLE: Mod değiştiği an C#'a yolla!
    sendControlCommand("MODE", mode.toUpperCase());
}

/* ─────────────────────────────────────────────
    SYSTEM TOGGLES
───────────────────────────────────────────── */
function handleToggle(id, state) {
    const row = document.getElementById('row-' + id);
    const stat = document.getElementById('status-' + id);

    if (state) {
        row.classList.add('is-on');
        stat.textContent = 'ACTIVE';
        showToast(id.toUpperCase() + ' — ACTIVATED', 'info');
        addLogLine(id.toUpperCase() + ' subsystem activated');
    } else {
        row.classList.remove('is-on');
        stat.textContent = 'OFFLINE';
        showToast(id.toUpperCase() + ' — DEACTIVATED', 'warn');
        addLogLine(id.toUpperCase() + ' subsystem deactivated');
    }

    // 🔥 JİLET HAMLE: Anahtar açılıp kapandığında C#'a yolla (Örn: HEADLIGHTS_ON)
    sendControlCommand("TOGGLE", id.toUpperCase() + (state ? "_ON" : "_OFF"));
}

/* ─────────────────────────────────────────────
    CRITICAL CONTROLS
───────────────────────────────────────────── */
function handleKillSwitch() {
    // Custom Modal yerine, %100 güvenilir Tarayıcı Confirm'i kullanıyoruz
    if (confirm("🛑 DİKKAT: MOTORLAR ANINDA KİLİTLENECEK VE GÜÇ KESİLECEK! DEVAM EDİLSİN Mİ?")) {
        showToast('⚠ KILL SWITCH ENGAGED — ALL SYSTEMS HALTED', 'danger');

        // Buton efektini oynat
        document.getElementById('btnKill').style.background = '#660011';
        document.getElementById('btnKill').style.animationPlayState = 'paused';
        setTimeout(() => {
            document.getElementById('btnKill').style.background = '';
            document.getElementById('btnKill').style.animationPlayState = '';
        }, 2000);

        // 🔥 C#'a ve Pi 5'e Nükleer Emri Fırlat!
        sendControlCommand("CRITICAL", "KILL_SWITCH");
    }
}

function handleReboot() {
    if (confirm("↺ Pi 5 Yeniden Başlatılacak. Bağlantı tamamen kopacak. Onaylıyor musun?")) {
        showToast('REBOOT SIGNAL SENT TO PI 5', 'warn');

        // 🔥 C#'a Reboot Emrini Fırlat!
        sendControlCommand("CRITICAL", "REBOOT");
    }
}

function handleReturnHome() {
    showToast('RETURN TO HOME WAYPOINT SET', 'info');
    addLogLine('RTH mission initiated');

    // Gerekirse backend'e dönüş komutu yolla
    sendControlCommand("MODE", "RETURN_TO_HOME");
}

/* ─────────────────────────────────────────────
    CONNECT
───────────────────────────────────────────── */
function handleConnect() {
    // Port seçme zorunluluğunu kaldırdık, anında bağlanır!
    const ip = document.getElementById('vehicleIp').value.trim() || '192.168.1.100';
    const btn = document.getElementById('connectBtn');

    // Eğer zaten bağlıysa bağlantıyı kes (Disconnect)
    if (isConnected) {
        isConnected = false;
        btn.textContent = 'CONNECT';
        btn.classList.remove('connected');
        document.getElementById('connStatusDot').className = 'status-dot off';
        document.getElementById('connStatusText').textContent = 'DISCONNECTED';
        stopUptime();

        // 🔥 JİLET HAMLE: Bağlantı Kesildiğini Veritabanına Yolla
        sendControlCommand("CONNECTION", "DISCONNECT");
        showToast('VEHICLE LINK TERMINATED', 'warn');
        return;
    }

    // Bağlanıyor...
    btn.textContent = 'CONNECTING...';
    setTimeout(() => {
        isConnected = true;
        btn.textContent = 'DISCONNECT';
        btn.classList.add('connected');
        document.getElementById('connStatusDot').className = 'status-dot on';
        document.getElementById('connStatusText').textContent = ip;
        startUptime();
        simulateTelemetry();

        // 🔥 JİLET HAMLE: Bağlanıldığını Veritabanına Yolla
        sendControlCommand("CONNECTION", "LINK_ESTABLISHED");
        showToast('✓ VEHICLE LINK ESTABLISHED — ' + ip, 'info');
    }, 800); // 1.2 saniyeden 0.8 saniyeye düşürdük, daha seri!
}

/* ─────────────────────────────────────────────
    UPTIME CLOCK & FAKE TELEMETRY
───────────────────────────────────────────── */
function startUptime() {
    uptimeSeconds = 0;
    uptimeInterval = setInterval(() => {
        uptimeSeconds++;
        const h = String(Math.floor(uptimeSeconds / 3600)).padStart(2, '0');
        const m = String(Math.floor((uptimeSeconds % 3600) / 60)).padStart(2, '0');
        const s = String(uptimeSeconds % 60).padStart(2, '0');
        document.getElementById('telemUptime').textContent = h + ':' + m + ':' + s;
    }, 1000);
}

function stopUptime() {
    clearInterval(uptimeInterval);
    document.getElementById('telemUptime').textContent = '00:00:00';
}

function simulateTelemetry() {
    if (!isConnected) return;
    const rssi = (-50 - Math.floor(Math.random() * 30));
    const batt = (85 + Math.floor(Math.random() * 12));
    const temp = (42 + Math.floor(Math.random() * 15));
    document.getElementById('telemSignal').textContent = rssi + ' dBm';
    document.getElementById('telemBatt').textContent = batt + '%';
    document.getElementById('telemTemp').textContent = temp + '°C';
    setTimeout(simulateTelemetry, 4000);
}

/* ─────────────────────────────────────────────
    MODAL & TOAST UI HELPERS
───────────────────────────────────────────── */
function openModal(icon, title, body, onConfirm) {
    document.getElementById('modalIcon').textContent = icon;
    document.getElementById('modalTitle').textContent = title;
    document.getElementById('modalBody').textContent = body;
    document.getElementById('modalOverlay').classList.add('show');
    pendingAction = onConfirm;
}

function closeModal() {
    document.getElementById('modalOverlay').classList.remove('show');
    pendingAction = null;
}

function executeConfirmed() {
    closeModal();
    if (typeof pendingAction === 'function') pendingAction();
}

document.getElementById('modalOverlay').addEventListener('click', function (e) {
    if (e.target === this) closeModal();
});

function showToast(message, type = 'info') {
    const container = document.getElementById('toast-container');
    const toast = document.createElement('div');
    toast.className = 'toast' + (type === 'warn' ? ' warn' : type === 'danger' ? ' danger' : '');
    toast.textContent = '> ' + message;
    container.appendChild(toast);
    setTimeout(() => {
        toast.style.opacity = '0';
        toast.style.transition = 'opacity .3s';
        setTimeout(() => toast.remove(), 300);
    }, 3500);
}

function addLogLine(msg) {
    const now = new Date();
    const ts = String(now.getHours()).padStart(2, '0') + ':' +
        String(now.getMinutes()).padStart(2, '0') + ':' +
        String(now.getSeconds()).padStart(2, '0');
    logLines.push(ts + '  ' + msg);
    if (logLines.length > 5) logLines.shift();
    eventLog.innerHTML = logLines.map(l => {
        const parts = l.split('  ');
        return '<span style="color:#2a5a6a">[' + parts[0] + ']</span>  ' + parts.slice(1).join('  ') + '<br>';
    }).join('');
}