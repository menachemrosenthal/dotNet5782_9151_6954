﻿using System;

namespace BO
{
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException() : base()
        {
        }

        public DuplicateItemException(string? message) : base(message)
        {
        }

        public DuplicateItemException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
