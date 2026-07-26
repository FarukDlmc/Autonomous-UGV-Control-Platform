using System;
using System.Collections.Generic;

namespace WebAutonomousControlStation.Models;

public partial class CalibrationSetting
{
    public int Id { get; set; }

    public int MaxHizLimit { get; set; }

    public double YonHassasiyeti { get; set; }

    public DateTime? SonGuncelleme { get; set; }
}
