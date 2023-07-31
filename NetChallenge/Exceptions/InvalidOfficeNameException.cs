using System;

namespace NetChallenge.Exceptions
{
    [Serializable]
    public class InvalidOfficeNameException : Exception
    {
        public InvalidOfficeNameException() : base("Nombre de oficina inválido.")
        {
        }

        public InvalidOfficeNameException(string message) : base(message)
        {
        }
    }
}