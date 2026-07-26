using System;
using System.Collections.Generic;

namespace WebAutonomousControlStation.Models;

public partial class Oturumlar
{
    public int Id { get; set; }

    public DateTime? Baslangic { get; set; }

    public DateTime? Bitis { get; set; }

    public int? SureSaniye { get; set; }

    public double? MaxHiz { get; set; }

    public double? OrtHiz { get; set; }

    public double? ToplamMesafe { get; set; }

    public double? MinBatarya { get; set; }
}
