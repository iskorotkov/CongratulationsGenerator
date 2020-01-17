using CongratulationsGenerator.Core;
using CongratulationsGenerator.WishesDistributors;

namespace CongratulationsGenerator.Startup
{
    static class Startup
    {
        public static void Main()
        {
            var officeFactory = new MicrosoftOffice.MicrosoftOfficeFactory();
            var distributorFactory = new DistributorFactory();
            
            Gender.Add(new Gender(@"^[мМmM].*", "Дорогой"));
            Gender.Add(new Gender(@"^[жЖwW].*", "Дорогая"));
            Gender.Add(new Gender(@"", "Дорогой(ая)"));

            var generator = new Generator(officeFactory, distributorFactory, officeFactory);
            generator.Generate();
        }
    }
}
