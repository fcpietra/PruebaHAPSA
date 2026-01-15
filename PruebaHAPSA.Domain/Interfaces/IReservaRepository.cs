using PruebaHAPSA.Domain.Entities;
using PruebaHAPSA.Domain.Enums;

namespace PruebaHAPSA.Domain.Interfaces;

public interface IReservaRepository
{
    Task AddAsync(Reserva reserva);
    Task<bool> HayDisponibilidadAsync(DateTime fechaHora, int cantidadPersonas);
    Task<(IEnumerable<Reserva> Items, int Total)> GetByFiltersAsync(
    DateTime? fecha,
    string? tipo,
    EstadoReserva? estado,
    string? nombre,
    string? email,
    int pagina,
    int registrosPorPagina);
    Task<Reserva?> GetByIdAsync(int id);
    Task UpdateAsync(Reserva reserva);
}