using System;

namespace BL.Exceptions
{
    /// <summary>
    /// Custom exception for users's data problem case
    /// </summary>
    public class UserArgumentException : Exception
    {
        public UserArgumentException() { }

        public UserArgumentException(string message) : base(message) { }

    }
}
