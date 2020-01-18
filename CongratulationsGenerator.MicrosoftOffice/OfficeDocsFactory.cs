using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class OfficeDocsFactory : IDocumentsFactory
    {
        public OfficeDocsFactory(string tableFilename)
        {
            _tableFilename = tableFilename;
        }
        private readonly string _tableFilename;

        public IDataTable OpenDataTable()
        {
            return new ExcelReader(_tableFilename);
        }

        public ITemplateDocument OpenTemplateDocument(string filename, string celebration)
        {
            return new WordWriter(filename, celebration);
        }
    }
}
