using System;
using CongratulationsGenerator.Core;
using CongratulationsGenerator.Permutations;
using CongratulationsGenerator.WishesDistributors;
using CongratulationsGenerator.MicrosoftOffice;

namespace CongratulationsGenerator.Startup
{
    public static class Startup
    {
        public static void Main()
        {
            var generator = InitializeGenerator();
            try
            {
                generator.Generate();
                Console.WriteLine("Letters generation completed!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something bad happened!\n\n{e.Message}");
            }
        }

        public static Generator InitializeGenerator()
        {
            const string filename = @"C:\Projects\CongratulationsGenerator\Resources\Data.xlsx";

            var officeFactory = new OfficeDocsFactory(filename);
            var distributorFactory = new DistributorFactory();
            var configBackendFactory = new ExcelBackendFactory(filename);
            var configFactory = new OfficeConfigFactory(configBackendFactory);

            Gender.Register(new Gender(@"^[мМmM].*", "Дорогой"));
            Gender.Register(new Gender(@"^[жЖwW].*", "Дорогая"));
            Gender.Register(new Gender(@"", "Дорогой(ая)"));

            Distributor.PermutationGeneratorFactory = new RandomInserterFactory();

            var generator = new Generator(officeFactory, distributorFactory, configFactory);
            return generator;
        }
    }
}
