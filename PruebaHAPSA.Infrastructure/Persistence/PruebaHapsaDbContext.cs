using Microsoft.EntityFrameworkCore;
using PruebaHAPSA.Domain.Entities;

namespace PruebaHAPSA.Infrastructure.Persistence;

public class PruebaHapsaDbContext : DbContext
{
    public PruebaHapsaDbContext(DbContextOptions<PruebaHapsaDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<ReservaVip> ReservasVip { get; set; }
    public DbSet<ReservaCumpleanos> ReservasCumpleanos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración TPH (Opcional, EF lo hace solo, pero para ser explícitos)
        modelBuilder.Entity<Reserva>()
            .HasDiscriminator<string>("TipoReserva")
            .HasValue<ReservaEstandar>("Estandar") 
            .HasValue<ReservaVip>("Vip")
            .HasValue<ReservaCumpleanos>("Cumpleanos");
    }
}