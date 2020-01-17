using System.Collections.Generic;

namespace CongratulationsGenerator.Core
{
    public interface IDistributorFactory
    {
        IWishesDistributor CreateDistributor(IEnumerable<WishCategory> wishCategories);
    }
}
