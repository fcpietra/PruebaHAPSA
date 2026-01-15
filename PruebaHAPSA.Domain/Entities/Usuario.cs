namespace PruebaHAPSA.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    // En DDD real, acá irían métodos de negocio, no solo properties anémicas.
}