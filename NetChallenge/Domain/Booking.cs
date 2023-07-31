using System;

namespace NetChallenge.Domain
{
    /// <summary>
    /// Representa la reserva de una oficina para una fecha y un horario determinado.
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Local donde está la oficina
        /// </summary>
        public Location Location { get; set; }
        
        /// <summary>
        /// Oficina que se reserva
        /// </summary>
        public Office Office { get; set; }
        
        /// <summary>
        /// Fecha de la reunión
        /// </summary>
        public DateTime DateTime { get; set; }
        
        /// <summary>
        /// Duración de la reunión
        /// </summary>
        public TimeSpan Duration { get; set; }
        
        /// <summary>
        /// Usuario que realizó la reserva
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userName">Usuario que realizó la reserva</param>
        public Booking(string userName)
        {
            UserName = userName;
        }
    }
}