/**
 * system-logs.js  —  BALKAR GCS  |  System Logs
 * Filtering (dropdown + search), helpers (export, clear, uptime) and LIVE SIGNALR INTEGRATION.
 */

(function () {
    'use strict';

    /* ── Element refs ─────────────────────────── */
    const searchInput = document.getElementById('logSearch');
    const levelFilter = document.getElementById('levelFilter');
    const logBody = document.getElementById('logBody');
    const exportBtn = document.getElementById('exportCsv');
    const clearBtn = document.getElementById('clearLogs');
    const uptimeEl = document.getElementById('uptimeDisplay');
    const sessionStartEl = document.getElementById('sessionStartText');

    /* ── Filter logic ─────────────────────────── */
    function applyFilters() {
        const query = searchInput.value.trim().toLowerCase();
        const level = levelFilter.value;
        const rows = logBody.querySelectorAll('.log-row');

        rows.forEach(function (row) {
            const rowLevel = (row.dataset.level || '').toUpperCase();
            const rowText = row.textContent.toLowerCase();

            const levelMatch = (level === 'ALL') || (rowLevel === level);
            const searchMatch = !query || rowText.includes(query);

            if (levelMatch && searchMatch) {
                row.style.display = 'flex'; 
            } else {
                row.style.display = 'none';
            }
        });
    }

    searchInput.addEventListener('input', applyFilters);
    levelFilter.addEventListener('change', applyFilters);

    /* ── Update Sidebar Stats ─────────────────── */
    function updateSidebarStats() {
        const rows = logBody.querySelectorAll('.log-row');
        let critCount = 0;
        let warnCount = 0;
        let lastCrit = "No critical issues.";
        let lastWarn = "System stable.";

        // En üstte en yeni loglar olduğu için, ilk bulduğu critical/warning en son olandır
        rows.forEach(function (row) {
            const level = row.dataset.level;
            const msg = row.querySelector('.col-msg').textContent;
            const module = row.querySelector('.col-module').textContent;

            if (level === 'CRITICAL') {
                critCount++;
                if (critCount === 1) lastCrit = module + " — " + msg.substring(0, 20) + "...";
            }
            if (level === 'WARNING' || level === 'ERROR') {
                warnCount++;
                if (warnCount === 1) lastWarn = module + " — " + msg.substring(0, 20) + "...";
            }
        });

        document.getElementById('criticalCount').textContent = critCount;
        document.getElementById('lastCriticalMsg').textContent = lastCrit;

        document.getElementById('warningCount').textContent = warnCount;
        document.getElementById('lastWarningMsg').textContent = lastWarn;
    }

    /* ── Export CSV ───────────────────────────── */
    exportBtn.addEventListener('click', function () {
        // Gizli olmayan (görünen) logları al
        const rows = Array.from(logBody.querySelectorAll('.log-row')).filter(r => r.style.display !== 'none');
        const lines = ['TIME,MODULE,LEVEL,MESSAGE'];

        rows.forEach(function (row) {
            const cols = row.querySelectorAll('.col-time, .col-module, .col-level, .col-msg');
            if (cols.length === 4) {
                const escaped = Array.from(cols).map(function (c) {
                    return '"' + c.textContent.replace(/"/g, '""') + '"';
                });
                lines.push(escaped.join(','));
            }
        });

        const blob = new Blob([lines.join('\n')], { type: 'text/csv' });
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = 'BALKAR_System_Logs_' + Date.now() + '.csv';
        a.click();
        URL.revokeObjectURL(url);
    });

    /* ── Clear logs ───────────────────────────── */
    clearBtn.addEventListener('click', function () {
        if (!confirm('Tüm log ekranı temizlensin mi? (Veritabanından silinmez)')) return;
        logBody.innerHTML = '';
        updateSidebarStats();
    });

    let activeKeys = {};

    window.addEventListener('keydown', function (e) {
        let key = e.key.toUpperCase();
        // Yeni tuşları (Q ve R) listeye dahil ettik
        if (['W', 'A', 'S', 'D', 'E', 'Q', 'R'].includes(key) && !activeKeys[key]) {
            activeKeys[key] = true;
            sendDriveCommand(key);
        }
    });

    window.addEventListener('keyup', function (e) {
        let key = e.key.toUpperCase();
        if (['W', 'A', 'S', 'D', 'E', 'Q', 'R'].includes(key)) {
            activeKeys[key] = false;

            // Güvenlik: Sadece sürüş tuşlarından (WASD) el çekildiğinde STOP gönder
            if (['W', 'A', 'S', 'D'].includes(key)) {
                if (!activeKeys['W'] && !activeKeys['A'] && !activeKeys['S'] && !activeKeys['D']) {
                    sendDriveCommand("STOP");
                }
            }
        }
    });

    function sendDriveCommand(cmd) {
        fetch('/Home/SendDriveCommand', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ Command: cmd })
        }).catch(err => console.error("Komut gönderilemedi:", err));
    }

    /* ── Uptime counter ───────────────────────── */
    if (uptimeEl) {
        var startTime = Date.now();
        var d = new Date();
        sessionStartEl.textContent = "Session started " + d.getHours().toString().padStart(2, '0') + ":" + d.getMinutes().toString().padStart(2, '0') + ":" + d.getSeconds().toString().padStart(2, '0');

        function pad(n) { return String(n).padStart(2, '0'); }

        function tick() {
            var elapsed = Math.floor((Date.now() - startTime) / 1000);
            var h = Math.floor(elapsed / 3600);
            var m = Math.floor((elapsed % 3600) / 60);
            var s = elapsed % 60;
            uptimeEl.textContent = pad(h) + ':' + pad(m) + ':' + pad(s);
        }
        tick();
        setInterval(tick, 1000);
    }

    /* ════════════════════════════════════════════════════
       SIGNALR LIVE DATA BINDING (CANLI VERİ AKIŞI)
    ════════════════════════════════════════════════════ */
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/telemetryHub") // C# Program.cs de belirlediğin isim
        .withAutomaticReconnect()
        .build();

    connection.on("ReceiveSystemLog", function (log) {

        // Zaman formatı (HH:mm:ss.fff)
        const dateObj = new Date(log.tarihSaat);
        let timeString = "";
        try {
            timeString = dateObj.toISOString().split('T')[1].replace('Z', '');
        } catch (e) {
            timeString = log.tarihSaat;
        }

        // CSS Class belirleme
        const level = (log.logLevel || 'INFO').toUpperCase();
        let levelClass = 'level-info';
        let rowModifier = '';

        if (level === 'WARNING') levelClass = 'level-warning';
        else if (level === 'ERROR') levelClass = 'level-error';
        else if (level === 'CRITICAL') {
            levelClass = 'level-critical';
            rowModifier = 'log-row--critical';
        }

        // HTML elementini yarat (Senin CSS yapına %100 uygun)
        const newRow = document.createElement("div");
        newRow.className = `log-row ${rowModifier}`;
        newRow.dataset.level = level;

        newRow.innerHTML = `
            <span class="col-time">${timeString}</span>
            <span class="col-module">${log.module}</span>
            <span class="col-level ${levelClass}">${level}</span>
            <span class="col-msg">${log.message}</span>
        `;

        // Tablonun en üstüne ekle (Prepend)
        logBody.prepend(newRow);

        // Maksimum 100 log tut (Sayfa şişmesin diye en eskisini siler)
        if (logBody.children.length > 100) {
            logBody.removeChild(logBody.lastElementChild);
        }

        // Yeni log eklendiğinde filtreleri ve yan menüyü güncelle
        applyFilters();
        updateSidebarStats();
    });

    connection.start().then(function () {
        console.log("✅ BALKAR GCS: System Logs - SignalR Bağlantısı Başarılı!");
    }).catch(function (err) {
        console.error("❌ SignalR Hatası: " + err.toString());
    });

    document.addEventListener("DOMContentLoaded", function () {
        updateSidebarStats();
    });

})();