using PruebaHAPSA.Domain.Enums;

namespace PruebaHAPSA.Application.DTOs;

public class FiltrosReservaDto
{
    public DateTime? Fecha { get; set; }
    public string? Tipo { get; set; }
    public EstadoReserva? Estado { get; set; }
    public string? Nombre { get; set; }
    public string? Email { get; set; }

    // Paginación
    public int Pagina { get; set; } = 1;
    public int RegistrosPorPagina { get; set; } = 10;
}