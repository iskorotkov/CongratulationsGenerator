using System.Collections.Generic;
using CongratulationsGenerator.Core;
using Microsoft.Office.Interop.Word;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class WordDoc : ITemplateDocument
    {
        private readonly Application _app;
        private readonly Document _doc;
        
        public WordDoc(string filename)
        {
            _app = new Application();
            _doc = _app.Documents.Add(filename);
        }

        public void AddRecipient(Recipient recipient, IEnumerable<string> wishes)
        {
            _doc.Bookmarks["Dear"].Range.Text = recipient.Gender.DearForm;
            _doc.Bookmarks["Name"].Range.Text = recipient.Name;

            var index = 0;
            foreach (var wish in wishes)
            {
                _doc.Bookmarks["Wish" + index].Range.Text = wish;
                
                index++;
            }
        }

        public void ApplyFont(string font)
        {
            throw new System.NotImplementedException();
        }

        public void SaveDoc()
        {
            _doc.Save();
        }

        public void CloseDoc()
        {
            _app.Quit(false);
        }
    }
}
