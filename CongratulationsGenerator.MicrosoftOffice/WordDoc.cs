using System.Collections.Generic;
using CongratulationsGenerator.Core;
using Microsoft.Office.Interop.Word;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class WordDoc : ITemplateDocument
    {
        private readonly string _filename;
        private readonly Application _app;
        private readonly Document _doc;

        private bool _shouldAddTemplateList;
        
        public WordDoc(string filename)
        {
            _app = new Application();
            _doc = _app.Documents.Add(filename);
            _filename = filename;
        }

        private void AddTemplateList()
        {
            _app.Selection.EndKey(WdUnits.wdStory);
            _app.Selection.InsertNewPage();
            _app.Selection.InsertFile(_filename, "", false, false, false);
        }

        public void AddRecipient(Recipient recipient, IEnumerable<string> wishes)
        {
            if (_shouldAddTemplateList)
            {
                AddTemplateList();
            }

            _shouldAddTemplateList = true;
            
            _doc.Bookmarks["Dear"].Range.Text = recipient.Gender.DearForm;
            _doc.Bookmarks["Name"].Range.Text = recipient.Name;

            var index = 1;
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
