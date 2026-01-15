using Microsoft.AspNetCore.Mvc;
using PruebaHAPSA.Application.DTOs;
using PruebaHAPSA.Application.Interfaces;

namespace PruebaHAPSA.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly IReservaService _service;

    public ReservasController(IReservaService service)
    {
        _service = service;
    }
    /// <summary>
    /// Creates a new standard reservation with the specified details.
    /// </summary>
    /// <remarks>If the input data is invalid, the method returns a 400 Bad Request response with details. For
    /// unexpected errors, a 500 Internal Server Error response is returned.</remarks>
    /// <param name="dto">An object containing the information required to create the reservation. Cannot be null.</param>
    /// <returns>An HTTP 201 Created response with the reservation ID and a confirmation message if successful; otherwise, an
    /// error response indicating the reason for failure.</returns>
    [HttpPost]
    public async Task<IActionResult> CrearReservaEstandar([FromBody] CrearReservaDto dto)
    {
        try
        {
            var id = await _service.CrearReservaEstandarAsync(dto);
            return CreatedAtAction(nameof(CrearReservaEstandar), new { id }, new { id, mensaje = "Reserva creada con estado Pendiente" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocurrió un error interno.");
        }
    }
    /// <summary>
    /// Creates a new VIP reservation using the specified reservation details.
    /// </summary>
    /// <remarks>Returns HTTP 400 Bad Request if the input data is invalid, or HTTP 409 Conflict if a
    /// reservation cannot be created due to a conflict. For unexpected errors, returns HTTP 500 Internal Server
    /// Error.</remarks>
    /// <param name="dto">An object containing the details required to create the VIP reservation. Cannot be null.</param>
    /// <returns>An HTTP 201 Created response with the reservation ID and confirmation message if successful; otherwise, an error
    /// response indicating the reason for failure.</returns>
    [HttpPost("vip")] // POST: api/reservas/vip
    public async Task<IActionResult> CrearReservaVip([FromBody] CrearReservaVipDto dto)
    {
        try
        {
            var id = await _service.CrearReservaVipAsync(dto);
            return CreatedAtAction(nameof(CrearReservaVip), new { id }, new { id, mensaje = "Reserva VIP Confirmada" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno.");
        }
    }
    /// <summary>
    /// Creates a new birthday reservation using the specified reservation details.
    /// </summary>
    /// <param name="dto">An object containing the details required to create a birthday reservation. Cannot be null.</param>
    /// <returns>An HTTP 201 Created response with the reservation ID and a confirmation message if the reservation is created
    /// successfully; otherwise, an appropriate error response.</returns>
    [HttpPost("cumpleanos")] // POST: api/reservas/cumpleanos
    public async Task<IActionResult> CrearReservaCumpleanos([FromBody] CrearReservaCumpleanosDto dto)
    {
        try
        {
            var id = await _service.CrearReservaCumpleanosAsync(dto);
            return CreatedAtAction(nameof(CrearReservaCumpleanos), new { id }, new { id, mensaje = "Reserva de Cumpleaños creada (Pendiente)" });
        }
        catch (ArgumentException ex) 
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno.");
        }
    }
    /// <summary>
    /// Retrieves a filtered and paginated list of reservations based on the specified criteria.
    /// </summary>
    /// <remarks>This method responds to HTTP GET requests at 'api/reservas'. The returned list is determined
    /// by the filter parameters provided in the query string. If an error occurs while retrieving the data, a generic
    /// error message is returned with status code 500.</remarks>
    /// <param name="filtros">The filter criteria to apply to the reservation list. This includes parameters such as date, reservation type,
    /// and pagination options. Cannot be null.</param>
    /// <returns>An <see cref="IActionResult"/> containing the filtered list of reservations if successful; otherwise, a 500
    /// Internal Server Error result if an error occurs.</returns>
    [HttpGet] // api/reservas?fecha=2026-01-14&tipo=Vip&pagina=1
    public async Task<IActionResult> ObtenerListado([FromQuery] FiltrosReservaDto filtros)
    {
        try
        {
            var resultado = await _service.ObtenerListadoAsync(filtros);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error al obtener el listado.");
        }
    }
    /// <summary>
    /// Retrieves the details of a reservation with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the reservation to retrieve.</param>
    /// <returns>An <see cref="IActionResult"/> containing the reservation details if found; otherwise, a 404 Not Found response
    /// if the reservation does not exist, or a 500 Internal Server Error response if an error occurs.</returns>
    [HttpGet("{id}")] // GET: api/reservas/5
    public async Task<IActionResult> ObtenerDetalle(int id)
    {
        try
        {
            var detalle = await _service.ObtenerDetalleAsync(id);

            if (detalle == null)
                return NotFound(new { error = $"No se encontró la reserva con ID {id}" });

            return Ok(detalle);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno al obtener el detalle.");
        }
    }
    /// <summary>
    /// Confirms a reservation with the specified identifier.
    /// </summary>
    /// <remarks>This endpoint is typically used to update the status of a reservation to confirmed. Only
    /// reservations that exist and are in a valid state can be confirmed.</remarks>
    /// <param name="id">The unique identifier of the reservation to confirm.</param>
    /// <returns>An IActionResult indicating the result of the operation. Returns 200 OK if the reservation is confirmed
    /// successfully; 404 Not Found if the reservation does not exist; 400 Bad Request if the reservation cannot be
    /// confirmed due to its current state; or 500 Internal Server Error for unexpected errors.</returns>
    [HttpPut("{id}/confirmar")] // PUT: api/reservas/5/confirmar
    public async Task<IActionResult> ConfirmarReserva(int id)
    {
        try
        {
            await _service.ConfirmarReservaAsync(id);
            return Ok(new { mensaje = "Reserva confirmada exitosamente." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno al confirmar la reserva.");
        }
    }
    /// <summary>
    /// Cancels an existing reservation with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the reservation to cancel.</param>
    /// <param name="dto">An object containing the reason for cancellation. Must not be null.</param>
    /// <returns>An IActionResult indicating the result of the cancellation operation. Returns 200 OK if the reservation is
    /// successfully canceled; 404 Not Found if the reservation does not exist; 400 Bad Request if the request is
    /// invalid; or 409 Conflict if the reservation cannot be canceled due to its current state.</returns>
    [HttpPut("{id}/cancelar")] // PUT: api/reservas/5/cancelar
    public async Task<IActionResult> CancelarReserva(int id, [FromBody] CancelarReservaDto dto)
    {
        try
        {
            await _service.CancelarReservaAsync(id, dto.Motivo);
            return Ok(new { mensaje = "Reserva cancelada correctamente." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno al cancelar la reserva.");
        }
    }
    /// <summary>
    /// Marks the specified reservation as 'No Show'.
    /// </summary>
    /// <remarks>Use this endpoint to indicate that a reservation holder did not attend as expected. This
    /// action may affect the reservation's status and related business logic.</remarks>
    /// <param name="id">The unique identifier of the reservation to update.</param>
    /// <returns>An IActionResult indicating the result of the operation. Returns 200 OK if the reservation was successfully
    /// marked as 'No Show'; 404 Not Found if the reservation does not exist; 400 Bad Request if the operation is
    /// invalid; or 500 Internal Server Error for unexpected errors.</returns>
    [HttpPut("{id}/no-asistio")] // PUT: api/reservas/5/no-asistio
    public async Task<IActionResult> MarcarNoAsistio(int id)
    {
        try
        {
            await _service.MarcarNoAsistioAsync(id);
            return Ok(new { mensaje = "Reserva marcada como 'No Asistió'." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex) 
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno.");
        }
    }
}