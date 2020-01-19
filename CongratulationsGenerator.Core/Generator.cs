using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task Generate()
        {
            var configTask = Task.Run(_configurationFactory.GetConfiguration);
            var tableTask = Task.Run(_documentsFactory.OpenDataTable);

            var config = await configTask;
            var template = _documentsFactory.OpenTemplateDocument(
                config.GetTemplatePath(),
                config.Get("Celebration name")
            );

            var table = await tableTask;
            var recipients = table.GetRecipients().ToList();
            var wishes = table.GetWishes();
            var closeTableTask = Task.Run(table.Close);
            
            var distributor = _distributorFactory.CreateDistributor(wishes);
            if (distributor.IsEnoughWishes(recipients.Count))
            {
                Task addRecipientTask = null;
                foreach (var recipient in recipients)
                {
                    var recipientWishes = distributor.GetNextWishes();
                    if (addRecipientTask != null)
                    {
                        await addRecipientTask;
                    }
                    addRecipientTask = template.AddRecipient(recipient, recipientWishes);
                }

                if (addRecipientTask != null)
                {
                    await addRecipientTask;
                }

                await template.ApplyFont(config.GetFont());
                template.ShowDoc();

                // TODO: Add config values for auto saving and auto closing.

                var filename = Path.Combine(config.Get("output path"), config.Get("Default file name"));
                template.SaveDoc(filename);
                
                await closeTableTask;
            }
            else
            {
                var closeDocTask = Task.Run(template.CloseDoc);
                await Task.WhenAll(closeTableTask, closeDocTask);
                throw new NotEnoughWishesException();
            }
        }
    }
}
