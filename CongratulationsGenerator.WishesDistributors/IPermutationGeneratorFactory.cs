namespace CongratulationsGenerator.WishesDistributors
{
    public interface IPermutationGeneratorFactory
    {
        IElementsSwapper MakePermutationGenerator();
    }
}
