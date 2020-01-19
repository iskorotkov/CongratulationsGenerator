using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public void Generate()
        {
            try
            {
                var (recipients, wishes) = PrepareData();
                _distributor = _distributorFactory.CreateDistributor(wishes);
                if (_distributor.IsEnoughWishes(recipients.Count))
                {
                    GenerateLettersText(recipients);
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
            _table.Close();
            _table = null;
            return (recipients, wishes);
        }

        private void GenerateLettersText(IEnumerable<Recipient> recipients)
        {
            _config = _configurationFactory.GetConfiguration();
            _template = _documentsFactory.OpenTemplateDocument(_config.TemplatePath, _config.CelebrationName);
            AddRecipients(recipients);
            _template.ApplyFont(_config.Font);
        }

        private void FinishGeneration()
        {
            // TODO: Add config values for auto saving and auto closing.
            var filename = Path.Combine(_config.OutputPath, _config.DefaultFilename);
            _template.SaveDoc(filename);
            _template.ShowDoc();
        }

        private void AddRecipients(IEnumerable<Recipient> recipients)
        {
            foreach (var recipient in recipients)
            {
                var recipientWishes = _distributor.GetNextWishes();
                _template.AddRecipient(recipient, recipientWishes);
            }
        }
    }
}
