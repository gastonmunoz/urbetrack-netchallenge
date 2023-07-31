using System;

namespace NetChallenge.Exceptions
{
    public class ExistingOfficeNameInLocationException : Exception
    {
        public ExistingOfficeNameInLocationException(string officeName, string locationName): base("El nombre: \"{}\" ya está utilizado en el local \"{}\"")
        {

        }
    }
}
