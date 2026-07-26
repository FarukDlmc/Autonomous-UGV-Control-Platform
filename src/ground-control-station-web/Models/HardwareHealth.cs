using System;
using System.Collections.Generic;

namespace WebAutonomousControlStation.Models;

public partial class HardwareHealth
{
    public int Id { get; set; }

    public DateTime? TarihSaat { get; set; }

    public double CpuSicaklik { get; set; }

    public int CalismaSuresiDk { get; set; }

    public int PilDurumuYuzde { get; set; }
}
