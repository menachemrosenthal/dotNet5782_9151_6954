using System;

namespace BL.Exceptions
{
    public class UselessDroneException : Exception
    {
        public UselessDroneException() : base()
        {
        }

        public UselessDroneException(string? message) : base(message)
        {
        }

        public UselessDroneException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
