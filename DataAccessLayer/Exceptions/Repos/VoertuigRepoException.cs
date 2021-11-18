using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Exceptions.Repos
{
    public class VoertuigRepoException : Exception
    {
        public VoertuigRepoException(string message) : base(message)
        {
        }

        public VoertuigRepoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
