using System;

namespace NetChallenge.Exceptions
{
    internal class BookingWithoutUserNameException : Exception
    {
        public BookingWithoutUserNameException() : base("La reunión debe tener un usuario asociado.")
        {
        }

        public BookingWithoutUserNameException(string message) : base(message)
        {
        }
    }
}