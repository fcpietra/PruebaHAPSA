namespace PruebaHAPSA.Application.DTOs
{
    public record CrearReservaVipDto(
        string Nombre,
        string Email,
        DateTime FechaHora,
        int CantidadPersonas,
        string CodigoVip,
        string? MesaPreferida
    );
}
