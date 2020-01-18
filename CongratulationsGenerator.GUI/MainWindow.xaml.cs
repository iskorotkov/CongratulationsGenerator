using System.Threading.Tasks;

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

        private async void Generate(object sender, System.Windows.RoutedEventArgs e)
        {
            var generator = Startup.Startup.InitializeGenerator();
            LockButton();
            await Task.Run(generator.Generate);
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
