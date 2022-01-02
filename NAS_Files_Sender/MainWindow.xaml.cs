using NAS_Files_Sender.ViewModels;
using System;
using System.Windows;
using System.Windows.Forms;

namespace NAS_Files_Sender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel mainViewModel = new();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainViewModel;
        }

        private void Btn_Select_DestinationFolder_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new FolderBrowserDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ShowNewFolderButton = true,
            };
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(dialog.SelectedPath);
                mainViewModel.DestinationFolder = dialog.SelectedPath;
            }
        }
    }
}
