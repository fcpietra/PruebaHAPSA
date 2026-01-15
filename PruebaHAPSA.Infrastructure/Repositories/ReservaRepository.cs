using Microsoft.EntityFrameworkCore;
using PruebaHAPSA.Domain.Entities;
using PruebaHAPSA.Domain.Enums;
using PruebaHAPSA.Domain.Interfaces;

namespace PruebaHAPSA.Infrastructure.Persistence.Repositories;

public class ReservaRepository : IReservaRepository
{
    private readonly PruebaHapsaDbContext _context;

    public ReservaRepository(PruebaHapsaDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Reserva reserva)
    {
        await _context.Reservas.AddAsync(reserva);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HayDisponibilidadAsync(DateTime fechaHora, int cantidadPersonas)
    {
        // Lógica simplificada: Supongamos que el restaurante tiene capacidad para 50 personas en simultáneo.
        // Buscamos reservas +- 2 horas de la fecha solicitada.
        var inicioBloque = fechaHora.AddHours(-2);
        var finBloque = fechaHora.AddHours(2);

        var ocupacionActual = await _context.Reservas
            .Where(r => r.FechaHora >= inicioBloque && r.FechaHora <= finBloque && r.Estado != EstadoReserva.Cancelada)
            .SumAsync(r => r.CantidadPersonas);

        // Si la ocupación actual + la nueva supera 50, no hay lugar.
        return (ocupacionActual + cantidadPersonas) <= 50;
    }

    public async Task<(IEnumerable<Reserva> Items, int Total)> GetByFiltersAsync(
    DateTime? fecha,
    string? tipo,
    EstadoReserva? estado,
    string? nombre,
    string? email,
    int pagina,
    int registrosPorPagina)
    {
        var query = _context.Reservas.AsQueryable();

        if (fecha.HasValue)
            query = query.Where(r => r.FechaHora.Date == fecha.Value.Date);

        if (estado.HasValue)
            query = query.Where(r => r.Estado == estado.Value);

        if (!string.IsNullOrWhiteSpace(nombre))
            query = query.Where(r => r.Nombre.Contains(nombre));

        if (!string.IsNullOrWhiteSpace(email))
            query = query.Where(r => r.Email.Contains(email));

        if (!string.IsNullOrWhiteSpace(tipo))
        {
            switch (tipo.ToLower())
            {
                case "vip":
                    query = query.OfType<ReservaVip>();
                    break;
                case "cumpleanos":
                    query = query.OfType<ReservaCumpleanos>();
                    break;
                case "estandar":
                    query = query.OfType<ReservaEstandar>();
                    break;
            }
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderByDescending(r => r.FechaHora)
            .Skip((pagina - 1) * registrosPorPagina)
            .Take(registrosPorPagina)
            .ToListAsync();

        return (items, total);
    }

    public async Task<Reserva?> GetByIdAsync(int id)
    {
        return await _context.Reservas.FindAsync(id);
    }
    public async Task UpdateAsync(Reserva reserva)
    {
        _context.Reservas.Update(reserva);
        await _context.SaveChangesAsync();
    }
}