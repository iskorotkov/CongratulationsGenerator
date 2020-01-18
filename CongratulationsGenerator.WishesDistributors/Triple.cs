using System.Collections.Generic;

namespace CongratulationsGenerator.WishesDistributors
{
    public struct Triple
    {
        public Triple(string first, string second, string third)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public string First { get; }
        public string Second { get; }
        public string Third { get; }

        public IEnumerable<string> Wishes()
        {
            return new List<string> {First, Second, Third};
        }
    }
}
