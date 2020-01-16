namespace CongratulationsGenerator.Startup
{
    static class Startup
    {
        public static void Main()
        {
            var officeFactory = new MicrosoftOffice.MicrosoftOfficeFactory();
            Core.Generator.DocumentsFactory = officeFactory;
            Core.Generator.ConfigurationFactory = officeFactory;
            Core.Generator.DistributorFactory = new WishesDistributors.DistributorFactory();

            Core.Generator.Generate();
        }
    }
}
