using System.Collections.Generic;
using System.Threading.Tasks;

namespace CongratulationsGenerator.Core
{
    public interface ITemplateDocument
    {
        Task AddRecipient(Recipient recipient, IEnumerable<string> wishes);
        Task ApplyFont(string font);
        void SaveDoc(string filename);
        void CloseDoc();
        void ShowDoc();
    }
}
