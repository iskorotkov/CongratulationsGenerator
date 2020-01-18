using System.Collections.Generic;
using static CongratulationsGenerator.WishesDistributors.Distributor;

namespace CongratulationsGenerator.WishesDistributors
{
    public class TripleGenerator
    {
        private List<int> _wishesUsedPerCategory;
        public List<Triple> OptimalVariants { get; } = new List<Triple>();
        public List<Triple> OtherVariants { get; } = new List<Triple>();

        public void GenerateTriples(IReadOnlyList<List<string>> wishes)
        {
            var length = wishes.Count;

            _wishesUsedPerCategory = new List<int>(length);
            for (var i = 0; i < length; i++)
            {
                _wishesUsedPerCategory.Add(0);
            }

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

        private void ForEveryCategoryTriple(IReadOnlyList<List<string>> wishes, int firstCategory, int secondCategory,
            int thirdCategory)
        {
            for (var firstWish = 0; firstWish < wishes[firstCategory].Count; firstWish++)
            {
                for (var secondWish = 0; secondWish < wishes[secondCategory].Count; secondWish++)
                {
                    for (var thirdWish = 0; thirdWish < wishes[thirdCategory].Count; thirdWish++)
                    {
                        ForEveryWishTriple(wishes, 
                            firstCategory, firstWish, 
                            secondCategory, secondWish,
                            thirdCategory, thirdWish);
                    }
                }
            }
        }

        private void ForEveryWishTriple(IReadOnlyList<List<string>> wishes,
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

            var first = wishes[firstCategory][firstWish];
            var second = wishes[secondCategory][secondWish];
            var third = wishes[thirdCategory][thirdWish];

            collectionToAdd.Add(new Triple(first, second, third));
        }
    }
}
