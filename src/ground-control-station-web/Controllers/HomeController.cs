using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using WebAutonomousControlStation.Models;
using Microsoft.AspNetCore.SignalR;
using WebAutonomousControlStation.Hubs;
using Microsoft.EntityFrameworkCore;

namespace WebAutonomousControlStation.Controllers
{
    public class HomeController : Controller
    {
        // Aracın dinlediği IP ve port
        private readonly string aracIP = "127.0.0.1";
        private readonly int port = 5000;

        private readonly BalkarIkaDbContext _context;
        private readonly IHubContext<TelemetryHub> _hubContext;

        private static DateTime _sonTelemetriKayitZamani = DateTime.MinValue;

        public HomeController(BalkarIkaDbContext context, IHubContext<TelemetryHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // Python'dan gelecek olan canlı verileri yakalayacağımız yer
        [HttpPost]
        public async Task<IActionResult> CanliVeriAl([FromBody] OtonomVeri veri)
        {
            // Dashboard'a fırlat
            await _hubContext.Clients.All.SendAsync("CanliVeriGuncelle", veri);
            return Ok();
        }

        [HttpPost]
        public IActionResult AracKomutGonder(string komut) //Arayüzden gelen manuel hareket emirlerini araca gmnder
        {
            try
            {
                // TCP/IP üzerinden araca bağlanıyoruz
                using (TcpClient client = new TcpClient(aracIP, port))
                using (NetworkStream stream = client.GetStream())
                {
                    // Gönderilecek strigi byte dizisine çeviriyoruz
                    byte[] data = Encoding.UTF8.GetBytes(komut);

                    // Veriyi fırlatıyoruz
                    stream.Write(data, 0, data.Length);
                }

                // Komut başarılıysa, veritabanına kaydet
                var log = new TelemetryLog
                {
                    TarihSaat = DateTime.Now,
                    AnlikHiz = komut == "DUR" ? 0 : 15,
                    YonAcisi = "0 (Düz)",
                    SurusModu = $"Manuel Komut: {komut}"
                };

                _context.TelemetryLogs.Add(log);
                _context.SaveChanges();

                return Json(new { success = true, message = $"Komut araca iletildi ve buluta (Neon) kaydedildi: {komut}" });
            }
            catch (Exception ex)
            {
                // Araç kapalıysa veya Wi-Fi koptuysa 
                return Json(new { success = false, message = $"Bağlantı hatası: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AiTespitKaydet([FromBody] WebAutonomousControlStation.Models.AiDetectionLog tespit) // Python'dan gelen nesne tespitleri
        {
            if (tespit == null || string.IsNullOrEmpty(tespit.NesneAdi))
                return BadRequest("Geçersiz veri");

            // Veritabanına kaydet
            tespit.TarihSaat = DateTime.UtcNow;
            _context.AiDetectionLogs.Add(tespit);
            await _context.SaveChangesAsync();

            // Dashboarda fırlat
            await _hubContext.Clients.All.SendAsync("YeniAiTespiti", tespit.NesneAdi, tespit.DogrulukOrani);

            return Ok(new { success = true, message = "Tespit kaydedildi." });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTelemetry([FromBody] TelemetryPacket data) // Python'dan gelen telemetri verilerini arayüzde göstermek için yakalayacağımız yer
        {
            if (data == null) return BadRequest();

            // Dashboard'a fırlat
            await _hubContext.Clients.All.SendAsync("ReceiveTelemetry", data);

            return Ok();
        }

        //Telemetry sayfası için veriler.
        public class TelemetryPacket
        {
            public double Lat { get; set; }
            public double Lon { get; set; }
            public double Alt { get; set; }
            public int Satellites { get; set; }
            public int Ping { get; set; }
            public int Throttle { get; set; }
            public int Steering { get; set; }
            public double Yaw { get; set; }
            public double Pitch { get; set; }
            public double Roll { get; set; }
            public int AiConf { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> SendDriveCommand([FromBody] DriveRequest request) // Arayüzden gelen hareket komutlarını Pi 5'e iletmek için kullanacağımız yer
        {
            if (request == null || string.IsNullOrEmpty(request.Command))
                return BadRequest();

            // Pi 5 üzerinde flask ile çalışan drive API'sine komutu gönder.
            string piAddress = "http://10.140.82.176:5000/drive";

            using var client = new HttpClient();
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new { action = request.Command }), System.Text.Encoding.UTF8, "application/json");

            try
            {
                await client.PostAsync(piAddress, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Pi 5'e komut giderken hata: " + ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> BalkarTelemetry([FromBody] TelemetryPacket data) //Telemetry sayfası için gelen veriler
        {
            if (data == null) return BadRequest();

            await _hubContext.Clients.All.SendAsync("ReceiveTelemetry", data);

            // Veritabanına kaydetme, performans için saniyede 1 defa kayıt 
            if ((DateTime.UtcNow - _sonTelemetriKayitZamani).TotalSeconds >= 1)
            {
                var yeniLog = new WebAutonomousControlStation.Models.BalkarTelemetryLog
                {
                    TarihSaat = DateTime.UtcNow,
                    Lat = data.Lat,
                    Lon = data.Lon,
                    Alt = data.Alt,
                    Pitch = data.Pitch,
                    Roll = data.Roll,
                    Yaw = data.Yaw,
                    Throttle = data.Throttle,
                    Steering = data.Steering,
                    Ping = data.Ping
                };

                _context.BalkarTelemetryLogs.Add(yeniLog);
                await _context.SaveChangesAsync();

                _sonTelemetriKayitZamani = DateTime.UtcNow;
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddSystemLog([FromBody] SystemLogRequest request) //SystemLogs sayfası için gelen loglar
        {
            if (request == null) return BadRequest();

            var yeniLog = new WebAutonomousControlStation.Models.BalkarSystemLog
            {
                TarihSaat = DateTime.UtcNow,
                Module = request.Module,
                LogLevel = request.LogLevel,
                Message = request.Message
            };

            _context.BalkarSystemLogs.Add(yeniLog);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceiveSystemLog", yeniLog);

            return Ok();
        }

        public class SystemLogRequest
        {
            public string Module { get; set; }
            public string LogLevel { get; set; }
            public string Message { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> SendSystemCommand([FromBody] SystemCommandRequest request) //ControlPanel sayfasından gelen sistem komutlarını Pi 5'e iletmek için
        {
            if (request == null) return BadRequest();

            var log = new WebAutonomousControlStation.Models.BalkarControlLog
            {
                TarihSaat = DateTime.UtcNow,
                Kullanici = "Admin",
                KomutTipi = request.CommandType,
                KomutDetayi = request.CommandDetail,
                Durum = "İLETİLDİ"
            };
            _context.BalkarControlLogs.Add(log);
            await _context.SaveChangesAsync();

            // Pi 5 üzerinde flask ile çalışan system_command API'sine komutu gönder.
            string piAddress = $"http://{request.TargetIp}:5000/system_command";

            try
            {
                using var client = new HttpClient();
                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
                await client.PostAsync(piAddress, content);
            }
            catch (Exception)
            {
                return Ok(new { success = false, message = "Araç bağlantısı yok!" });
            }

            return Ok(new { success = true });
        }

        public class SystemCommandRequest
        {
            public string CommandType { get; set; }
            public string CommandDetail { get; set; }
            public string TargetIp { get; set; }
        }

        public class DriveRequest
        {
            public string Command { get; set; }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult AIVision()
        {
            return View();
        }

        public IActionResult Telemetry()
        {
            return View();
        }

        public async Task<IActionResult> SystemLogs() // SystemLogs sayfası için geçmiş loglar
        {
            var gecmisLoglar = await _context.BalkarSystemLogs
                .OrderByDescending(l => l.TarihSaat)
                .Take(30)
                .ToListAsync();

            return View(gecmisLoglar);
        }

        public IActionResult ControlPanel()
        {
            return View();
        }
    }
}