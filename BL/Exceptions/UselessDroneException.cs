using System;

namespace BO
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
