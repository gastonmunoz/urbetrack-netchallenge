using System;

namespace NetChallenge.Exceptions
{
    public class LocationWithoutNeighborhoodException : Exception
    {
        public LocationWithoutNeighborhoodException() : base("El local debe tener un barrio.")
        {
        }
    }
}
