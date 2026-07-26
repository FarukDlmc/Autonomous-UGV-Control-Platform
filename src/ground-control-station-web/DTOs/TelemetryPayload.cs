namespace WebAutonomousControlStation.DTOs
{
    public class TelemetryPayload
    {
        // Temel Durumlar
        public string DriveMode { get; set; } // "AUTO" veya "MANUAL"
        public bool IsArmed { get; set; }
        public string PixhawkLink { get; set; } // "ttyACM0" veya "DISCONNECTED"

        // Sensör ve Hareket
        public double SpeedMs { get; set; }
        public int LatencyMs { get; set; }
        public bool ObstacleDetected { get; set; }

        // PWM Çıkışları
        public int MotorLPwm { get; set; }
        public int MotorRPwm { get; set; }
        public int SteerPwm { get; set; }
        public int CamTiltPwm { get; set; }

        // Donanım Durumu
        public double RpiCpuTemp { get; set; }
        public int SystemLoadPct { get; set; }

        // Radar Mesafeleri (6 yön: Ön, Ön-Sağ, Arka-Sağ, Arka, Arka-Sol, Ön-Sol)
        public int[] RadarDistances { get; set; }
    }
}
