namespace CongratulationsGenerator.Core
{
    public interface IGender
    {
        string DearForm();
    }

    namespace Genders
    {
        public class Male : IGender
        {
            public string DearForm() => "Дорогой";
        }

        public class Female : IGender
        {
            public string DearForm() => "Дорогая";
        }

        public class Other : IGender
        {
            public string DearForm() => "Дорогой(ая)";
        }
    }
    
    public static class GenderUtils
    {
        public static IGender DetermineGender(string gender)
        {
            if (string.IsNullOrEmpty(gender))
            {
                return new Genders.Other();
            }

            gender = gender.ToLower();
            return gender[0] switch
            {
                'm' => new Genders.Male(),
                'м' => new Genders.Male(),
                'f' => new Genders.Female(),
                'ж' => new Genders.Female(),
                _ => new Genders.Other()
            };
        }
    }
}
