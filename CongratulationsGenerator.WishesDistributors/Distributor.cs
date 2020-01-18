using System.Collections.Generic;
using System.Linq;
using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.WishesDistributors
{
    public class Distributor : IWishesDistributor
    {
        public static IPermutationGeneratorFactory PermutationGeneratorFactory;

        public Distributor(IEnumerable<WishCategory> wishCategories)
        {
            _wishCategories = wishCategories.ToList();
            _wishCategories.Sort((e1, e2) => e2.Wishes.Count.CompareTo(e1.Wishes.Count));
        }

        private readonly List<WishCategory> _wishCategories;
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
            var tripleGenerator = new TripleGenerator();
            tripleGenerator.GenerateTriples(_wishCategories.ToList(), permutationsGenerator);

            _optimalVariants = tripleGenerator.OptimalVariants;
            _otherVariants = tripleGenerator.OtherVariants;
        }
    }
}
