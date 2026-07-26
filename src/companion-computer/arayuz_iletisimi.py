import collections
import collections.abc
collections.MutableMapping = collections.abc.MutableMapping

import time
import threading
import requests
import psutil
from dronekit import connect

class BalkarTelemetri:
    def __init__(self, port='/dev/ttyACM0', baud=115200, csharp_api_url="http://10.140.82.199:5255/api/telemetry/vitals"):
        self.api_url = csharp_api_url
        self.sistem_aktif = True
        
        print(">>> [TELEMETRİ] Pixhawk'a bağlanılıyor...")
        # wait_ready=False ile hızlı bağlanıyoruz
        self.vehicle = connect(port, wait_ready=False, baud=baud)
        print("✅ [TELEMETRİ] Bağlantı başarılı! Veri akışı başlıyor...")

    def get_rpi_temp(self):
        """Raspberry Pi'nin gerçek işlemci sıcaklığını okur"""
        try:
            with open('/sys/class/thermal/thermal_zone0/temp', 'r') as f:
                return float(f.read()) / 1000.0
        except:
            return 45.0 # Okuyamazsa varsayılan değer

    def _veri_basma_dongusu(self):
        """Arka planda saniyede 5 kez (5Hz) C# arayüzüne canlı veri basar"""
        while self.sistem_aktif:
            try:
                # 1. PWM Verisini Çek (Önce bizim yazdığımız otonom komuta bakar, yoksa kumandaya bakar)
                ch2_pwm = self.vehicle.channels.overrides.get('2') or self.vehicle.channels.get('2') or 1500
                ch4_pwm = self.vehicle.channels.overrides.get('4') or self.vehicle.channels.get('4') or 1500
                
                # GPS uyduları tam oturana kadar hız 'None' dönebilir, korumaya alıyoruz
                g_speed = self.vehicle.groundspeed
                guvenli_hiz = round(g_speed, 2) if g_speed is not None else 0.0
                
                # 2. JSON Paketini Hazırla
                payload = {
                    "driveMode": "AUTO" if self.vehicle.mode.name == "AUTO" else "MANUAL",
                    "isArmed": self.vehicle.armed,
                    "pixhawkLink": "ttyACM0",
                    "speedMs": guvenli_hiz,
                    "latencyMs": 15,
                    "obstacleDetected": False,
                    "motorLPwm": ch2_pwm,
                    "motorRPwm": ch2_pwm, 
                    "steerPwm": ch4_pwm,
                    "camTiltPwm": 1500,
                    "rpiCpuTemp": round(self.get_rpi_temp() or 45.0, 1),
                    "systemLoadPct": int(psutil.cpu_percent()),
                    "radarDistances": [200, 200, 200, 200, 200, 200]
                }
                
                # 3. Paketi fırlat! (Üniversite/Telefon Wi-Fi gecikmeleri için sabır süresini 0.5 saniyeye çıkardık)
                requests.post(self.api_url, json=payload, timeout=0.5)
                
            except requests.exceptions.RequestException:
                # Ağ gecikirse terminali kirletme, sessizce bir sonraki pakete geç
                pass
            except Exception as e:
                print(f"Telemetri Hatası: {e}")
                
            time.sleep(0.2)

    def baslat(self):
        """Telemetriyi arka plan iş parçacığında (Thread) başlatır"""
        self.thread = threading.Thread(target=self._veri_basma_dongusu, daemon=True)
        self.thread.start()

    def kapat(self):
        self.sistem_aktif = False
        self.vehicle.close()
        print("🔌 Telemetri yayını durduruldu.")

# --- MODÜLÜ CANLI TEST ETMEK İÇİN ---
if __name__ == "__main__":
    # DİKKAT: Buradaki IP adresini 1. Adımda bulduğun laptobunun Wi-Fi IP adresiyle değiştir!
    HEDEF_CSHARP_IP = "http://10.140.82.199:5255/api/telemetry/vitals" 
    
    telemetri = BalkarTelemetri(csharp_api_url=HEDEF_CSHARP_IP)
    telemetri.baslat()
    
    try:
        print("\n📡 VERİ AKIŞI AKTİF. Çıkmak için CTRL+C'ye basın...")
        print("Laptobundaki C# Dashboard'una bak, RPi5'in sıcaklığı ve değerleri canlı akıyor olmalı!")
        
        # Test için manuel gaz verip değerlerin ekranda oynadığını görelim
        # Uyarı: Araç tekerlekleri havada olsun!
        time.sleep(2)
        telemetri.vehicle.armed = True
        
        while True:
            # PWM değerlerini dalgalandırarak arayüzdeki grafikleri hareket ettiriyoruz
            for pwm in range(1500, 1800, 50):
                telemetri.vehicle.channels.overrides = {'2': pwm, '4': 1500}
                time.sleep(1)
            for pwm in range(1800, 1500, -50):
                telemetri.vehicle.channels.overrides = {'2': pwm, '4': 1500}
                time.sleep(1)

    except KeyboardInterrupt:
        pass
    finally:
        telemetri.vehicle.channels.overrides = {}
        telemetri.vehicle.armed = False
        telemetri.kapat()
