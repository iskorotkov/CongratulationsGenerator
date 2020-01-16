using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.WishesDistributors
{
    public class DistributorFactory : IDistributorFactory
    {
        public IWishesDistributor CreateDistributor()
        {
            return new SimpleDistributor();
        }
    }
}
