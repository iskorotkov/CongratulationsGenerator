using System.Collections.Generic;
using System.IO;
using System.Linq;
using CongratulationsGenerator.Core;
using Microsoft.Office.Interop.Word;
using Task = System.Threading.Tasks.Task;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class WordWriter : ITemplateDocument
    {
        private readonly Application _app;
        private readonly string _celebration;
        private readonly Document _doc;
        private readonly string _filename;

        private bool _shouldAddTemplateList;

        public WordWriter(string filename, string celebration)
        {
            _app = new Application();
            _doc = _app.Documents.Add(filename);
            _filename = filename;
            _celebration = celebration;
        }

        public async Task AddRecipient(Recipient recipient, IEnumerable<string> wishes)
        {
            if (_shouldAddTemplateList)
            {
                AddTemplateList();
            }

            _shouldAddTemplateList = true;

            var tasks = new List<Task>
            {
                Task.Run(() => { _doc.Bookmarks["Dear"].Range.Text = recipient.Gender.DearForm; }),
                Task.Run(() => { _doc.Bookmarks["Name"].Range.Text = recipient.Name; }),
                Task.Run(() =>
                {
                    if (_celebration != null)
                    {
                        _doc.Bookmarks["Celebration"].Range.Text = _celebration;
                    }
                })
            };

            var index = 1;
            foreach (var wish in wishes)
            {
                var i = index;
                var w = wish;
                tasks.Add(Task.Run(() => { _doc.Bookmarks["Wish" + i].Range.Text = w; }));
                index++;
            }

            await Task.WhenAll(tasks);
        }

        public async Task ApplyFont(string font)
        {
            var paragraphsTask = Task.Run(() => _doc.Paragraphs);
            var shapesTask = Task.Run(() => _doc.Shapes);

            var paragraphs = await paragraphsTask;
            var tasks = (from Paragraph paragraph in paragraphs
                select Task.Run(() =>
                {
                    var p = paragraph;
                    p.Range.Font.Name = font;
                })).ToList();

            var shapes = await shapesTask;
            tasks.AddRange(from Shape shape in shapes
                select Task.Run(() =>
                {
                    var s = shape;
                    s.TextFrame.TextRange.Font.Name = font;
                }));

            await Task.WhenAll(tasks);
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

        private void AddTemplateList()
        {
            _app.Selection.EndKey(WdUnits.wdStory);
            _app.Selection.InsertNewPage();
            _app.Selection.InsertFile(_filename, "", false, false, false);
        }
    }
}
