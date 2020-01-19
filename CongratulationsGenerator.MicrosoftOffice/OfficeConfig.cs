using System.Collections.Generic;
using System.IO;
using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class OfficeConfig : IConfiguration
    {
        public OfficeConfig(IConfigBackendFactory configDocFactory)
        {
            IConfigBackend configDoc = null;
            try
            {
                configDoc = configDocFactory.OpenConfig();
                PreprocessPreferences(configDoc.ReadPreferences());
            }
            finally
            {
                configDoc?.Close();
            }
        }

        public string Font => Preferences["font"];
        public string TemplatePath => Path.Combine(Preferences["resources path"], Preferences["template file"]);
        public string OutputPath => Preferences["output path"];
        public string DefaultFilename => Preferences["default file name"];
        public string CelebrationName => Preferences["celebration name"];
        public Dictionary<string, string> Preferences { get; } = new Dictionary<string, string>();

        private void PreprocessPreferences(Dictionary<string, string> preferences)
        {
            foreach (var value in preferences)
            {
                Preferences.Add(value.Key.ToLower(), value.Value);
            }
        }
    }
}
