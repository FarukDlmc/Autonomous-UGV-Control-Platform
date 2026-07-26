using System;

namespace WebAutonomousControlStation.Models
{
    public class BalkarSystemLog
    {
        public int Id { get; set; }
        public DateTime TarihSaat { get; set; }
        public string Module { get; set; }
        public string LogLevel { get; set; } // INFO, WARNING, ERROR, CRITICAL
        public string Message { get; set; }
    }
}