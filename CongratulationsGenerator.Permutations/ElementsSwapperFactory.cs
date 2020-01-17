using CongratulationsGenerator.WishesDistributors;

namespace CongratulationsGenerator.Permutations
{
    public class ElementsSwapperFactory : IPermutationGeneratorFactory
    {
        public IPermutationGenerator MakePermutationGenerator()
        {
            return new ElementsSwapper();
        }
    }
}
