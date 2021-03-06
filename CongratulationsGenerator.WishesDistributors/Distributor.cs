﻿using System.Collections.Generic;
using System.Linq;
using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.WishesDistributors
{
    public class Distributor : IWishesDistributor
    {
        public static IPermutationGeneratorFactory PermutationGeneratorFactory;

        private readonly List<WishCategory> _wishCategories;
        private List<Triple> _optimalVariants;
        private List<Triple> _otherVariants;

        public Distributor(IEnumerable<WishCategory> wishCategories)
        {
            _wishCategories = wishCategories.ToList();
            ReverseSortByWishesCount();
        }

        public bool IsEnoughWishes(int recipients)
        {
            var variants = 0;
            var length = _wishCategories.Count;
            for (var first = 0; first < length; ++first)
            {
                for (var second = first + 1; second < length; ++second)
                {
                    for (var third = second + 1; third < length; ++third)
                    {
                        variants += _wishCategories[first].Wishes.Count
                                    * _wishCategories[second].Wishes.Count
                                    * _wishCategories[third].Wishes.Count;
                    }
                }
            }

            return variants >= recipients;
        }

        public IEnumerable<string> GetNextWishes()
        {
            if (_optimalVariants == null)
            {
                GenerateTriples();
            }

            var collectionToUse = SelectCollection();
            var result = collectionToUse.First();
            collectionToUse.Remove(result);
            return result.Wishes();
        }

        private List<Triple> SelectCollection()
        {
            if (_optimalVariants.Count > 0)
            {
                return _optimalVariants;
            }

            if (_otherVariants.Count > 0)
                return _otherVariants;

            throw new NotEnoughWishesException();
        }

        private void ReverseSortByWishesCount()
        {
            _wishCategories.Sort((e1, e2) => e2.Wishes.Count.CompareTo(e1.Wishes.Count));
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
