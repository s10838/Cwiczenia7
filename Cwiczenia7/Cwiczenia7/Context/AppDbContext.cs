using Cwiczenia7.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia7.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<SailboatReservation> SailboatReservations { get; set; }
    public DbSet<Sailboat> Sailboats { get; set; }
    public DbSet<BoatStandard> BoatStandards { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientCategory> ClientCategories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SailboatReservation>()
            .HasKey(sr => new { sr.IdSailboat, sr.IdReservation });
    }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseLazyLoadingProxies();
    // }
}
