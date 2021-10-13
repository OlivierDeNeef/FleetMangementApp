using System;

namespace DomainLayer.Exceptions.Models
{
    public class VoertuigException : Exception
    {

        public VoertuigException()
        {
            
        }

        public VoertuigException(string message) : base(message)
        {
            
        }

        public VoertuigException(string message, Exception innerException) : base(message,innerException)
        {
            
        }
    }
}