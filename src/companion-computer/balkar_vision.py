import cv2
from flask import Flask, Response, jsonify
from ultralytics import YOLO
import time
import requests
import threading

app = Flask(__name__)

# C# Sunucusu Bilgileri
CSHARP_ENDPOINT = "http://10.140.82.199:5255/Home/AiTespitKaydet"

print("🧠 [AI MOTORU] YOLOv8 Nano modeli yükleniyor...")
model = YOLO('yolov8n.pt') 

KAMERA_INDEKS = 0
kamera = cv2.VideoCapture(KAMERA_INDEKS)

# Kontrol Değişkenleri
CizimAktif = True
SonKare = None

# --- Yeni yardımcı fonksiyon: C#'a veri gönderici ---
def tespit_gonder(label, conf):
    try:
        payload = {"NesneAdi": label, "DogrulukOrani": conf}
        requests.post(CSHARP_ENDPOINT, json=payload, timeout=2)
    except:
        pass # Bağlantı hatası olursa sistemi durdurmasın

def video_karesi_uret():
    global CizimAktif, SonKare
    while True:
        basarili, kare = kamera.read()
        if not basarili:
            time.sleep(2)
            continue
            
        sonuclar = model(kare, stream=True, verbose=False)
        
        # Tespitleri işle ve C#'a gönder
        for sonuc in sonuclar:
            # Tespit edilen kutuları kontrol et ve arka planda gönder
            for box in sonuc.boxes:
                label = model.names[int(box.cls[0])]
                conf = int(box.conf[0] * 100)
                
                if label in ['person', 'traffic sign', 'car']:
                    threading.Thread(target=tespit_gonder, args=(label, conf)).start()
            
            # Görüntüye kutuları çiz veya çizme
            if CizimAktif:
                islenmis_kare = sonuc.plot() 
            else:
                islenmis_kare = kare
        
        SonKare = islenmis_kare.copy()
            
        basarili_kod, jpeg_tampon = cv2.imencode('.jpg', islenmis_kare, [int(cv2.IMWRITE_JPEG_QUALITY), 70])
        if not basarili_kod:
            continue
            
        kare_byte = jpeg_tampon.tobytes()
        yield (b'--frame\r\n'
               b'Content-Type: image/jpeg\r\n\r\n' + kare_byte + b'\r\n')

@app.route('/video_feed')
def video_feed():
    return Response(video_karesi_uret(), mimetype='multipart/x-mixed-replace; boundary=frame')

@app.route('/toggle_bbox', methods=['POST', 'OPTIONS'])
def toggle_bbox():
    global CizimAktif
    CizimAktif = not CizimAktif
    resp = jsonify({"bbox_aktif": CizimAktif})
    resp.headers['Access-Control-Allow-Origin'] = '*'
    return resp

@app.route('/capture', methods=['GET'])
def capture():
    global SonKare
    if SonKare is not None:
        _, jpeg = cv2.imencode('.jpg', SonKare)
        resp = Response(jpeg.tobytes(), mimetype='image/jpeg')
        resp.headers['Access-Control-Allow-Origin'] = '*'
        resp.headers['Content-Disposition'] = 'attachment; filename=balkar_yakalanan_kare.jpg'
        return resp
    return "Görüntü Yok", 404

if __name__ == '__main__':
    print("📡 [AI VISION] Flask canlı video sunucusu başlatılıyor...")
    app.run(host='0.0.0.0', port=5000, threaded=True)