using System;
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
                var (recipients, wishes) = PrepareData();
                _distributor = _distributorFactory.CreateDistributor(wishes);
                if (_distributor.IsEnoughWishes(recipients.Count))
                {
                    await GenerateLettersText(recipients);
                    FinishGeneration();
                }
                else
                {
                    throw new NotEnoughWishesException();
                }
            }
            catch (Exception)
            {
                _template?.CloseDoc();
                _table?.Close();
                throw;
            }
        }

        private (List<Recipient> recipients, IEnumerable<WishCategory> wishes) PrepareData()
        {
            _table = _documentsFactory.OpenDataTable();
            var recipients = _table.GetRecipients().ToList();
            var wishes = _table.GetWishes();
            // TODO: Await?
            _table.Close();
            _table = null;
            return (recipients, wishes);
        }

        private async Task GenerateLettersText(IEnumerable<Recipient> recipients)
        {
            _config = _configurationFactory.GetConfiguration();
            _template = _documentsFactory.OpenTemplateDocument(_config.TemplatePath, _config.CelebrationName);
            await AddRecipients(recipients);
            await _template.ApplyFont(_config.Font);
        }

        private void FinishGeneration()
        {
            // TODO: Add config values for auto saving and auto closing.
            var filename = Path.Combine(_config.OutputPath, _config.DefaultFilename);
            // TODO: Await?
            _template.SaveDoc(filename);
            _template.ShowDoc();
        }

        private async Task AddRecipients(IEnumerable<Recipient> recipients)
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
        }
    }
}
