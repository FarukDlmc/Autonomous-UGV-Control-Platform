// ============================================================
// ai-vision.js — GCS AI Vision Logic
// ============================================================

(function () {
    'use strict';

    // ── State ──────────────────────────────────────────────
    let isStreaming = false;
    let bboxEnabled = true;
    let logCount = 0;

    // Pi 5'in Canlı Yayın IP Adresi (Değişmediğinden emin ol!)
    const PI_STREAM_URL = "http://10.140.82.176:5000/video_feed";

    // ── DOM Refs ───────────────────────────────────────────
    const streamBtn = document.getElementById('streamBtn');
    const bboxBtn = document.getElementById('bboxBtn');
    const logBox = document.getElementById('logBox');
    const logCountEl = document.getElementById('logCount');
    const fpsCounter = document.getElementById('fpsCounter');
    const fpsOverlay = document.getElementById('fpsOverlay');
    const feedPlaceholder = document.getElementById('feedPlaceholder');
    const streamImg = document.getElementById('aiStreamFeed');

    // ── Helpers ────────────────────────────────────────────
    function timestamp() {
        const now = new Date();
        return [
            String(now.getHours()).padStart(2, '0'),
            String(now.getMinutes()).padStart(2, '0'),
            String(now.getSeconds()).padStart(2, '0')
        ].join(':');
    }

    window.addLogEntry = function (label, confidence) {
        logCount++;
        const entry = document.createElement('div');
        entry.className = 'log-entry';

        const confText = typeof confidence === 'number' ? `${confidence}%` : confidence;

        entry.innerHTML =
            `<span class="log-ts">${timestamp()}</span>` +
            `${label} &mdash; ${confText}`;

        logBox.appendChild(entry);
        logBox.scrollTop = logBox.scrollHeight;

        while (logBox.children.length > 200) {
            logBox.removeChild(logBox.firstChild);
        }

        logCountEl.textContent = logCount;
    };

    // 🔴 KRİTİK GÜVENLİK SİGORTASI: Resim Yüklenemezse Haber Ver!
    streamImg.onerror = function () {
        if (isStreaming) {
            addLogEntry('System Error', 'BAĞLANTI KOPTU VEYA IP YANLIŞ');
            console.error("Görüntü çekilemedi. Lütfen Pi 5'in IP adresini ve kodun çalışıp çalışmadığını kontrol et.");
        }
    };

    // ── Stream Toggle ──────────────────────────────────────
    window.toggleStream = function () {
        isStreaming = !isStreaming;

        if (isStreaming) {
            streamBtn.classList.add('streaming');
            streamBtn.innerHTML = '<span class="btn-icon">◼</span> STOP STREAM';
            streamBtn.style.color = "#ff4444";
            streamBtn.style.borderColor = "#ff4444";

            feedPlaceholder.style.display = 'none';
            fpsOverlay.textContent = 'FPS: 30';
            fpsCounter.textContent = '30';

            // Cache'i kırarak tertemiz bir istek atıyoruz
            const timestamp = new Date().getTime();
            streamImg.src = `${PI_STREAM_URL}?t=${timestamp}`;
            streamImg.style.display = 'block';

            addLogEntry('System', 'Stream Started');
        } else {
            streamBtn.classList.remove('streaming');
            streamBtn.innerHTML = '<span class="btn-icon">▶</span> START STREAM';
            streamBtn.style.color = "";
            streamBtn.style.borderColor = "";

            feedPlaceholder.style.display = 'flex';
            fpsOverlay.textContent = 'FPS: --';
            fpsCounter.textContent = '--';

            streamImg.src = "";
            streamImg.style.display = 'none';

            addLogEntry('System', 'Stream Stopped');
        }
    };

    // ── Capture (Ekran Görüntüsü Al ve İndir) ─────────────────
    window.captureFrame = function () {
        if (!isStreaming) {
            addLogEntry('System Error', 'ÖNCE YAYINI BAŞLATIN');
            return;
        }

        addLogEntry('System', 'CAPTURE CMD SENT');
        const btn = document.querySelector('.btn-capture');
        btn.style.background = 'rgba(0,200,255,.18)';
        setTimeout(() => { btn.style.background = 'transparent'; }, 180);

        // Arka planda Pi 5'in Capture URL'sini tetikle, fotoğraf laptoba insin!
        const a = document.createElement('a');
        a.href = 'http://10.140.82.176:5000/capture';
        a.target = '_blank';
        a.click();
    };

    // ── BBox Toggle (YOLO Kutularını Kapat/Aç) ────────────────
    window.toggleBbox = function () {
        if (!isStreaming) return;

        // Pi 5'e "Kutuları Değiştir" komutunu (POST) gönderiyoruz
        fetch('http://10.140.82.176:5000/toggle_bbox', { method: 'POST' })
            .then(response => response.json())
            .then(data => {
                bboxEnabled = data.bbox_aktif;
                bboxBtn.classList.toggle('active', bboxEnabled);

                // Sağdaki fiyakalı terminale log düşür
                addLogEntry('System', 'BBOX ' + (bboxEnabled ? 'ENABLED' : 'DISABLED'));
            })
            .catch(error => {
                console.error("BBOX komutu iletilemedi:", error);
            });
    };

    // ── Init ───────────────────────────────────────────────
    (function initLog() {
        logBox.innerHTML = '';
        addLogEntry('System Ready ⚡', '—');
    })();

    // ── SignalR AI Detection Alıcısı ────────────────────────
    // Eğer sayfada signalR connection tanımlıysa (telemetriden kalma connection objen):
    if (typeof connection !== 'undefined') {
        connection.on("YeniAiTespiti", function (nesneAdi, dogrulukOrani) {
            // Sağdaki fiyakalı panele otomatik satır ekler
            window.addLogEntry(nesneAdi, dogrulukOrani);
        });
    } else {
        console.warn("SignalR bağlantı objesi (connection) bulunamadı! Loglar canlı güncellenemeyecek.");
    }

})();