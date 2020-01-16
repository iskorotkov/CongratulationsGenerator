using System.Collections.Generic;
using CongratulationsGenerator.Core;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public class ExcelDoc : IDataTable, IConfiguration
    {
        public ExcelDoc(string filename)
        {
            
        }
        public IEnumerable<Recipient> GetRecipients()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<WishCategory> GetWishes()
        {
            throw new System.NotImplementedException();
        }

        public string GetFont()
        {
            throw new System.NotImplementedException();
        }

        public string GetTemplatePath()
        {
            throw new System.NotImplementedException();
        }
    }
}
