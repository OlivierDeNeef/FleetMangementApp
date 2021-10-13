using System;

namespace DomainLayer.Exceptions.Managers
{
    public class BestuurderManagerException : Exception
    {
        public BestuurderManagerException()
        {
            
        }

        public BestuurderManagerException(string message) : base(message)
        {
            
        }

        public BestuurderManagerException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}