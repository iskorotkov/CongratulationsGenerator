namespace CongratulationsGenerator.Core
{
    public interface IDistributorFactory
    {
        IWishesDistributor CreateDistributor();
    }
}
