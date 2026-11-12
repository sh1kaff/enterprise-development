using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;

namespace TransportCompany.Infrastructure.MsSqlServer.Data;

/// <summary>
/// Контекст базы данных
/// </summary>
public class TransportCompanyDbContext(DbContextOptions<TransportCompanyDbContext> options)
    : DbContext(options)
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<VehicleModel> VehicleModels => Set<VehicleModel>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Trip> Trips => Set<Trip>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).HasMaxLength(100);
            entity.Property(c => c.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.PassportNumber).HasMaxLength(20);
            entity.Property(d => d.FullName).HasMaxLength(100);
        });

        modelBuilder.Entity<VehicleModel>(entity =>
        {
            entity.HasKey(m => m.Id);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.LicensePlate).HasMaxLength(15);

            entity.HasOne(v => v.Model)
                .WithMany()
                .HasForeignKey(v => v.ModelId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Origin).HasMaxLength(100);
            entity.Property(t => t.Destination).HasMaxLength(100);
            entity.Property(t => t.TransportationCost).HasPrecision(18, 2);

            entity.HasOne(t => t.Driver)
                .WithMany()
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Vehicle)
                .WithMany()
                .HasForeignKey(t => t.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Client)
                .WithMany()
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}