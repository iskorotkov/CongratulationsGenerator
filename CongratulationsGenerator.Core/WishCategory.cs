using System.Collections.Generic;
using System.Linq;

namespace CongratulationsGenerator.Core
{
    public class WishCategory
    {
        public WishCategory(string name, IEnumerable<string> wishes)
        {
            Name = name;
            Wishes = wishes.ToList();
        }

        public string Name { get; }
        public List<string> Wishes { get; }
    }
}
