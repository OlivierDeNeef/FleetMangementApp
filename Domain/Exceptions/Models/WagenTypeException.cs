using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.Models
{
    public class WagenTypeException : Exception
    {
        public WagenTypeException()
        {

        }

        public WagenTypeException(string message) : base(message)
        {

        }

        public WagenTypeException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
