using System;

namespace NetChallenge.Exceptions
{
    public class InvalidLocationNameException: Exception
    {
        public InvalidLocationNameException(): base("La oficina no tiene un nombre de local válido.")
        {
        }
    }
}
