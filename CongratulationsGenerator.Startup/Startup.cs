using CongratulationsGenerator.Core;
using CongratulationsGenerator.Permutations;
using CongratulationsGenerator.WishesDistributors;

namespace CongratulationsGenerator.Startup
{
    static class Startup
    {
        public static void Main()
        {
            var officeFactory = new MicrosoftOffice.MicrosoftOfficeFactory();
            var distributorFactory = new DistributorFactory();
            
            Gender.Register(new Gender(@"^[мМmM].*", "Дорогой"));
            Gender.Register(new Gender(@"^[жЖwW].*", "Дорогая"));
            Gender.Register(new Gender(@"", "Дорогой(ая)"));
            
            Distributor.PermutationGeneratorFactory = new RandomInserterFactory();

            var generator = new Generator(officeFactory, distributorFactory, officeFactory);
            generator.Generate();
        }
    }
}
