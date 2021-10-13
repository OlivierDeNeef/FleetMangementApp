using System;

namespace DomainLayer.Exceptions.Managers
{
    public class WagenTypeManagerException : Exception
    {
        public WagenTypeManagerException()
        {
            
        }

        public WagenTypeManagerException(string message) : base(message)
        {
            
        }

        public WagenTypeManagerException(string message, Exception innerException) : base(message, innerException) 
        {
            
        }
    }
}