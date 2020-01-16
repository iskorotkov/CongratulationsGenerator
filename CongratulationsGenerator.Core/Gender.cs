namespace CongratulationsGenerator.Core
{
    public enum Gender
    {
        Male,
        Female,
        Other,
    }
    
    public static class GenderUtils
    {
        public static Gender DetermineGender(string gender)
        {
            if (string.IsNullOrEmpty(gender))
            {
                return Gender.Other;
            }

            gender = gender.ToLower();
            return gender[0] switch
            {
                'm' => Gender.Male,
                'м' => Gender.Male,
                'f' => Gender.Female,
                'ж' => Gender.Female,
                _ => Gender.Other
            };
        }
    }
}
