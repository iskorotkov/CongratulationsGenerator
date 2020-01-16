using System.Collections.Generic;

namespace CongratulationsGenerator.Core
{
    public interface ITemplateDocument
    {
        void CreateNewDoc();
        void AddRecipient(Recipient name, IEnumerable<string> wishes);
        void SaveDoc();
        void CloseDoc();
    }
}
