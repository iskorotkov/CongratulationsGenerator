using System.Collections.Generic;

namespace CongratulationsGenerator.Core
{
    public class WishCategory
    {
        public WishCategory(string name, List<string> wishes)
        {
            Name = name;
            Wishes = wishes;
        }

        public string Name { get; }
        public List<string> Wishes { get; }
    }
}
