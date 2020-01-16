namespace CongratulationsGenerator.Core
{
    public interface IDocumentsFactory
    {
        IDataTable OpenDataTable();
        ITemplateDocument OpenTemplateDocument(string filename);
    }
}
