using System.Collections.Generic;
using CongratulationsGenerator.Core;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using IDataTable = CongratulationsGenerator.Core.IDataTable;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class ExcelDoc : IDataTable, IConfiguration
    {
        private readonly Application _app;
        private readonly Workbook _book;

        public ExcelDoc(string filename)
        {
            _app = new Application();
            _book = _app.Workbooks.Open(filename, null, true);
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private static IEnumerable<Recipient> ReadRecipients(Worksheet sheet)
        {
            var recipients = new List<Recipient>();
            var range = sheet.UsedRange;
            var rows = range.Rows.Count;
            
            // The first line is the header line. Starting from the second one.
            for (var i = 1; i < rows; i++)
            {
                string name = range.Cells[i, 0];
                string gender = range.Cells[i, 1];
                recipients.Add(new Recipient(name, gender));
            }

            return recipients;
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private static IEnumerable<WishCategory> ReadWishes(Worksheet sheet)
        {
            var wishes = new List<WishCategory>();
            var range = sheet.UsedRange;
            var rows = range.Rows.Count;
            var columns = range.Columns.Count;

            // Each column is a separate wishes category.
            for (var column = 0; column < columns; column++)
            {
                string name = sheet.Cells[0, column];
                var wishesInCategory = new HashSet<string>();

                // The first line is the header line. Starting from the second one.
                for (var row = 1; row < rows; row++)
                {
                    string wish = sheet.Cells[row, column];
                    wishesInCategory.Add(wish);
                }
                wishes.Add(new WishCategory(name, wishesInCategory));
            }

            return wishes;
        }
        
        public IEnumerable<Recipient> GetRecipients()
        {
            // TODO: Retrieve recipients sheet using its name.
            return ReadRecipients(_book.Worksheets[0]);
        }

        public IEnumerable<WishCategory> GetWishes()
        {
            // TODO: Retrieve wishes sheet using its name.
            return ReadWishes(_book.Worksheets[1]);
        }

        public void Close()
        {
            _app.Quit();
        }

        public string GetFont()
        {
            // TODO: Read font from config sheet.
            return "Consolas";
        }

        public string GetTemplatePath()
        {
            // TODO: Read template path from config sheet.
            return @"C:\Projects\CongratulationsGenerator\Resources\Congratulations letter template.dotx";
        }
    }
}
