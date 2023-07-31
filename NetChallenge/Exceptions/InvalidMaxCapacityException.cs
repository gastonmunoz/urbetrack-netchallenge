using System;

namespace NetChallenge.Exceptions
{
    public class InvalidMaxCapacityException : Exception
    {
        public InvalidMaxCapacityException() : base("La capacidad máxima de la oficina debe ser mayor a cero.")
        {

        }
    }
}
