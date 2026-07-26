using System;
using System.Collections.Generic;

namespace WebAutonomousControlStation.Models;

public partial class Telemetri
{
    public int Id { get; set; }

    public DateTime? Zaman { get; set; }

    public double? Lat { get; set; }

    public double? Lon { get; set; }

    public double? Alt { get; set; }

    public double? Roll { get; set; }

    public double? Pitch { get; set; }

    public double? Hiz { get; set; }

    public double? BataryaPct { get; set; }

    public double? BataryaV { get; set; }

    public double? MotorTemp { get; set; }

    public string? Mod { get; set; }
}
