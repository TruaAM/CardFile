using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exceptions
{
    public class MaterialArgumentException : Exception
    {
        public MaterialArgumentException() { }

        public MaterialArgumentException(string message) : base(message) { }

    }
}
