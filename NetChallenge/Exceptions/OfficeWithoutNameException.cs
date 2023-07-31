using System;

namespace NetChallenge.Exceptions
{
    public class OfficeWithoutNameException : Exception
    {
        public OfficeWithoutNameException() : base("La oficina debe tener un nombre.")
        {
        }
    }
}
