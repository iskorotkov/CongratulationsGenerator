using System.Collections.Generic;
using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.WishesDistributors
{
    public class DistributorFactory : IDistributorFactory
    {
        public IWishesDistributor CreateDistributor(IEnumerable<WishCategory> wishCategories)
        {
            return new SimpleDistributor(wishCategories);
        }
    }
}
