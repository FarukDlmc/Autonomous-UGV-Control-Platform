class BalkarPKontrolcu:
    def __init__(self, kp=1.2, merkez_pwm=1500, sol_limit=1900, sag_limit=1100):
        """
        P-Kontrolcü Sınıfı
        kp: Oransal kazanç katsayısı (Aracın hassasiyeti). Deneyerek optimize edilir.
        merkez_pwm: Direksiyonun tam düz durduğu PWM değeri (1500).
        sol_limit: Direksiyonun dönebileceği maksimum sol PWM (1900).
        sag_limit: Direksiyonun dönebileceği maksimum sağ PWM (1100).
        """
        self.kp = kp
        self.merkez_pwm = merkez_pwm
        self.sol_limit = sol_limit
        self.sag_limit = sag_limit
        self.ekran_merkezi = 320 # 640 genişlikteki kamera görüntüsünün tam ortası

    def direksiyon_hesapla(self, hedef_x):
        """
        Kameradan gelen hedefin X koordinatına göre direksiyon PWM değerini hesaplar.
        """
        if hedef_x is None:
            # Eğer kamera o an hedefi göremediyse direksiyonu düz tut
            return self.merkez_pwm

        # 1. Hatayı Hesapla (Hata = Merkez - Anlık Durum)
        hata = self.ekran_merkezi - hedef_x

        # 2. Direksiyon PWM değerini oransal olarak hesapla
        hesaplanan_pwm = self.merkez_pwm + (self.kp * hata)
        
        # Geometrik dönüşüm uyarısı: Küsuratlı değerleri tam sayıya yuvarla
        hesaplanan_pwm = int(round(hesaplanan_pwm))

        # 3. DOYUM (Saturation) FİLTRESİ: 
        # Hesaplanan değer servo motorun fiziksel limitlerini aşmasın (1100 - 1900 arası kalsın)
        # Senin sisteminde sağ tam 1100 (küçük değer), sol tam 1900 (büyük değer)
        if hesaplanan_pwm > self.sol_limit:
            hesaplanan_pwm = self.sol_limit
        elif hesaplanan_pwm < self.sag_limit:
            hesaplanan_pwm = self.sag_limit

        return hesaplanan_pwm

# --- ALGORİTMAYI MASADA TEST ETMEK İÇİN SİMÜLASYON ---
if __name__ == "__main__":
    import time
    
    # Kontrolcüyü varsayılan Kp = 1.2 ile başlatıyoruz
    kontrolcu = BalkarPKontrolcu(kp=1.2)
    
    print("🔬 P-Kontrolcü Matematiksel Testi Başladı (Kamera Genişliği: 640px)")
    print(f"Mevcut Hassasiyet (Kp): {kontrolcu.kp}\n")
    
    # Farklı senaryolarda kameradan gelen sahte X koordinatları
    test_koordinatlari = [
        (320, "Hedef Tam Merkezde (Düz Gitmeli)"),
        (350, "Hedef Hafif Sağda (Direksiyon hafif sağa kırılmalı)"),
        (500, "Hedef Çok Sağda (Direksiyon sert sağa kırılmalı)"),
        (640, "Hedef En Sağda (Doyum filtresi devreye girmeli - 1100 PWM olmalı)"),
        (290, "Hedef Hafif Solda (Direksiyon hafif sola kırılmalı)"),
        (100, "Hedef Çok Solda (Doyum filtresi devreye girmeli - 1900 PWM olmalı)"),
        (None, "Hedef Kayboldu (Güvenli mod - 1500 PWM olmalı)")
    ]
    
    for x_koord, aciklama in test_koordinatlari:
        pwm_sonuc = kontrolcu.direksiyon_hesapla(x_koord)
        print(f"📋 Durum: {aciklama}")
        print(f"   -> Gelen X: {x_koord} | Hesaplanan Direksiyon PWM: {pwm_sonuc}")
        print("-" * 60)
        time.sleep(0.5)
