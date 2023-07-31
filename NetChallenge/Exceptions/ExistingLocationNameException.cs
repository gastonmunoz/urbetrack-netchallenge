using System;

namespace NetChallenge.Exceptions
{
    public class ExistingLocationNameException : Exception
    {
        public ExistingLocationNameException(string name) : base($"Ya existe otro local con el nombre: {name}.")
        {
        }
    }
}
