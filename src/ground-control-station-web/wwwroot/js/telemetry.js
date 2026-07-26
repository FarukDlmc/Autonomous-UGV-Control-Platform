// ============================================================
// telemetry.js — GCS Telemetry Data Binder (SignalR)
// ============================================================

(function () {
    'use strict';

    // 1. SignalR Bağlantısını Başlat
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/telemetryHub") 
        .withAutomaticReconnect()
        .build();

    // 2. C#'tan Gelen Veriyi Yakala ve Arayüze Bas
    connection.on("ReceiveTelemetry", function (data) {

        // --- SÜTUN 1: VERİ KARTLARI ---

        // GPS
        document.getElementById("gpsLat").innerHTML = data.lat.toFixed(5) + "&deg;";
        document.getElementById("gpsLon").innerHTML = data.lon.toFixed(5) + "&deg;";

        // Altitude (Yükseklik)
        document.getElementById("altValue").innerText = data.alt.toFixed(1) + " m";

        // Satellites (Uydular) ve Ping
        document.getElementById("satActive").innerText = data.satellites;
        // Ping değerini arayüzdeki "Signal: Excellent" kısmına Ping verisi olarak yazdırıyoruz
        document.getElementById("satSignal").innerText = data.ping + " ms";


        // --- SÜTUN 2: MOTOR & GAZ ÇUBUKLARI ---

        // Senin arayüzünde Sol Motor (Motor A) ve Sağ Motor (Motor B) var. 
        // Python'dan gelen Throttle (Gaz) ve Steering (Yön) verisini PWM'e (1000 - 2000 arasına) çevirelim.
        // Formül: Temel PWM 1500 (Durma). Gaz (0-100) bunu 2000'e taşır. Steering sağ/sol farkını yaratır.

        let basePwm = 1500 + (data.throttle * 5); // %100 gaz = 2000 PWM
        let pwmA = Math.round(basePwm + (data.steering * 5)); // Sol Motor
        let pwmB = Math.round(basePwm - (data.steering * 5)); // Sağ Motor

        // Sınırlandırma (1000 ile 2000 arası)
        pwmA = Math.min(Math.max(pwmA, 1000), 2000);
        pwmB = Math.min(Math.max(pwmB, 1000), 2000);

        // Çubuk yükseklik yüzdesi (1000=0%, 2000=100%)
        let fillAPercent = ((pwmA - 1000) / 1000) * 100;
        let fillBPercent = ((pwmB - 1000) / 1000) * 100;

        document.getElementById("pwmA").innerText = pwmA;
        document.getElementById("fillA").style.height = fillAPercent + "%";

        document.getElementById("pwmB").innerText = pwmB;
        document.getElementById("fillB").style.height = fillBPercent + "%";


        // --- SÜTUN 3: ATTITUDE (DENGE VE GÖRÜŞ) ---

        // Compass (Pusula)
        document.getElementById("compassNeedle").style.transform = `rotate(${data.yaw}deg)`;

        // Pitch (Eğim) - Arayüzde -50 ile +50 derece arası bir skala varsayıyoruz
        document.getElementById("pitchDeg").innerHTML = data.pitch.toFixed(1) + "&deg;";
        let pitchPercent = Math.min(Math.max(50 + data.pitch, 0), 100);
        document.getElementById("pitchFill").style.width = pitchPercent + "%";

        // Roll (Yatma)
        document.getElementById("rollDeg").innerHTML = data.roll.toFixed(1) + "&deg;";
        let rollPercent = Math.min(Math.max(50 + data.roll, 0), 100);
        document.getElementById("rollFill").style.width = rollPercent + "%";

        // AI Vision Confidence (Yapay Zeka Doğruluk)
        document.getElementById("tfScore").innerText = data.aiConf + "%";
        document.getElementById("tfFill").style.width = data.aiConf + "%";
    });

    // 3. Bağlantıyı Başlat
    connection.start().then(function () {
        console.log("Telemetry SignalR Bağlantısı Başarılı!");
    }).catch(function (err) {
        return console.error("SignalR Hatası: " + err.toString());
    });

    // 4. Alt Bilgi (Footer) Saat Güncellemesi
    setInterval(() => {
        const now = new Date();
        document.getElementById('sysTime').innerText = now.toLocaleTimeString('tr-TR', { hour12: false });
    }, 1000);

    // --- KLAVYE İLE SÜRÜŞ KONTROLÜ (WASD) ---
    let activeKeys = {};

    window.addEventListener('keydown', function (e) {
        let key = e.key.toUpperCase();
        if (['W', 'A', 'S', 'D'].includes(key) && !activeKeys[key]) {
            activeKeys[key] = true;
            sendDriveCommand(key);
        }
    });

    window.addEventListener('keyup', function (e) {
        let key = e.key.toUpperCase();
        if (['W', 'A', 'S', 'D'].includes(key)) {
            activeKeys[key] = false;
            // Eğer hiçbir tuşa basılmıyorsa aracı DURDUR
            if (!activeKeys['W'] && !activeKeys['A'] && !activeKeys['S'] && !activeKeys['D']) {
                sendDriveCommand("STOP");
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

})();