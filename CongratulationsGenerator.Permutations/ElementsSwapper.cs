using System;
using System.Collections.Generic;
using System.Linq;
using CongratulationsGenerator.WishesDistributors;

namespace CongratulationsGenerator.Permutations
{
    public class ElementsSwapper : IPermutationGenerator
    {
        public IEnumerable<T> MakePermutation<T>(IEnumerable<T> original)
        {
            var items = original.ToList();
            var permutations = items.Count;

            var random = new Random();
            for (var i = 0; i < permutations; i++)
            {
                var from = random.Next(items.Count);
                var to = random.Next(items.Count);

                var temp = items[from];
                items[from] = items[to];
                items[to] = temp;
            }

            return items;
        }
    }
}
