using System.Collections.Generic;
using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.WishesDistributors
{
    public class SimpleDistributor : IWishesDistributor
    {
        public bool IsEnoughWishes()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetNextWishes()
        {
            throw new System.NotImplementedException();
        }

        public void SetWishes(IEnumerable<WishCategory> wishes)
        {
            throw new System.NotImplementedException();
        }
    }
}
