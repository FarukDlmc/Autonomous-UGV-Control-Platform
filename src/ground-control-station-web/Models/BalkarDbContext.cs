using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAutonomousControlStation.Models;

public partial class BalkarDbContext : DbContext
{
    public BalkarDbContext()
    {
    }

    public BalkarDbContext(DbContextOptions<BalkarDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CalibrationSetting> CalibrationSettings { get; set; }

    public virtual DbSet<HardwareHealth> HardwareHealths { get; set; }

    public virtual DbSet<TelemetryLog> TelemetryLogs { get; set; }

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

        modelBuilder.Entity<TelemetryLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("telemetry_logs_pkey");

            entity.ToTable("telemetry_logs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnlikHiz).HasColumnName("anlik_hiz");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
