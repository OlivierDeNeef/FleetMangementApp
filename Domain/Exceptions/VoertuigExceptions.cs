using System;

namespace DomainLayer.Exceptions
{
    public class VoertuigExceptions : Exception
    {

        public VoertuigExceptions()
        {
            
        }

        public VoertuigExceptions(string message) : base(message)
        {
            
        }

        public VoertuigExceptions(string message, Exception innerException) : base(message,innerException)
        {
            
        }
    }
}