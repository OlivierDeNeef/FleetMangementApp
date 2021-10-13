using System;

namespace DomainLayer.Exceptions.Managers
{
    public class VoertuigManagerException : Exception
    {

        public VoertuigManagerException()
        {
            
        }

        public VoertuigManagerException(string message) : base(message)
        {
            
        }

        public VoertuigManagerException(string message, Exception innerException) : base( message, innerException)
        {
            
        }
    }
}