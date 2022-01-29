using System.IO;
using System.Linq;

namespace NAS_Files_Sender.Models
{
    public class SourceFolder
    {
        public SourceFolder()
        {
            Completion = 0;
        }

        public DirectoryInfo? DirectoryInfo { get; set; }
        public int Completion { get; set; }
        public double? Size
        {
            get
            {
                if (DirectoryInfo != null)
                {
                    return DirectoryInfo.EnumerateFiles("*", SearchOption.AllDirectories)?.Sum(x => x.Length);
                }

                return 0;
            }
        }
    }
}
