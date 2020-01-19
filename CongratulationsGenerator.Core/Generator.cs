using System.Collections.Generic;
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

        private IConfiguration _config;
        private IWishesDistributor _distributor;
        private IDataTable _table;
        private ITemplateDocument _template;
        
        private readonly List<Task> _cleanupTasks = new List<Task>();

        public Generator(IDocumentsFactory documentsFactory, IDistributorFactory distributorFactory,
            IConfigurationFactory configurationFactory)
        {
            _documentsFactory = documentsFactory;
            _distributorFactory = distributorFactory;
            _configurationFactory = configurationFactory;
        }

        public async Task Generate()
        {
            try
            {
                await PrepareForGeneration();
                var recipients = _table.GetRecipients().ToList();
                var wishes = _table.GetWishes();
                _cleanupTasks.Add(Task.Run(_table.Close));

                _distributor = _distributorFactory.CreateDistributor(wishes);
                if (_distributor.IsEnoughWishes(recipients.Count))
                {
                    await GenerateLettersText(recipients);
                    await Task.WhenAll(_cleanupTasks);
                }
                else
                {
                    _cleanupTasks.Add(Task.Run(_template.CloseDoc));
                    throw new NotEnoughWishesException();
                }
            }
            finally
            {
                await Task.WhenAll(_cleanupTasks);
            }
        }

        private async Task GenerateLettersText(IEnumerable<Recipient> recipients)
        {
            Task addRecipientTask = null;
            foreach (var recipient in recipients)
            {
                var recipientWishes = _distributor.GetNextWishes();
                if (addRecipientTask != null)
                {
                    await addRecipientTask;
                }

                addRecipientTask = _template.AddRecipient(recipient, recipientWishes);
            }

            if (addRecipientTask != null)
            {
                await addRecipientTask;
            }

            await _template.ApplyFont(_config.Font);

            // TODO: Add config values for auto saving and auto closing.

            var filename = Path.Combine(_config.OutputPath, _config.DefaultFilename);
            _cleanupTasks.Add(Task.Run(() => _template.SaveDoc(filename)));
            _cleanupTasks.Add(Task.Run(_template.ShowDoc));
        }

        private async Task PrepareForGeneration()
        {
            var configTask = Task.Run(_configurationFactory.GetConfiguration);
            var tableTask = Task.Run(_documentsFactory.OpenDataTable);

            _config = await configTask;
            _template = _documentsFactory.OpenTemplateDocument(
                _config.TemplatePath,
                _config.CelebrationName
            );

            _table = await tableTask;
        }
    }
}
