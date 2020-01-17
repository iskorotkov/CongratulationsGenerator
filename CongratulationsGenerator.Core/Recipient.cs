namespace CongratulationsGenerator.Core
{
    public class Recipient
    {
        public Recipient(string name, string gender)
        {
            Name = name;
            Gender = Gender.Create(gender);
        }

        public Recipient(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
        }
        
        public string Name { get; }
        public Gender Gender { get; }
    }
}
