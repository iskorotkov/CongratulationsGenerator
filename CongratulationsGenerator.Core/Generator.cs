﻿using System.IO;

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
            var template = _documentsFactory.OpenTemplateDocument(
                config.GetTemplatePath(),
                config.Get("Celebration name")
            );

            var recipients = table.GetRecipients();
            var wishes = table.GetWishes();

            table.Close();

            var distributor = _distributorFactory.CreateDistributor(wishes);

            // TODO: Check whether there are enough wishes.

            foreach (var recipient in recipients)
            {
                var recipientWishes = distributor.GetNextWishes();
                template.AddRecipient(recipient, recipientWishes);

                // TODO: Replace celebration name in text if needed.
            }

            template.ApplyFont(config.GetFont());
            template.ShowDoc();

            var filename = Path.Combine(config.Get("output path"), config.Get("Default file name"));
            template.SaveDoc(filename);
        }
    }
}
