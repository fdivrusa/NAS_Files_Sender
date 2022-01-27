using NAS_Files_Sender.Models;
using System.Collections.ObjectModel;

namespace NAS_Files_Sender.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _destinationFolder;

        public MainViewModel() : base()
        {
            _destinationFolder = "No destination folder selected";
        }

        public string DestinationFolder
        {
            get { return _destinationFolder; }
            set
            {
                if (_destinationFolder != value)
                {
                    _destinationFolder = value;
                    NotifyPropertyChanged(nameof(DestinationFolder));
                }
            }
        }

        public ObservableCollection<SourceFolder> SourceFolders { get; } = new ObservableCollection<SourceFolder>();
    }
}
