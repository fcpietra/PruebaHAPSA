public record ReservaResumenDto(
    int Id,
    string Nombre,
    string Email,
    DateTime FechaHora,
    int CantidadPersonas,
    string Estado,
    string Tipo
);

// Helper para devolver lista paginada
public record PagedResult<T>(List<T> Items, int TotalRegistros, int TotalPaginas);