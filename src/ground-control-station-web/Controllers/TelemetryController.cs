using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebAutonomousControlStation.DTOs;
using WebAutonomousControlStation.Hubs;
using WebAutonomousControlStation.Models; 

[ApiController]
[Route("api/[controller]")]
public class TelemetryController : ControllerBase
{
    private readonly IHubContext<GcsHub> _hubContext; //SignalR Hub Context
    private readonly BalkarIkaDbContext _context;

    public TelemetryController(IHubContext<GcsHub> hubContext, BalkarIkaDbContext context)
    {
        _hubContext = hubContext;
        _context = context; 
    }

    [HttpPost("vitals")]
    public async Task<IActionResult> PostTelemetry([FromBody] TelemetryPayload payload) //Dashboarda canlı olarak iletmek istediğimiz telemetri verilerini içeren DTO sınıfı TelemetryPayload,
                                                                                        //API'ye POST isteğiyle gönderilir.
    {
        if (payload == null) return BadRequest("Payload boş olamaz.");

        // SignalR Yayını
        await _hubContext.Clients.All.SendAsync("ReceiveTelemetry", payload);

        // Veritabanı Modeline Eşleme
        var newLog = new TelemetryLog
        {
            TarihSaat = DateTime.Now,
            SurusModu = payload.DriveMode,
            AnlikHiz = (int)Math.Round(payload.SpeedMs),
            YonAcisi = payload.SteerPwm.ToString(),

            // Sonradan eklediğimö diğer donanım sütunları
            IsArmed = payload.IsArmed,
            PixhawkLink = payload.PixhawkLink,
            LatencyMs = payload.LatencyMs,
            MotorLpwm = payload.MotorLPwm,
            MotorRpwm = payload.MotorRPwm,
            SteerPwm = payload.SteerPwm,
            RpiCpuTemp = payload.RpiCpuTemp,
            SystemLoadPct = payload.SystemLoadPct
        };

        //veritabanına kaydetme
        _context.TelemetryLogs.Add(newLog);
        await _context.SaveChangesAsync();

        // Canlı Terminal Logu
        string logMsg = $"> DB INSERT OK: Mod={newLog.SurusModu} | Hız={newLog.AnlikHiz} | Isı={payload.RpiCpuTemp}°C";
        await _hubContext.Clients.All.SendAsync("ReceiveLog", logMsg);

        return Ok(new { status = "SUCCESS", message = "Telemetry saved and piped" });
    }
}