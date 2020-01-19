using System.Collections.Generic;

namespace CongratulationsGenerator.Core
{
    public interface IConfiguration
    {
        string Font { get; }
        string TemplatePath { get; }
        string OutputPath { get; }
        string DefaultFilename { get; }
        string CelebrationName { get; }
        Dictionary<string, string> Preferences { get; }
    }
}
