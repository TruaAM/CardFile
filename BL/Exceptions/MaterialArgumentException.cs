using System;

namespace BL.Exceptions
{
    /// <summary>
    /// Custom exception for materil's data problem case
    /// </summary>
    public class MaterialArgumentException : Exception
    {
        public MaterialArgumentException() { }

        public MaterialArgumentException(string message) : base(message) { }

    }
}
