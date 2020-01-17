using CongratulationsGenerator.WishesDistributors;

namespace CongratulationsGenerator.Permutations
{
    public class RandomInserterFactory : IPermutationGeneratorFactory
    {
        public IPermutationGenerator MakePermutationGenerator()
        {
            return new RandomInserter();
        }
    }
}
