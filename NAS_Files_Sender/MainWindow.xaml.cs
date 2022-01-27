using NAS_Files_Sender.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace NAS_Files_Sender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel mainViewModel;

        public MainWindow()
        {
            mainViewModel = new MainViewModel();
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
                mainViewModel.DestinationFolder = dialog.SelectedPath;
            }
        }

        private void Btn_Add_SourceFolder_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new FolderBrowserDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ShowNewFolderButton = true,
            };
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                mainViewModel.SourceFolders.Add(new FileInfo(dialog.SelectedPath));
            }
        }
    }
}
