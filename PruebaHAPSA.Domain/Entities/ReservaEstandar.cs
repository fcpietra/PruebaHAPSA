using PruebaHAPSA.Domain.Enums;

namespace PruebaHAPSA.Domain.Entities
{
    public class ReservaEstandar : Reserva
    {
        private ReservaEstandar() { }

        public static ReservaEstandar Crear(string nombre, string email, DateTime fechaHora, int cantidadPersonas)
        {
            // Validaciones Comunes
            ValidarComunes(nombre, email, fechaHora);
            // Validación específica para ReservaEstandar
            if (cantidadPersonas < 1 || cantidadPersonas > 4)
                throw new ArgumentException("La cantidad de personas debe ser entre 1 y 4 para una reserva estándar.");
            // Validar horario de reserva (entre 19:00 y 23:30)
            var horaInicio = new TimeSpan(19, 0, 0);
            var horaFin = new TimeSpan(23, 30, 0);
            var horaReserva = fechaHora.TimeOfDay;
            if (horaReserva < horaInicio || horaReserva > horaFin)
                throw new ArgumentException("El horario de reserva debe ser entre las 19:00 y las 23:30.");


            // Crear instancia
            return new ReservaEstandar
            {
                Nombre = nombre,
                Email = email,
                FechaHora = fechaHora,
                CantidadPersonas = cantidadPersonas,
                Estado = EstadoReserva.Pendiente
            };
        }

    }
}
