using System;
using System.Reflection.Metadata.Ecma335;

namespace DomainLayer.Exceptions
{
    public class AdresException : Exception
    {

        public AdresException()
        {
            
        }

        public AdresException(string message) : base(message)
        {
            
        }

        public AdresException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
        
    }
}