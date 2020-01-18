using CongratulationsGenerator.Core;
using CongratulationsGenerator.Permutations;
using CongratulationsGenerator.WishesDistributors;
using CongratulationsGenerator.MicrosoftOffice;

namespace CongratulationsGenerator.Startup
{
    static class Startup
    {
        public static void Main()
        {
            const string config = @"C:\Projects\CongratulationsGenerator\Resources\Data.xlsx";
            
            var officeFactory = new OfficeDocsFactory(config);
            var distributorFactory = new DistributorFactory();
            var configBackendFactory = new ExcelBackendFactory(config);
            var configFactory = new OfficeConfigFactory(configBackendFactory);

            Gender.Register(new Gender(@"^[мМmM].*", "Дорогой"));
            Gender.Register(new Gender(@"^[жЖwW].*", "Дорогая"));
            Gender.Register(new Gender(@"", "Дорогой(ая)"));

            Distributor.PermutationGeneratorFactory = new RandomInserterFactory();

            var generator = new Generator(officeFactory, distributorFactory, configFactory);
            generator.Generate();
        }
    }
}
