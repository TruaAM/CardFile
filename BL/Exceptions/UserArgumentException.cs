using System;

namespace BL.Exceptions
{
    public class UserArgumentException : Exception
    {
        public UserArgumentException() { }

        public UserArgumentException(string message) : base(message) { }

    }
}
