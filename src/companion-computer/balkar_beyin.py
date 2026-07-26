import collections
import collections.abc
collections.MutableMapping = collections.abc.MutableMapping

import time
from dronekit import connect, VehicleMode

# Dışarıdan P-Kontrolcü sınıfımızı içeri alıyoruz!
from balkar_kontrolcu import BalkarPKontrolcu

# --- PWM SABİTLERİ ---
GAZ_CH = '2'
YON_CH = '4'
DUR_PWM = 1500
YON_DUZ_PWM = 1500
OTONOM_HIZ_PWM = 1650 # Araç otonomda kendi kendine giderkenki gaz değeri

class BalkarDurumMakinesi:
    def __init__(self, port='/dev/ttyACM0', baud=115200):
        self.mevcut_durum = "DUR"
        
        # P-Kontrolcü motorumuzu beynin içine monte ediyoruz
        self.direksiyon_kontrolcusu = BalkarPKontrolcu(kp=1.2)
        
        print(">>> BALKAR Ana Beyin Başlatılıyor...")
        self.vehicle = connect(port, wait_ready=False, baud=baud)
        
        self.vehicle.parameters['BRD_SAFETYENABLE'] = 0
        self.vehicle.parameters['ARMING_CHECK'] = 0
        
        self.vehicle.mode = VehicleMode("MANUAL")
        self.vehicle.armed = True
        print("✅ Beyin Devrede! Sistem Durumu: DUR")

    def durum_degistir(self, yeni_durum):
        gecerli_durumlar = ["DUR", "MANUEL", "OTONOM"]
        if yeni_durum in gecerli_durumlar:
            self.mevcut_durum = yeni_durum
            print(f"\n🔄 Vites Değişti! Yeni Mod: {self.mevcut_durum}")
            if self.mevcut_durum == "DUR":
                self.motorlari_durdur()
        else:
            print("⚠️ Geçersiz Mod İsteği!")

    def motorlari_durdur(self):
        self.vehicle.channels.overrides = {GAZ_CH: DUR_PWM, YON_CH: YON_DUZ_PWM}
        print("🛑 Motorlar Kilitlendi.")

    def manuel_surus_isle(self, gaz_pwm, yon_pwm):
        if self.mevcut_durum == "MANUEL":
            self.vehicle.channels.overrides = {GAZ_CH: gaz_pwm, YON_CH: yon_pwm}
            print(f"🕹️ MANUEL: Gaz={gaz_pwm}, Yön={yon_pwm}")
        else:
            pass # Başka moddaysa manuel komutu sessizce yoksay

    def otonom_karar_isle(self, hedef_x):
        """
        Kameradan (örneğin YOLO'dan) gelen X koordinatını alır, 
        matematiksel kontrolcüye sokar ve araca uygular.
        """
        if self.mevcut_durum == "OTONOM":
            # 1. Matematiği çalıştır ve dönmesi gereken PWM'i al
            hesaplanan_yon_pwm = self.direksiyon_kontrolcusu.direksiyon_hesapla(hedef_x)
            
            # 2. Araca fiziksel olarak komutu gönder (Gaz sabit ileri, Yön dinamik)
            self.vehicle.channels.overrides = {GAZ_CH: OTONOM_HIZ_PWM, YON_CH: hesaplanan_yon_pwm}
            
            # Formül mantığı: Merkez PWM + (Kp * Hata)
            print(f"🤖 OTONOM AKTİF | Gelen X: {hedef_x} -> Motorlara İletilen Yön PWM: {hesaplanan_yon_pwm} (Gaz: {OTONOM_HIZ_PWM})")
        else:
            pass

    def kapat(self):
        self.motorlari_durdur()
        self.vehicle.armed = False
        self.vehicle.close()
        print("🔌 Beyin kapatıldı.")

# --- ENTEGRASYON CANLI TEST SENARYOSU ---
if __name__ == "__main__":
    beyin = BalkarDurumMakinesi()
    time.sleep(2)
    
    try:
        beyin.durum_degistir("OTONOM")
        print("\n--- OTONOM SİMÜLASYONU BAŞLIYOR (Kamera Verisi Akıyor) ---")
        
        # Sanki kamera saniyede bir yeni veri gönderiyormuş gibi simüle ediyoruz
        kamera_verileri = [
            320, # Tam merkez
            380, # Hedef sağa kaydı
            450, # Hedef daha sağa kaydı
            320, # Hedef tekrar merkeze geldi
            200, # Hedef sert sola kaydı
            None # Hedef kameradan çıktı!
        ]
        
        for piksel_x in kamera_verileri:
            beyin.otonom_karar_isle(piksel_x)
            time.sleep(1)
            
        print("\n--- ACİL DURUM TESTİ ---")
        beyin.durum_degistir("DUR")
        time.sleep(1)

    except KeyboardInterrupt:
        print("\nTest iptal edildi.")
    finally:
        beyin.kapat()