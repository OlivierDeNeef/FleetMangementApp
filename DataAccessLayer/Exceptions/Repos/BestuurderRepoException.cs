using System;

namespace DataAccessLayer.Exceptions.Repos
{
    public class BestuurderRepoException : Exception
    {

        public BestuurderRepoException()
        {
            
        }

        public BestuurderRepoException(string message) : base(message)
        {
            
        }

        public BestuurderRepoException(string message, Exception innerException) :base(message, innerException)
        {
            
        }
    }
}