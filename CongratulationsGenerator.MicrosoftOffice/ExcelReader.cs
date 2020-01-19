using System;
using System.Collections.Generic;
using CongratulationsGenerator.Core;
using Microsoft.Office.Interop.Excel;
using IDataTable = CongratulationsGenerator.Core.IDataTable;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class ExcelReader : IDataTable, IConfigBackend
    {
        private readonly Application _app;
        private readonly Workbook _book;

        public ExcelReader(string filename)
        {
            _app = new Application();
            try
            {
                _book = _app.Workbooks.Open(filename, null, true);
            }
            catch (Exception)
            {
                _app.Quit();
                GC.Collect();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
                throw;
            }
        }

        public Dictionary<string, string> ReadPreferences()
        {
            var preferences = new Dictionary<string, string>();
            var sheet = _book.Sheets["Preferences"];
            var rows = sheet.UsedRange.Rows.Count;

            for (var row = 1; row <= rows; row++)
            {
                var name = sheet.Cells[row, 1].Value.ToString();
                var value = sheet.Cells[row, 2].Value.ToString();
                preferences.Add(name.ToLower(), value);
            }

            return preferences;
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
            _book.Close(false);
            _app.Quit();
            GC.Collect();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_book);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
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
                string name;
                try
                {
                    name = sheet.Cells[1, column].Value.ToString();
                }
                catch (Exception)
                {
                    // Probably the column is missing. Let's proceed to the next one.
                    continue;
                }

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
    }
}
