using System;
using System.Threading;

namespace DomainLayer.Exceptions.Managers
{
    public class RijbewijsTypeManagerException : Exception
    {
        public RijbewijsTypeManagerException()
        {
            
        }

        public RijbewijsTypeManagerException(string message) : base(message)
        {
            
        }

        public RijbewijsTypeManagerException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}