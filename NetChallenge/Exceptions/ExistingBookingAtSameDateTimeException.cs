using System;

namespace NetChallenge.Exceptions
{
    public class ExistingBookingAtSameDateTimeException : Exception
    {
        public ExistingBookingAtSameDateTimeException(string userName) : base($"Ya existe una reunión agendada por {userName} con el mismo horario en la misma oficina.")
        {
        }
    }
}