using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebAutonomousControlStation.DTOs;

namespace WebAutonomousControlStation.Hubs
{
    public class GcsHub : Hub
    {
        // Bu metodu doğrudan Python veya arka planda çalışan API Controller tetikleyecek
        public async Task SendTelemetryUpdate(TelemetryPayload payload)
        {
            // Dashboarddaki grafikleri güncellemek için
            await Clients.All.SendAsync("ReceiveTelemetry", payload);
        }

        // Terminal logları için ayrı bir canlı akış
        public async Task SendTerminalLog(string message)
        {
            await Clients.All.SendAsync("ReceiveLog", message);
        }
    }
}
