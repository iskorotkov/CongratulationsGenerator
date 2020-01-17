using System.Collections.Generic;

namespace CongratulationsGenerator.Core
{
    public interface IWishesDistributor
    {
        bool IsEnoughWishes(int recipients);
        IEnumerable<string> GetNextWishes();
    }
}
