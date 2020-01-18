using System;
using System.Threading.Tasks;
using System.Windows;

namespace CongratulationsGenerator.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string DefaultText = "Generate letters for your friends";
        private const string PleaseWaitText = "Please wait";

        public MainWindow()
        {
            InitializeComponent();
            UnlockButton();
        }

        private async void Generate(object sender, RoutedEventArgs e)
        {
            var generator = Startup.Startup.InitializeGenerator();
            LockButton();
            await Task.Run(() =>
            {
                try
                {
                    generator.Generate();
                    MessageBox.Show("Letters generation completed!", "Success", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Something bad happened!", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    throw;
                }
            });
            UnlockButton();
        }

        private void LockButton()
        {
            GenerateLettersButton.IsEnabled = false;
            GenerateLettersButton.Content = PleaseWaitText;
        }

        private void UnlockButton()
        {
            GenerateLettersButton.Content = DefaultText;
            GenerateLettersButton.IsEnabled = true;
        }
    }
}
