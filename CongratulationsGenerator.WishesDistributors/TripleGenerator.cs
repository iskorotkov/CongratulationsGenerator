using System.Collections.Generic;

namespace CongratulationsGenerator.WishesDistributors
{
    public class TripleGenerator
    {
        public List<Distributor.Triple> OptimalVariants { get; } = new List<Distributor.Triple>();
        public List<Distributor.Triple> OtherVariants { get; } = new List<Distributor.Triple>();
        
        public void GenerateTriples(IReadOnlyList<List<string>> wishes)
        {
            // TODO: Fix optimal triples generation.
            
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

        private void ForEveryCategoryTriple(IReadOnlyList<List<string>> wishes, int firstCategory, int secondCategory,
            int thirdCategory)
        {
            for (var firstWish = 0; firstWish < wishes[firstCategory].Count; firstWish++)
            {
                for (var secondWish = 0; secondWish < wishes[secondCategory].Count; secondWish++)
                {
                    for (var thirdWish = 0; thirdWish < wishes[thirdCategory].Count; thirdWish++)
                    {
                        ForEveryWishTriple(wishes, firstCategory, firstWish, secondCategory, secondWish,
                            thirdCategory,
                            thirdWish);
                    }
                }
            }
        }

        private void ForEveryWishTriple(IReadOnlyList<List<string>> wishes,
            int firstCategory, int firstWish,
            int secondCategory, int secondWish,
            int thirdCategory, int thirdWish)
        {
            var collectionToAdd = firstWish == secondWish && secondWish == thirdWish
                ? OptimalVariants
                : OtherVariants;

            var first = wishes[firstCategory][firstWish];
            var second = wishes[secondCategory][secondWish];
            var third = wishes[thirdCategory][thirdWish];
            AddTriple(collectionToAdd, first, second, third);
        }

        private static void AddTriple(ICollection<Distributor.Triple> collectionToAdd, string first, string second, string third)
        {
            collectionToAdd.Add(new Distributor.Triple(first, second, third));
        }
    }
}
