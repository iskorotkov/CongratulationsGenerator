using System;
using System.Collections.Generic;
using System.Linq;
using CongratulationsGenerator.WishesDistributors;

namespace CongratulationsGenerator.Permutations
{
    public class RandomInserter : IPermutationGenerator
    {
        public IEnumerable<T> MakePermutation<T>(IEnumerable<T> original)
        {
            var list = original.ToList();
            var results = new List<T>();
            var random = new Random();
            
            while (list.Count > 0)
            {
                var index = random.Next(list.Count);
                results.Add(list[index]);
                list.RemoveAt(index);
            }

            return results;
        }
    }
}
