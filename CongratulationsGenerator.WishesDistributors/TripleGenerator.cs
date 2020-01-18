using System.Collections.Generic;
using System.Linq;
using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.WishesDistributors
{
    public class TripleGenerator
    {
        private List<int> _wishesUsedPerCategory;
        public List<Triple> OptimalVariants { get; private set; } = new List<Triple>();
        public List<Triple> OtherVariants { get; private set; } = new List<Triple>();

        public void GenerateTriples(IReadOnlyList<WishCategory> wishes, IPermutationGenerator permutationGenerator)
        {
            _wishesUsedPerCategory = new List<int>(wishes.Count);
            for (var i = 0; i < wishes.Count; i++)
            {
                _wishesUsedPerCategory.Add(0);
            }

            IterateOverCategories(wishes);
            
            OptimalVariants = permutationGenerator.MakePermutation(OptimalVariants).ToList();
            OtherVariants = permutationGenerator.MakePermutation(OtherVariants).ToList();
        }

        private void IterateOverCategories(IReadOnlyList<WishCategory> wishes)
        {
            var length = wishes.Count;
            for (var firstCategory = 0; firstCategory < length; firstCategory++)
            {
                for (var secondCategory = firstCategory + 1; secondCategory < length; secondCategory++)
                {
                    for (var thirdCategory = secondCategory + 1; thirdCategory < length; thirdCategory++)
                    {
                        ForEveryCategoryTriple(wishes, firstCategory, secondCategory, thirdCategory);
                    }
                }
            }
        }

        private void ForEveryCategoryTriple(IReadOnlyList<WishCategory> wishes, int firstCategory, int secondCategory,
            int thirdCategory)
        {
            for (var firstWish = 0; firstWish < wishes[firstCategory].Wishes.Count; firstWish++)
            {
                for (var secondWish = 0; secondWish < wishes[secondCategory].Wishes.Count; secondWish++)
                {
                    for (var thirdWish = 0; thirdWish < wishes[thirdCategory].Wishes.Count; thirdWish++)
                    {
                        ForEveryWishTriple(wishes, 
                            firstCategory, firstWish, 
                            secondCategory, secondWish,
                            thirdCategory, thirdWish);
                    }
                }
            }
        }

        private void ForEveryWishTriple(IReadOnlyList<WishCategory> wishes,
            int firstCategory, int firstWish,
            int secondCategory, int secondWish,
            int thirdCategory, int thirdWish)
        {
            var collectionToAdd = OtherVariants;
            if (firstWish >= _wishesUsedPerCategory[firstCategory]
                && secondWish >= _wishesUsedPerCategory[secondCategory]
                && thirdWish >= _wishesUsedPerCategory[thirdCategory])
            {
                collectionToAdd = OptimalVariants;

                _wishesUsedPerCategory[firstCategory]++;
                _wishesUsedPerCategory[secondCategory]++;
                _wishesUsedPerCategory[thirdCategory]++;
            }

            var first = wishes[firstCategory].Wishes[firstWish];
            var second = wishes[secondCategory].Wishes[secondWish];
            var third = wishes[thirdCategory].Wishes[thirdWish];

            collectionToAdd.Add(new Triple(first, second, third));
        }
    }
}
