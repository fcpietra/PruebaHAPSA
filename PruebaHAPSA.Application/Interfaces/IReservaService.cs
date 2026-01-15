using PruebaHAPSA.Application.DTOs;
using PruebaHAPSA.Domain.Entities;
using PruebaHAPSA.Domain.Enums;

namespace PruebaHAPSA.Application.Interfaces;

public interface IReservaService
{
    Task<int> CrearReservaEstandarAsync(CrearReservaDto dto);
    Task<int> CrearReservaVipAsync(CrearReservaVipDto dto);
    Task<int> CrearReservaCumpleanosAsync(CrearReservaCumpleanosDto dto);
    Task<PagedResult<ReservaResumenDto>> ObtenerListadoAsync(FiltrosReservaDto filtros);
    Task<ReservaDetalleDto?> ObtenerDetalleAsync(int id);
    Task ConfirmarReservaAsync(int id);
    Task CancelarReservaAsync(int id, string motivo);
    Task MarcarNoAsistioAsync(int id);
}