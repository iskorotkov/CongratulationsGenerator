using System;

namespace CongratulationsGenerator.Core
{
    public class NotEnoughWishesException : Exception
    {
        public NotEnoughWishesException() : base("Number of provided wishes are not enough to congratulate everyone.")
        {
        }
    }
}
