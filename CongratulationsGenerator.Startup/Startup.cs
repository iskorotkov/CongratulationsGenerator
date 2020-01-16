namespace CongratulationsGenerator.Startup
{
    static class Startup
    {
        public static void Main()
        {
            Core.Generator.DocumentsFactory = new MicrosoftOffice.MicrosoftOfficeDocumentsFactory();
            Core.Generator.DistributorFactory = new WishesDistributors.DistributorFactory();

            Core.Generator.Generate();
        }
    }
}
