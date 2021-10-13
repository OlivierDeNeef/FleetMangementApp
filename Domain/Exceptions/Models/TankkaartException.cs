using System;

namespace DomainLayer.Exceptions.Models
{
    public class TankkaartException : Exception
    {
        public TankkaartException()
        {
            
        }

        public TankkaartException(string message):base(message)
        {
            
        }

        public TankkaartException(string message, Exception innerException): base(message, innerException)
        {
            
        }
    }
}