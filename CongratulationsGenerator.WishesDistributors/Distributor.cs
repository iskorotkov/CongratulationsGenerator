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
            _wishCategories = wishCategories;
        }

        private readonly IEnumerable<WishCategory> _wishCategories;
        
        public bool IsEnoughWishes(int recipients)
        {
            var variants = _wishCategories.Aggregate(1, (current, wishCategory) => current * wishCategory.Wishes.Count);
            return variants >= recipients;
        }

        private struct Triple
        {
            public Triple(string x, string y, string z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public string X { get; }
            public string Y { get; }
            public string Z { get; }
        }

        public IEnumerable<string> GetNextWishes()
        {
            var optimalVariants = new HashSet<Triple>();
            var otherVariants = new HashSet<Triple>();

            foreach (var category in _wishCategories)
            {
                
            }

            for (var categoryIndex = 0; categoryIndex < _wishCategories.Count(); categoryIndex++)
            {
                
            }

            return null;
        }
    }
}
