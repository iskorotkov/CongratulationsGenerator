using System.Collections.Generic;
using System.IO;
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
            
            // TODO: Set celebration from passed value.

            var index = 1;
            foreach (var wish in wishes)
            {
                _doc.Bookmarks["Wish" + index].Range.Text = wish;
                index++;
            }
        }

        public void ApplyFont(string font)
        {
            foreach (Paragraph paragraph in _doc.Paragraphs)
            {
                paragraph.Range.Font.Name = font;
            }

            foreach (Shape shape in _doc.Shapes)
            {
                shape.TextFrame.TextRange.Font.Name = font;
            }
        }

        public void SaveDoc(string filename)
        {
            const string extension = "docx";
            if (!File.Exists(Path.ChangeExtension(filename, extension)))
            {
                _doc.SaveAs(filename);
                return;
            }

            for (var i = 1; i < 1000; i++)
            {
                var path = filename + i;
                if (File.Exists(Path.ChangeExtension(path, extension))) continue;
                _doc.SaveAs(path);
                return;
            }
            
            throw new OutputFileSavingException();
        }

        public void CloseDoc()
        {
            _doc.Close(false);
            _app.Quit(false);
        }

        public void ShowDoc()
        {
            _app.Visible = true;
        }
    }
}
