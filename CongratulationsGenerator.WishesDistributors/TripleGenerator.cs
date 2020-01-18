using System.Collections.Generic;
using System.Linq;
using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.WishesDistributors
{
    public class TripleGenerator
    {
        private IReadOnlyList<WishCategory> _wishes;
        private List<int> _usedWishesPerCategory;
        public List<Triple> OptimalVariants { get; private set; } = new List<Triple>();
        public List<Triple> OtherVariants { get; private set; } = new List<Triple>();

        public void GenerateTriples(IReadOnlyList<WishCategory> wishes, IPermutationGenerator permutationGenerator)
        {
            _wishes = wishes;
            InitializeUsedWishesList();
            IterateOverCategories();
            OptimalVariants = permutationGenerator.MakePermutation(OptimalVariants).ToList();
            OtherVariants = permutationGenerator.MakePermutation(OtherVariants).ToList();
        }

        private void InitializeUsedWishesList()
        {
            _usedWishesPerCategory = new List<int>(_wishes.Count);
            for (var i = 0; i < _wishes.Count; i++)
            {
                _usedWishesPerCategory.Add(0);
            }
        }

        private void IterateOverCategories()
        {
            var length = _wishes.Count;
            for (var firstCategory = 0; firstCategory < length; firstCategory++)
            {
                for (var secondCategory = firstCategory + 1; secondCategory < length; secondCategory++)
                {
                    for (var thirdCategory = secondCategory + 1; thirdCategory < length; thirdCategory++)
                    {
                        ForEveryCategoryTriple(firstCategory, secondCategory, thirdCategory);
                    }
                }
            }
        }

        private void ForEveryCategoryTriple(int firstCategory, int secondCategory, int thirdCategory)
        {
            for (var firstWish = 0; firstWish < Wishes(firstCategory).Count; firstWish++)
            {
                for (var secondWish = 0; secondWish < Wishes(secondCategory).Count; secondWish++)
                {
                    for (var thirdWish = 0; thirdWish < Wishes(thirdCategory).Count; thirdWish++)
                    {
                        ForEveryWishTriple(
                            firstCategory, firstWish,
                            secondCategory, secondWish,
                            thirdCategory, thirdWish
                        );
                    }
                }
            }
        }

        private List<string> Wishes(int category)
        {
            return _wishes[category].Wishes;
        }

        private void ForEveryWishTriple(
            int firstCategory, int firstWish,
            int secondCategory, int secondWish,
            int thirdCategory, int thirdWish
        )
        {
            var collectionToAdd = CollectionToAdd(
                firstCategory, firstWish,
                secondCategory, secondWish,
                thirdCategory, thirdWish
            );

            var first = Wish(firstCategory, firstWish);
            var second = Wish(secondCategory, secondWish);
            var third = Wish(thirdCategory, thirdWish);

            collectionToAdd.Add(new Triple(first, second, third));
        }

        private List<Triple> CollectionToAdd(
            int firstCategory, int firstWish,
            int secondCategory, int secondWish,
            int thirdCategory, int thirdWish
        )
        {
            if (!AreWishesAvailable(firstCategory, firstWish, secondCategory, secondWish, thirdCategory, thirdWish))
            {
                return OtherVariants;
            }

            MarkWishesAsTaken(firstCategory, secondCategory, thirdCategory);
            return OptimalVariants;
        }

        private string Wish(int category, int wish)
        {
            return _wishes[category].Wishes[wish];
        }

        private void MarkWishesAsTaken(int firstCategory, int secondCategory, int thirdCategory)
        {
            _usedWishesPerCategory[firstCategory]++;
            _usedWishesPerCategory[secondCategory]++;
            _usedWishesPerCategory[thirdCategory]++;
        }

        private bool AreWishesAvailable(
            int firstCategory, int firstWish,
            int secondCategory, int secondWish,
            int thirdCategory, int thirdWish
        )
        {
            return firstWish >= _usedWishesPerCategory[firstCategory]
                   && secondWish >= _usedWishesPerCategory[secondCategory]
                   && thirdWish >= _usedWishesPerCategory[thirdCategory];
        }
    }
}
