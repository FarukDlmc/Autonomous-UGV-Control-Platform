using System;

namespace WebAutonomousControlStation.Models
{
    public class BalkarTelemetryLog
    {
        public int Id { get; set; }
        public DateTime TarihSaat { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Alt { get; set; }
        public double Pitch { get; set; }
        public double Roll { get; set; }
        public double Yaw { get; set; }
        public int Throttle { get; set; }
        public int Steering { get; set; }
        public int Ping { get; set; }
    }
}