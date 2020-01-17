using CongratulationsGenerator.WishesDistributors;

namespace CongratulationsGenerator.Permutations
{
    public class PermutationGeneratorFactory : IPermutationGeneratorFactory
    {
        public IPermutationGenerator MakePermutationGenerator()
        {
            return new PermutationGenerator();
        }
    }
}
