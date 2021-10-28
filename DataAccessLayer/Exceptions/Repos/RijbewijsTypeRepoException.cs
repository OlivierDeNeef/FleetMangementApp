using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Exceptions.Repos
{
    public class RijbewijsTypeRepoException: Exception
    {
        public RijbewijsTypeRepoException(string message) : base(message)
        {
        }

        public RijbewijsTypeRepoException(string message,Exception innerException) : base(message,innerException)
        {
        }
    }
}
