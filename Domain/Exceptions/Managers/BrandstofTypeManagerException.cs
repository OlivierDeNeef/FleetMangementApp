using System;

namespace DomainLayer.Exceptions.Managers
{
    public class BrandstofTypeManagerException : Exception
    {
        public BrandstofTypeManagerException()
        {
            
        }

        public BrandstofTypeManagerException(string message) : base(message)
        {
            
        }
        public BrandstofTypeManagerException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

    }
}