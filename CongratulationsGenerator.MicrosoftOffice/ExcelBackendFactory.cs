namespace CongratulationsGenerator.MicrosoftOffice
{
    public class ExcelBackendFactory : IConfigBackendFactory
    {
        private readonly string _filename;

        public ExcelBackendFactory(string filename)
        {
            _filename = filename;
        }

        public IConfigBackend OpenConfig()
        {
            return new ExcelReader(_filename);
        }
    }
}
