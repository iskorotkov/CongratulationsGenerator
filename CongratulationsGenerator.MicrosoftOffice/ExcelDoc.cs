using System;
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

            // Indexing in Excel sheet starts with 1.
            // The first line is the header line. Starting from the second one.
            for (var i = 2; i <= rows; i++)
            {
                string name = range.Cells[i, 1].Value.ToString();
                string gender = range.Cells[i, 2].Value.ToString();
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

            // Indexing in Excel sheet starts with 1.
            // Each column is a separate wishes category.
            for (var column = 1; column <= columns; column++)
            {
                string name = sheet.Cells[1, column].Value.ToString();
                var wishesInCategory = new HashSet<string>();

                // The first line is the header line. Starting from the second one.
                for (var row = 2; row <= rows; row++)
                {
                    string wish;
                    try
                    {
                        wish = sheet.Cells[row, column].Value.ToString();
                    }
                    catch (Exception)
                    {
                        // We have read all wishes in a category. We don't have to handle the exception.
                        // Let's move on reading the next category.
                        continue;
                    }

                    wishesInCategory.Add(wish);
                }

                wishes.Add(new WishCategory(name, wishesInCategory));
            }

            return wishes;
        }

        public IEnumerable<Recipient> GetRecipients()
        {
            // TODO: Move string to config file.
            return ReadRecipients(_book.Worksheets["Recipients"]);
        }

        public IEnumerable<WishCategory> GetWishes()
        {
            // TODO: Move string to config file.
            return ReadWishes(_book.Worksheets["Wishes"]);
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
            return @"C:\Projects\CongratulationsGenerator\Resources\Template.dotx";
        }
    }
}
