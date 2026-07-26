import collections
import collections.abc
collections.MutableMapping = collections.abc.MutableMapping

from dronekit import connect, VehicleMode
import time
import sys

# --- AYARLAR ---
# Senin kanalların (Değiştirme)
GAZ_CH = '2'
YON_CH = '4'

# PWM DEĞERLERİ
DUR             = 1500
HIZ_ILERI       = 1650  
HIZ_GERI        = 1300  
YON_DUZ         = 1500
SAG_TAM         = 1100 
SOL_TAM         = 1900

print(">>> Pixhawk'a bağlanılıyor...")
try:
    vehicle = connect('/dev/ttyACM0', wait_ready=False, baud=115200)
except Exception as e:
    print(f"❌HATA: {e}")
    sys.exit()

# --- BAŞLANGIÇ TEMİZLİĞİ ---
print("🧹 Motorlar susturuluyor ve güvenlik kaldırılıyor...")
vehicle.channels.overrides = {GAZ_CH: DUR, YON_CH: YON_DUZ}
vehicle.parameters['BRD_SAFETYENABLE'] = 0
vehicle.parameters['ARMING_CHECK'] = 0
time.sleep(1)

def komut_bas(gaz, yon, sure, mesaj):
    print(f"👉 {mesaj}")
    vehicle.channels.overrides = {GAZ_CH: gaz, YON_CH: yon}
    time.sleep(sure)

def acil_durdur():
    print("🛑 GÜVENLİ KAPANIŞ (KILL SWITCH)!")
    vehicle.channels.overrides = {GAZ_CH: DUR, YON_CH: YON_DUZ}
    time.sleep(0.5)
    vehicle.armed = False
    vehicle.channels.overrides = {}

def geri_vites_manevrasi():
    print("🔄 Geri Vites Manevrası...")
    # Fren-Boş-Geri taktiği
    vehicle.channels.overrides = {GAZ_CH: HIZ_GERI, YON_CH: YON_DUZ}
    time.sleep(0.5)
    vehicle.channels.overrides = {GAZ_CH: DUR, YON_CH: YON_DUZ}
    time.sleep(0.5)
    print("   << Geri Gidiyor")
    vehicle.channels.overrides = {GAZ_CH: HIZ_GERI, YON_CH: YON_DUZ}
    time.sleep(3)

try:
    print("Mod: MANUAL")
    vehicle.mode = VehicleMode("MANUAL")
    time.sleep(1)

    print("ARM ediliyor...")
    vehicle.armed = True
    
    # Arm bekleme döngüsü
    limit = 0
    while not vehicle.armed:
        print("ARM Bekleniyor...")
        vehicle.armed = True
        time.sleep(1)
        limit += 1
        if limit > 5:
            print("⚠️ Zorla devam ediliyor...")
            break

    print("✅ ARMED! Hareket Başlıyor!")
    time.sleep(1)

    # --- TEST SENARYOSU ---

    # 1. OLDUĞU YERDE DİREKSİYON TESTİ
    komut_bas(DUR, SAG_TAM, 1, "Direksiyon SAĞ (Dururken)")
    komut_bas(DUR, SOL_TAM, 1, "Direksiyon SOL (Dururken)")
    komut_bas(DUR, YON_DUZ, 0.5, "Direksiyon ORTA")

    # 2. DÜZ İLERİ
    komut_bas(HIZ_ILERI, YON_DUZ, 2, "DÜZ İLERİ >>")
    komut_bas(DUR, YON_DUZ, 1, "DUR ||")

    # 3. SAĞA DÖNEREK İLERİ
    # Hem gaz veriyoruz hem direksiyonu sağa kırıyoruz
    komut_bas(HIZ_ILERI, SAG_TAM, 3, "SAĞA DÖNEREK İLERİ ↘️")
    komut_bas(DUR, YON_DUZ, 1, "DUR ||")

    # 4. SOLA DÖNEREK İLERİ
    # Hem gaz veriyoruz hem direksiyonu sola kırıyoruz
    komut_bas(HIZ_ILERI, SOL_TAM, 3, "SOLA DÖNEREK İLERİ ↙️")
    komut_bas(DUR, YON_DUZ, 1, "DUR ||")

    # 5. GERİ GELME
    geri_vites_manevrasi()
    
    # BİTİŞ
    komut_bas(DUR, YON_DUZ, 1, "TEST SONU")

except KeyboardInterrupt:
    print("\n⚠️ Test iptal edildi!")

except Exception as e:
    print(f"⚠️ Hata: {e}")

finally:
    acil_durdur()
    vehicle.close()
    print("Bağlantı kapatıldı.")