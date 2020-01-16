using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class MicrosoftOfficeFactory : IDocumentsFactory, IConfigurationFactory
    {
        // TODO: Remove the absolute path to data table and use something flexible instead.
        private const string TableFilename = @"C:\Projects\CongratulationsGenerator\Resources\Data.xlsx";
        private static ExcelDoc _table;

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
