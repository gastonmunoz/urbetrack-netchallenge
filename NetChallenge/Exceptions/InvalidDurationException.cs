using System;

namespace NetChallenge.Exceptions
{
    public class InvalidDurationException : Exception
    {
        public InvalidDurationException() : base("Duración de la reunión inválida, debe ser mayor a 0 (cero).")
        {
        }

        public InvalidDurationException(string message) : base(message)
        {
        }
    }
}