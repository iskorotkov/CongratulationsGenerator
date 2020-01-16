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
            
            var generator = new Generator(officeFactory, distributorFactory, officeFactory);
            generator.Generate();
        }
    }
}
