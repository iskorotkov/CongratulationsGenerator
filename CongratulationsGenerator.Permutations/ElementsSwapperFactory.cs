using CongratulationsGenerator.WishesDistributors;

namespace CongratulationsGenerator.Permutations
{
    public class ElementsSwapperFactory : IPermutationGeneratorFactory
    {
        public IElementsSwapper MakePermutationGenerator()
        {
            return new ElementsSwapper();
        }
    }
}
