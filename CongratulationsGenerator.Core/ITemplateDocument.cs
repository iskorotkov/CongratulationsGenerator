using System.Collections.Generic;

namespace CongratulationsGenerator.Core
{
    public interface ITemplateDocument
    {
        void CreateNewDoc();
        void AddRecipient(Recipient name, IEnumerable<string> wishes);
        void ApplyFont(string font);
        void SaveDoc();
        void CloseDoc();
        void CloseTemplate();
    }
}
