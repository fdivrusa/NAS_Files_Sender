using System.IO;

namespace NAS_Files_Sender.Models
{
    public class SourceFolder
    {
        public FileInfo? FileInfo { get; set; }
        public int Completion { get; set; }
    }
}
