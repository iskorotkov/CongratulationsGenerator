using System.Collections.Generic;
using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.WishesDistributors
{
    public class SimpleDistributor : IWishesDistributor
    {
        public SimpleDistributor(IEnumerable<WishCategory> wishCategories)
        {
            _wishCategories = wishCategories;
        }

        private readonly IEnumerable<WishCategory> _wishCategories;
        
        public bool IsEnoughWishes(int recipients)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetNextWishes()
        {
            throw new System.NotImplementedException();
        }
    }
}
