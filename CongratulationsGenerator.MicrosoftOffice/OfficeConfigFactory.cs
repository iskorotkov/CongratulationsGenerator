using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class OfficeConfigFactory : IConfigurationFactory
    {
        private readonly IConfigBackendFactory _configDocFactory;

        public OfficeConfigFactory(IConfigBackendFactory configDocFactory)
        {
            _configDocFactory = configDocFactory;
        }

        public IConfiguration GetConfiguration()
        {
            return new OfficeConfig(_configDocFactory);
        }
    }
}
