using System.Collections.Generic;

namespace CongratulationsGenerator.Core
{
    public interface ITemplateDocument
    {
        void AddRecipient(Recipient recipient, IEnumerable<string> wishes);
        void ApplyFont(string font);
        void SaveDoc();
        void CloseDoc();
        void ShowDoc();
    }
}
