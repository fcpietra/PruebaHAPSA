using PruebaHAPSA.Application.DTOs;
using PruebaHAPSA.Application.Interfaces;
using PruebaHAPSA.Domain.Entities;
using PruebaHAPSA.Domain.Enums;
using PruebaHAPSA.Domain.Interfaces;

namespace PruebaHAPSA.Application.Services;

public class ReservaService : IReservaService
{
    private readonly IReservaRepository _repository;

    public ReservaService(IReservaRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> CrearReservaEstandarAsync(CrearReservaDto dto)
    {
        var reserva = ReservaEstandar.Crear(dto.Nombre, dto.Email, dto.FechaHora, dto.CantidadPersonas);

        await _repository.AddAsync(reserva);

        return reserva.Id;
    }

    public async Task<int> CrearReservaVipAsync(CrearReservaVipDto dto)
    {
        var hayLugar = await _repository.HayDisponibilidadAsync(dto.FechaHora, dto.CantidadPersonas);

        if (!hayLugar)
            throw new InvalidOperationException("No hay mesas disponibles para ese horario.");

        var reservaVip = ReservaVip.Crear(
            dto.Nombre,
            dto.Email,
            dto.FechaHora,
            dto.CantidadPersonas,
            dto.CodigoVip,
            dto.MesaPreferida
        );

        await _repository.AddAsync(reservaVip);
        return reservaVip.Id;
    }
    public async Task<int> CrearReservaCumpleanosAsync(CrearReservaCumpleanosDto dto)
    {
        var reserva = ReservaCumpleanos.Crear(
            dto.Nombre,
            dto.Email,
            dto.FechaHora,
            dto.CantidadPersonas,
            dto.EdadCumpleanero,
            dto.RequiereTorta
        );

        await _repository.AddAsync(reserva);

        return reserva.Id;
    }
    public async Task<PagedResult<ReservaResumenDto>> ObtenerListadoAsync(FiltrosReservaDto filtros)
    {
        var (entidades, total) = await _repository.GetByFiltersAsync(
            filtros.Fecha,
            filtros.Tipo,
            filtros.Estado,
            filtros.Nombre,
            filtros.Email,
            filtros.Pagina,
            filtros.RegistrosPorPagina
        );

        var dtos = entidades.Select(r => new ReservaResumenDto(
            r.Id,
            r.Nombre,
            r.Email,
            r.FechaHora,
            r.CantidadPersonas,
            r.Estado.ToString(),
            ObtenerTipoLegible(r) 
        )).ToList();

        var totalPaginas = (int)Math.Ceiling((double)total / filtros.RegistrosPorPagina);

        return new PagedResult<ReservaResumenDto>(dtos, total, totalPaginas);
    }

    public async Task<ReservaDetalleDto?> ObtenerDetalleAsync(int id)
    {
        var reserva = await _repository.GetByIdAsync(id);

        if (reserva == null)
            return null;

        string? codigoVip = null;
        string? mesaPreferida = null;
        int? edadCumpleanero = null;
        bool? requiereTorta = null;

        if (reserva is ReservaVip vip)
        {
            codigoVip = vip.CodigoVip;
            mesaPreferida = vip.MesaPreferida;
        }
        else if (reserva is ReservaCumpleanos cumple)
        {
            edadCumpleanero = cumple.EdadCumpleanero;
            requiereTorta = cumple.RequiereTorta;
        }

        return new ReservaDetalleDto(
            reserva.Id,
            reserva.Nombre,
            reserva.Email,
            reserva.FechaHora,
            reserva.CantidadPersonas,
            reserva.Estado.ToString(),
            ObtenerTipoLegible(reserva),
            codigoVip,
            mesaPreferida,
            edadCumpleanero,
            requiereTorta
        );
    }
    public async Task ConfirmarReservaAsync(int id)
    {
        var reserva = await _repository.GetByIdAsync(id);
        if (reserva == null)
            throw new KeyNotFoundException($"La reserva {id} no existe.");

        var hayLugar = await _repository.HayDisponibilidadAsync(reserva.FechaHora, 0);
        if (!hayLugar)
            throw new InvalidOperationException("No hay disponibilidad suficiente para confirmar esta reserva.");

        reserva.Confirmar();

        await _repository.UpdateAsync(reserva);
    }
    public async Task CancelarReservaAsync(int id, string motivo)
    {
        var reserva = await _repository.GetByIdAsync(id);

        if (reserva == null)
            throw new KeyNotFoundException($"No se encontró la reserva con ID {id}.");

        reserva.Cancelar(motivo);

        await _repository.UpdateAsync(reserva);
    }
    public async Task MarcarNoAsistioAsync(int id)
    {
        var reserva = await _repository.GetByIdAsync(id);

        if (reserva == null)
            throw new KeyNotFoundException($"No se encontró la reserva {id}.");

        reserva.MarcarNoAsistio();

        await _repository.UpdateAsync(reserva);
    }

    #region Helpers
    private string ObtenerTipoLegible(Reserva reserva)
    {
        return reserva switch
        {
            ReservaVip => "VIP",
            ReservaCumpleanos => "Cumpleaños",
            ReservaEstandar => "Estándar",
        };
    }
    #endregion
}