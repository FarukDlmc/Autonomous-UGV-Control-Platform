# 🚜 Autonomous UGV Control Platform
> **Otonom Kara Araçları İçin Kenar Bilişim Tabanlı Kontrol, Haberleşme ve Görüntü Aktarım Merkezi**

![C#](https://img.shields.io/badge/C%23-.NET%20Framework%2FCore-blue)
![Python](https://img.shields.io/badge/Python-3.10%2B-yellow)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC%20%26%20SignalR-purple)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-5432-blue)
![YOLOv8](https://img.shields.io/badge/YOLOv8-Nano%20Edge%20AI-brightgreen)
![License](https://img.shields.io/badge/License-MIT-green)

---

## 📋 Proje Hakkında

Bu proje; insansız kara araçlarının (UGV) uzaktan izlenebilmesini, otonom sevk edilmesini, kenar bilişim (Edge AI) yöntemleriyle çevre algılamasını ve donanım sağlığının anlık takibini sağlayan **çoklu platformlu (Web + Masaüstü)** bir kontrol ve haberleşme sistemidir.

Sistem; araç içi Raspberry Pi 5 gömülü bilgisayarı, Pixhawk sürüş kontrolcüsü, ASP.NET Core tabanlı web arayüzü ve C# WinForms masaüstü uygulamasının asenkron haberleşme köprüsüyle entegre çalışmasını sağlar.

---

## ✨ Temel Özellikler

- **🧠 Edge AI & Bilgisayar Görü:** Monokrom kamera girdileri üzerinden YOLOv8 Nano modeli ile gerçek zamanlı nesne, engel ve insan tespiti.
- **🎯 P-Kontrolcü Tabanlı Otonomi:** Kamera kadraj hatasına göre dinamik PWM (1100–1900) üreten oransal kontrolör ve durum makinesi (FSM) mimarisi.
- **⚡ Düşük Gecikmeli Haberleşme:** Flask HTTP REST API endpoint'leri ve sunucu-istemci arasında milisaniyelik veri akışı sağlayan SignalR WebSocket tünelleri.
- **🛡️ Güvenlik & Hata Toleransı:** Sinyal kopmalarında veya görüntü donmalarında motorları nötre çeken yazılımsal Watchdog ve Acil Durum Stop (E-Stop) protokolü.
- **📊 Tutarlı Veri Günlükleme:** 5432 portu üzerindeki PostgreSQL ilişkisel veritabanına mutlak **UTC** zaman damgasıyla telemetri ve sistem sağlığı kaydı.

---

## 🚀 Kurulum ve Çalıştırma

### 1. Gömülü Sistem (Raspberry Pi 5)
cd src/companion-computer
pip install -r requirements.txt
python app.py
