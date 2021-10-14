using System;

namespace DomainLayer.Exceptions.Models
{
    public class BrandstofTypeException : Exception
    {

        public BrandstofTypeException()
        {
            
        }

        public BrandstofTypeException(string message): base(message)
        {
            
        }

        public BrandstofTypeException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}