using System.Collections.Generic;
using CongratulationsGenerator.Core;
using Microsoft.Office.Interop.Word;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class WordDoc : ITemplateDocument
    {
        private Application _app;
        private Document _doc;
        
        public WordDoc(string filename)
        {
            _app = new Application();
            _doc = _app.Documents.Add(filename);
        }

        public void CreateNewDoc()
        {
            throw new System.NotImplementedException();
        }

        public void AddRecipient(Recipient name, IEnumerable<string> wishes)
        {
            throw new System.NotImplementedException();
        }

        public void ApplyFont(string font)
        {
            throw new System.NotImplementedException();
        }

        public void SaveDoc()
        {
            throw new System.NotImplementedException();
        }

        public void CloseDoc()
        {
            throw new System.NotImplementedException();
        }

        public void CloseTemplate()
        {
            throw new System.NotImplementedException();
        }
    }
}
