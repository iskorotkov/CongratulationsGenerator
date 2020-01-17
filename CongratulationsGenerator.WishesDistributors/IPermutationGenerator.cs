using System.Collections.Generic;

namespace CongratulationsGenerator.WishesDistributors
{
    public interface IPermutationGenerator
    {
        IEnumerable<T> MakePermutation<T>(IEnumerable<T> original);
    }
}
