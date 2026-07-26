using System;

namespace WebAutonomousControlStation.Models
{
    public class AiDetectionLog
    {
        public int Id { get; set; }
        public DateTime TarihSaat { get; set; }
        public string NesneAdi { get; set; } = null!;
        public int DogrulukOrani { get; set; }
    }
}