using System.Collections.Generic;
using CongratulationsGenerator.Core;
using NUnit.Framework;

namespace CongratulationsGenerator.WishesDistributors.Tests
{
    [TestFixture]
    public class EnoughWishes
    {
        [Test]
        public void UpToThreeCategories()
        {
            var distributor = new Distributor(CreateWishes(new[] {1}));
            Assert.IsTrue(distributor.IsEnoughWishes(0));
            Assert.IsFalse(distributor.IsEnoughWishes(1));
            
            distributor = new Distributor(CreateWishes(new[] {2, 2}));
            Assert.IsFalse(distributor.IsEnoughWishes(1));

            distributor = new Distributor(CreateWishes(new[] {2, 2, 1}));
            Assert.IsTrue(distributor.IsEnoughWishes(1));

            distributor = new Distributor(CreateWishes(new[] {2, 2, 1}));
            Assert.IsTrue(distributor.IsEnoughWishes(4));
            Assert.IsFalse(distributor.IsEnoughWishes(5));
        }

        [Test]
        public void MoreCategories()
        {
            var distributor = new Distributor(CreateWishes(new[] {2, 2, 2, 2}));
            Assert.IsTrue(distributor.IsEnoughWishes(0));
            Assert.IsTrue(distributor.IsEnoughWishes(32));
            Assert.IsFalse(distributor.IsEnoughWishes(33));
        }

        private static IEnumerable<WishCategory> CreateWishes(IEnumerable<int> wishesPerCategory)
        {
            var results = new List<WishCategory>();
            foreach (var n in wishesPerCategory)
            {
                var wishes = new List<string>(n);
                for (var i = 0; i < n; i++)
                {
                    wishes.Add("Wish");
                }
                
                results.Add(new WishCategory("Name", wishes));
            }

            return results;
        }
    }
}
