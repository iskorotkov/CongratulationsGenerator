using System.Collections.Generic;
using System.IO;
using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class OfficeConfig : IConfiguration
    {
        private readonly IConfigBackend _configDoc;
        private Dictionary<string, string> _preferences;

        public OfficeConfig(IConfigBackendFactory configDocFactory)
        {
            _configDoc = configDocFactory.OpenConfig();
        }

        public string GetFont()
        {
            return Get("font");
        }

        public string GetTemplatePath()
        {
            return Path.Combine(Get("resources path"), Get("template file"));
        }

        public string Get(string param)
        {
            if (_preferences == null)
            {
                ReadPreferences();
            }

            return _preferences[param.ToLower()];
        }

        public bool Exists(string param)
        {
            if (_preferences == null)
            {
                ReadPreferences();
            }

            return _preferences.ContainsKey(param.ToLower());
        }

        public string TryToGet(string param)
        {
            return Exists(param) ? Get(param) : null;
        }

        private void ReadPreferences()
        {
            _preferences = _configDoc.ReadPreferences();
            _configDoc.Close();
        }
    }
}
