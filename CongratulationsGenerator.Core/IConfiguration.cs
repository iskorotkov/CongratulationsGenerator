namespace CongratulationsGenerator.Core
{
    public interface IConfiguration
    {
        string GetFont();
        string GetTemplatePath();
        string GetParameter(string param);
        bool HasParameter(string param);
    }
}
