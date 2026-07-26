import collections
import collections.abc
collections.MutableMapping = collections.abc.MutableMapping

import time
import threading
from dronekit import connect, VehicleMode

# --- GÜVENLİK AYARLARI ---
ZAMAN_ASIMI_SN = 2.0  # 2 saniye içinde komut gelmezse tetiklenir
GAZ_CH = '2'
YON_CH = '4'
DUR_PWM = 1500
YON_DUZ_PWM = 1500
TEST_HIZ_PWM = 1650 # Test için gaz (İleri) PWM değeri

class BalkarWatchdog:
    def __init__(self, port='/dev/ttyACM0', baud=115200):
        self.son_sinyal_zamani = time.time()
        self.sistem_aktif = True
        self.guvende = True
        
        print(">>> Pixhawk'a bağlanılıyor (Watchdog)...")
        self.vehicle = connect(port, wait_ready=False, baud=baud)
        print("✅ Pixhawk Bağlantısı Başarılı! Güvenlik modülü devrede.")

        # Arm olabilmek için güvenlik parametrelerini kod üzerinden zorla kapatıyoruz
        self.vehicle.parameters['BRD_SAFETYENABLE'] = 0
        self.vehicle.parameters['ARMING_CHECK'] = 0

    def sinyal_alindi(self):
        """C# yer istasyonundan komut geldiğini simüle eder."""
        self.son_sinyal_zamani = time.time()
        if not self.guvende:
            print("✅ Bağlantı geri geldi! Araç tekrar kontrolde.")
            self.guvende = True

    def acil_fren(self):
        """Sinyal koptuğunda aracı çiviler."""
        print("\n🛑 WATCHDOG TETİKLENDİ: Sinyal Kaybı! Araç çivileniyor...")
        # 1. Motorlara acil DUR (1500 PWM) emri
        self.vehicle.channels.overrides = {GAZ_CH: DUR_PWM, YON_CH: YON_DUZ_PWM}
        time.sleep(0.5)
        # 2. Sistemi zorla DISARM et
        self.vehicle.armed = False
        # 3. Kumanda kanal kontrollerini serbest bırak
        self.vehicle.channels.overrides = {}

    def _bekci_dongusu(self):
        """Arka planda zamanı sayan acımasız kronometre."""
        print("👁️ Watchdog gözü açık. Bağlantı izleniyor...")
        while self.sistem_aktif:
            gecen_sure = time.time() - self.son_sinyal_zamani
            
            if gecen_sure > ZAMAN_ASIMI_SN and self.guvende:
                self.guvende = False
                self.acil_fren()
                
            time.sleep(0.1)

    def baslat(self):
        self.son_sinyal_zamani = time.time() 
        self.thread = threading.Thread(target=self._bekci_dongusu, daemon=True)
        self.thread.start()

    def kapat(self):
        self.sistem_aktif = False
        self.acil_fren()
        self.vehicle.close()
        print("🔌 Güvenlik modülü ve bağlantı kapatıldı.")

# --- CANLI FİZİKSEL TEST SENARYOSU ---
if __name__ == "__main__":
    try:
        arac_guvenlik = BalkarWatchdog()
        
        print("\n[HAZIRLIK] Araç MANUAL moda alınıp ARM ediliyor...")
        arac_guvenlik.vehicle.mode = VehicleMode("MANUAL")
        time.sleep(1)
        
        arac_guvenlik.vehicle.armed = True
        limit = 0
        while not arac_guvenlik.vehicle.armed:
            print("ARM bekleniyor...")
            time.sleep(1)
            limit += 1
            if limit > 5:
                print("⚠️ Zorla devam ediliyor...")
                break
                
        print("✅ ARMED! Tekerlekler dönmeye başlıyor...")
        # Araca doğrudan 1650 PWM ileri gaz veriyoruz!
        arac_guvenlik.vehicle.channels.overrides = {GAZ_CH: TEST_HIZ_PWM, YON_CH: YON_DUZ_PWM}
        
        # Watchdog'u (Güvenliği) başlat
        arac_guvenlik.baslat()
        
        print("\n[TEST] Sistem devrede! Gaz basılı (1650 PWM). C# sinyali taklit ediliyor.")
        for i in range(5):
            print(f"[{i+1}/5] Sinyal var. Motorlar DÖNÜYOR...")
            arac_guvenlik.sinyal_alindi()
            time.sleep(1)
            
        print("\n🚨 [KOPMA ANI] Eyvah! Wi-Fi koptu! Sinyal kesiliyor...")
        print("👀 Tekerleklere iyi bak, 2 saniye içinde kendi kendini durdurup DISARM olacak!")
        
        # Burada bilerek hiçbir sinyal göndermeyip 3 saniye uyutuyoruz. 
        # 2. Saniyede watchdog arka planda uyanıp freni çekecek!
        time.sleep(3) 
        
    except KeyboardInterrupt:
        print("\n⚠️ Test manuel olarak iptal edildi!")
    finally:
        arac_guvenlik.kapat()