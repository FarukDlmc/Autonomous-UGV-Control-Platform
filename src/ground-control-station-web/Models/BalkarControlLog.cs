using System;
namespace WebAutonomousControlStation.Models
{
    public class BalkarControlLog
    {
        public int Id { get; set; }
        public DateTime TarihSaat { get; set; }
        public string Kullanici { get; set; }
        public string KomutTipi { get; set; }
        public string KomutDetayi { get; set; }
        public string Durum { get; set; }
    }
}