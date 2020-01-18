using System.Collections.Generic;

namespace CongratulationsGenerator.MicrosoftOffice
{
    public interface IConfigBackend
    {
        Dictionary<string, string> ReadPreferences();
        void Close();
    }
}
