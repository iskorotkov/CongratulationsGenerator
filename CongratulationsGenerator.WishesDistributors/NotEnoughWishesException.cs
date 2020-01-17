using System;

namespace CongratulationsGenerator.WishesDistributors
{
    public class NotEnoughWishesException : Exception
    {
        public NotEnoughWishesException() : base("Number of provided wishes are not enough to congratulate everyone.")
        {
        }
    }
}
