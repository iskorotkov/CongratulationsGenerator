using System.Collections.Generic;

namespace CongratulationsGenerator.WishesDistributors
{
    public interface IElementsSwapper
    {
        IEnumerable<T> MakePermutation<T>(IEnumerable<T> original);
    }
}
