using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAutonomousControlStation.Models;

public partial class BalkarIkaDbContext : DbContext
{
    public BalkarIkaDbContext()
    {
    }

    public BalkarIkaDbContext(DbContextOptions<BalkarIkaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CalibrationSetting> CalibrationSettings { get; set; }

    public virtual DbSet<HardwareHealth> HardwareHealths { get; set; }

    public virtual DbSet<Oturumlar> Oturumlars { get; set; }

    public virtual DbSet<Telemetri> Telemetris { get; set; }

    public virtual DbSet<TelemetryLog> TelemetryLogs { get; set; }

    public virtual DbSet<Uyarilar> Uyarilars { get; set; }
    public virtual DbSet<AiDetectionLog> AiDetectionLogs { get; set; } = null!;
    public virtual DbSet<BalkarTelemetryLog> BalkarTelemetryLogs { get; set; } = null!;

    public virtual DbSet<BalkarSystemLog> BalkarSystemLogs { get; set; } = null!;

    public virtual DbSet<BalkarControlLog> BalkarControlLogs { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-delicate-rain-alk3pxsp-pooler.c-3.eu-central-1.aws.neon.tech;Port=5432;Database=Balkar_IKA_DB;Username=neondb_owner;Password=npg_ZfwPVW1RJ7jg");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalibrationSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("calibration_settings_pkey");

            entity.ToTable("calibration_settings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MaxHizLimit).HasColumnName("max_hiz_limit");
            entity.Property(e => e.SonGuncelleme)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("son_guncelleme");
            entity.Property(e => e.YonHassasiyeti).HasColumnName("yon_hassasiyeti");
        });

        modelBuilder.Entity<HardwareHealth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hardware_health_pkey");

            entity.ToTable("hardware_health");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CalismaSuresiDk).HasColumnName("calisma_suresi_dk");
            entity.Property(e => e.CpuSicaklik).HasColumnName("cpu_sicaklik");
            entity.Property(e => e.PilDurumuYuzde).HasColumnName("pil_durumu_yuzde");
            entity.Property(e => e.TarihSaat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tarih_saat");
        });

        modelBuilder.Entity<Oturumlar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oturumlar_pkey");

            entity.ToTable("oturumlar");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baslangic)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("baslangic");
            entity.Property(e => e.Bitis)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("bitis");
            entity.Property(e => e.MaxHiz)
                .HasDefaultValue(0.0)
                .HasColumnName("max_hiz");
            entity.Property(e => e.MinBatarya)
                .HasDefaultValue(100.0)
                .HasColumnName("min_batarya");
            entity.Property(e => e.OrtHiz)
                .HasDefaultValue(0.0)
                .HasColumnName("ort_hiz");
            entity.Property(e => e.SureSaniye).HasColumnName("sure_saniye");
            entity.Property(e => e.ToplamMesafe)
                .HasDefaultValue(0.0)
                .HasColumnName("toplam_mesafe");
        });

        modelBuilder.Entity<Telemetri>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("telemetri_pkey");

            entity.ToTable("telemetri");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alt).HasColumnName("alt");
            entity.Property(e => e.BataryaPct).HasColumnName("batarya_pct");
            entity.Property(e => e.BataryaV).HasColumnName("batarya_v");
            entity.Property(e => e.Hiz).HasColumnName("hiz");
            entity.Property(e => e.Lat).HasColumnName("lat");
            entity.Property(e => e.Lon).HasColumnName("lon");
            entity.Property(e => e.Mod)
                .HasMaxLength(20)
                .HasColumnName("mod");
            entity.Property(e => e.MotorTemp).HasColumnName("motor_temp");
            entity.Property(e => e.Pitch).HasColumnName("pitch");
            entity.Property(e => e.Roll).HasColumnName("roll");
            entity.Property(e => e.Zaman)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("zaman");
        });

        modelBuilder.Entity<TelemetryLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("telemetry_logs_pkey");

            entity.ToTable("telemetry_logs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnlikHiz).HasColumnName("anlik_hiz");
            entity.Property(e => e.MotorLpwm).HasColumnName("MotorLPwm");
            entity.Property(e => e.MotorRpwm).HasColumnName("MotorRPwm");
            entity.Property(e => e.PixhawkLink).HasMaxLength(50);
            entity.Property(e => e.SurusModu)
                .HasMaxLength(50)
                .HasColumnName("surus_modu");
            entity.Property(e => e.TarihSaat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tarih_saat");
            entity.Property(e => e.YonAcisi)
                .HasMaxLength(50)
                .HasColumnName("yon_acisi");
        });

        modelBuilder.Entity<Uyarilar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("uyarilar_pkey");

            entity.ToTable("uyarilar");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deger).HasColumnName("deger");
            entity.Property(e => e.Mesaj).HasColumnName("mesaj");
            entity.Property(e => e.Tip)
                .HasMaxLength(50)
                .HasColumnName("tip");
            entity.Property(e => e.Zaman)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("zaman");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
