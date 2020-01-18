namespace CongratulationsGenerator.Core
{
    public class Generator
    {
        private readonly IConfigurationFactory _configurationFactory;
        private readonly IDistributorFactory _distributorFactory;
        private readonly IDocumentsFactory _documentsFactory;

        public Generator(IDocumentsFactory documentsFactory, IDistributorFactory distributorFactory,
            IConfigurationFactory configurationFactory)
        {
            _documentsFactory = documentsFactory;
            _distributorFactory = distributorFactory;
            _configurationFactory = configurationFactory;
        }

        public void Generate()
        {
            var config = _configurationFactory.GetConfiguration();

            var table = _documentsFactory.OpenDataTable();
            var template = _documentsFactory.OpenTemplateDocument(config.GetTemplatePath());

            var recipients = table.GetRecipients();
            var wishes = table.GetWishes();
            
            // TODO: Close data table.

            var distributor = _distributorFactory.CreateDistributor(wishes);

            foreach (var recipient in recipients)
            {
                var recipientWishes = distributor.GetNextWishes();
                template.AddRecipient(recipient, recipientWishes);
            }

            template.ApplyFont(config.GetFont());
            template.SaveDoc();
            template.CloseDoc();
        }
    }
}
