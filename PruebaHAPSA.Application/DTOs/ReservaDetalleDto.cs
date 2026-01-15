namespace PruebaHAPSA.Application.DTOs;

public record ReservaDetalleDto(
    int Id,
    string Nombre,
    string Email,
    DateTime FechaHora,
    int CantidadPersonas,
    string Estado,
    string Tipo,

    // Datos VIP
    string? CodigoVip,
    string? MesaPreferida,

    // Datos Cumpleaños
    int? EdadCumpleanero,
    bool? RequiereTorta
);