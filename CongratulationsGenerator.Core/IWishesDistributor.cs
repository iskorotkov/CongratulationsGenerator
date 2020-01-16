using System.Collections.Generic;

namespace CongratulationsGenerator.Core
{
    public interface IWishesDistributor
    {
        bool IsEnoughWishes();
        IEnumerable<string> GetNextWishes();
        void SetWishes(IEnumerable<WishCategory> wishes);
    }
}
