using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class MicrosoftOfficeFactory : IDocumentsFactory, IConfigurationFactory
    {
        public MicrosoftOfficeFactory(string tableFilename)
        {
            _tableFilename = tableFilename;
        }
        private readonly string _tableFilename;
        private static ExcelDoc _table;

        public IConfiguration GetConfiguration()
        {
            return _table ??= new ExcelDoc(_tableFilename);
        }

        public IDataTable OpenDataTable()
        {
            return _table ??= new ExcelDoc(_tableFilename);
        }

        public ITemplateDocument OpenTemplateDocument(string filename)
        {
            return new WordDoc(filename);
        }
    }
}
