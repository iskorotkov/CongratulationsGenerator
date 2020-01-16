using System.Collections.Generic;

namespace CongratulationsGenerator.Core
{
    public interface IDataTable
    {
        IEnumerable<Recipient> GetRecipients();
        IEnumerable<WishCategory> GetWishes();
    }
}
