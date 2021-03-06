﻿using System;
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
            LockButton();

            try
            {
                var generator = Startup.Startup.InitializeGenerator();
                await Task.Run(generator.Generate);
                MessageBox.Show("Letters generation completed!", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Something bad happened!", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

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
