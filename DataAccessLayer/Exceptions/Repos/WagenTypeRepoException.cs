using System;

namespace DataAccessLayer.Exceptions.Repos
{
    public class WagenTypeRepoException : Exception
    {
        public WagenTypeRepoException()
        {
            
        }

        public WagenTypeRepoException(string message) : base(message)
        {
            
        }

        public WagenTypeRepoException(string message, Exception innerException) : base(message, innerException)
        {
            
        } 
    }
}