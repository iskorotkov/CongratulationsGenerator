using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class MicrosoftOfficeFactory : IDocumentsFactory, IConfigurationFactory
    {
        private static readonly string TableFilename = null;
        private static ExcelDoc _table = null;

        public IConfiguration GetConfiguration()
        {
            return _table ??= new ExcelDoc(TableFilename);
        }

        public IDataTable OpenDataTable()
        {
            return _table ??= new ExcelDoc(TableFilename);
        }

        public ITemplateDocument OpenTemplateDocument(string filename)
        {
            return new WordDoc(filename);
        }
    }
}
