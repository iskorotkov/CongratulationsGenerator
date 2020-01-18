using System.Collections.Generic;
using System.Linq;
using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.WishesDistributors
{
    public partial class Distributor : IWishesDistributor
    {
        public static IPermutationGeneratorFactory PermutationGeneratorFactory;

        public Distributor(IEnumerable<WishCategory> wishCategories)
        {
            _wishCategories = wishCategories;
        }

        private readonly IEnumerable<WishCategory> _wishCategories;
        private List<Triple> _optimalVariants;
        private List<Triple> _otherVariants;
        private bool _generated;

        public bool IsEnoughWishes(int recipients)
        {
            var variants = _wishCategories.Aggregate(1, (current, wishCategory) => current * wishCategory.Wishes.Count);
            return variants >= recipients;
        }


        public IEnumerable<string> GetNextWishes()
        {
            if (!_generated)
            {
                GenerateTriples();
                _generated = true;
            }

            var collectionToUse = _optimalVariants.Count > 0
                ? _optimalVariants
                : _otherVariants.Count > 0
                    ? _otherVariants
                    : null;

            if (collectionToUse == null)
            {
                throw new NotEnoughWishesException();
            }

            var result = collectionToUse.First();
            collectionToUse.Remove(result);
            return result.Wishes();
        }

        private void GenerateTriples()
        {
            var permutationsGenerator = PermutationGeneratorFactory.MakePermutationGenerator();

            var wishes = _wishCategories
                .Select(category => permutationsGenerator.MakePermutation(category.Wishes).ToList())
                .ToList();
            wishes = permutationsGenerator.MakePermutation(wishes).ToList();

            var tripleGenerator = new TripleGenerator();
            tripleGenerator.GenerateTriples(wishes);

            _optimalVariants = tripleGenerator.OptimalVariants;
            _otherVariants = tripleGenerator.OtherVariants;
        }
    }
}
