using System;

namespace BO
{
    public class NotFreeChargeSlot: Exception
    {
        public NotFreeChargeSlot() : base()
        {
        }

        public NotFreeChargeSlot(string? message) : base(message)
        {
        }

        public NotFreeChargeSlot(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
