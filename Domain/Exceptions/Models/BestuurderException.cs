using System;

namespace DomainLayer.Exceptions.Models
{
    public class BestuurderException : Exception
    {
        public BestuurderException()
        {
            
        }

        public BestuurderException(string message) : base(message)
        {
            
        }

        public BestuurderException(string message, Exception innerException): base(message,innerException)
        {
            
        }
    }
}