using System;

namespace DomainLayer.Exceptions.Managers
{
    public class TankkaartManagerException : Exception
    {

        public TankkaartManagerException()
        {
            
        }

        public TankkaartManagerException(string message) : base(message)
        {
            
        }

        public TankkaartManagerException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}