namespace CongratulationsGenerator.Core
{
    public interface IDocumentsFactory
    {
        IDataTable CreateDataTable();
        ITemplateDocument CreateTemplateDocument();
    }
}
