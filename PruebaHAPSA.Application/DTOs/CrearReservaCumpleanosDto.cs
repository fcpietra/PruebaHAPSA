namespace PruebaHAPSA.Application.DTOs
{
    public record CrearReservaCumpleanosDto(
        string Nombre,
        string Email,
        DateTime FechaHora,
        int CantidadPersonas,
        int EdadCumpleanero,
        bool RequiereTorta
    );
}
