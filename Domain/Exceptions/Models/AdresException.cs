using System;

namespace DomainLayer.Exceptions.Models
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