namespace CongratulationsGenerator.Core
{
    public static class Generator
    {
        public static IDocumentsFactory DocumentsFactory { private get; set; }
        public static IDistributorFactory DistributorFactory { private get; set; }
        public static IConfigurationFactory ConfigurationFactory { private get; set; }

        public static void Generate()
        {
            var config = ConfigurationFactory.GetConfiguration();

            var table = DocumentsFactory.OpenDataTable();
            var template = DocumentsFactory.OpenTemplateDocument(config.GetTemplatePath());
            var distributor = DistributorFactory.CreateDistributor();

            var recipients = table.GetRecipients();
            var wishes = table.GetWishes();
            distributor.SetWishes(wishes);

            template.CreateNewDoc();
            foreach (var recipient in recipients)
            {
                var recipientWishes = distributor.GetNextWishes();
                template.AddRecipient(recipient, recipientWishes);
            }
            template.ApplyFont(config.GetFont());
            template.SaveDoc();
            template.CloseDoc();
            template.CloseTemplate();
        }
    }
}
