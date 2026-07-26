using System;
using System.Collections.Generic;

namespace WebAutonomousControlStation.Models;

public partial class TelemetryLog
{
    public int Id { get; set; }

    public DateTime? TarihSaat { get; set; }

    public int AnlikHiz { get; set; }

    public string YonAcisi { get; set; } = null!;

    public string SurusModu { get; set; } = null!;

    public bool IsArmed { get; set; }

    public string? PixhawkLink { get; set; }

    public int LatencyMs { get; set; }

    public int MotorLpwm { get; set; }

    public int MotorRpwm { get; set; }

    public int SteerPwm { get; set; }

    public double RpiCpuTemp { get; set; }

    public int SystemLoadPct { get; set; }
}
