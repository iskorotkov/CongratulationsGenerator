namespace CongratulationsGenerator.Core
{
    public interface IConfiguration
    {
        string GetFont();
        string GetTemplatePath();
        string Get(string param);
        bool Exists(string param);
        string TryToGet(string param);
    }
}
