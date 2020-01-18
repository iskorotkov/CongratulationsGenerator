using System.IO;
using System.Linq;

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
            var template = _documentsFactory.OpenTemplateDocument(
                config.GetTemplatePath(),
                config.Get("Celebration name")
            );
            
            // TODO: Make IO operations async or in another thread.
            
            var table = _documentsFactory.OpenDataTable();
            var recipients = table.GetRecipients().ToList();
            var wishes = table.GetWishes();
            table.Close();

            var distributor = _distributorFactory.CreateDistributor(wishes);
            if (distributor.IsEnoughWishes(recipients.Count))
            {
                foreach (var recipient in recipients)
                {
                    var recipientWishes = distributor.GetNextWishes();
                    template.AddRecipient(recipient, recipientWishes);
                }

                template.ApplyFont(config.GetFont());
                template.ShowDoc();

                var filename = Path.Combine(config.Get("output path"), config.Get("Default file name"));
                
                // TODO: Add config values for auto saving and auto closing.
                template.SaveDoc(filename);
                // template.CloseDoc();
            }
            else
            {
                template.CloseDoc();
                throw new NotEnoughWishesException();
            }
        }
    }
}
