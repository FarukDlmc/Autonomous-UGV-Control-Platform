using System;
using System.Collections.Generic;

namespace WebAutonomousControlStation.Models;

public partial class Uyarilar
{
    public int Id { get; set; }

    public DateTime? Zaman { get; set; }

    public string? Tip { get; set; }

    public string? Mesaj { get; set; }

    public double? Deger { get; set; }
}
