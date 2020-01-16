namespace CongratulationsGenerator.Core
{
    public class Recipient
    {
        public Recipient(string name, string gender)
        {
            Name = name;
            Gender = GenderUtils.DetermineGender(gender);
        }
        
        public string Name { get; }
        public IGender Gender { get; }
    }
}
