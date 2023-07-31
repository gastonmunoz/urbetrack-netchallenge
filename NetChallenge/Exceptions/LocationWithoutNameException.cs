using System;

namespace NetChallenge.Exceptions
{
    public class LocationWithoutNameException : Exception
    {
        public LocationWithoutNameException() : base("El local debe tener un nombre.")
        {
        }
    }
}
