using System;

namespace DomainLayer.Exceptions.Models
{
    public class RijbewijsTypeException : Exception
    {
        public RijbewijsTypeException()
        {
            
        }

        public RijbewijsTypeException(string message) : base(message)
        {
            
        }

        public RijbewijsTypeException(string message, Exception innerException): base(message, innerException)
        {
            
        }
    }
}