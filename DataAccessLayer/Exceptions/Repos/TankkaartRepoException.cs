using System;

namespace DataAccessLayer.Exceptions.Repos
{
    public class TankkaartRepoException : Exception
    {

        public TankkaartRepoException(string message) : base(message)
        {
            
        }

        public TankkaartRepoException(string message, Exception innException) : base(message,innException)
        {
            
        }
    }
}