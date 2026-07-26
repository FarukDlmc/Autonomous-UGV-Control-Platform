namespace WebAutonomousControlStation.Models
{
    public class OtonomVeri
    {
        public double GpsLat { get; set; }
        public double GpsLon { get; set; }
        public double GpsPrecision { get; set; }
        public double Altitude { get; set; }
        public int SatActive { get; set; }
        public int PwmA { get; set; }
        public int PwmB { get; set; }
        public double Heading { get; set; }
        public double Pitch { get; set; }
        public double Roll { get; set; }
        public int CpuTemp { get; set; }
        public double TensorFlowGuvenSkoru { get; set; }
    }
}