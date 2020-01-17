using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CongratulationsGenerator.Core
{
    public class Gender
    {
        private static readonly List<Gender> Genders = new List<Gender>();

        private readonly string _pattern;

        public Gender(string pattern, string dearForm)
        {
            _pattern = pattern;
            DearForm = dearForm;
        }

        public string DearForm { get; }

        public static void Register(Gender gender)
        {
            Genders.Add(gender);
        }

        public static Gender Create(string gender)
        {
            return Genders.FirstOrDefault(possibleGender => possibleGender.Matched(gender));
        }

        private bool Matched(string gender)
        {
            return Regex.IsMatch(gender, _pattern);
        }
    }
}
