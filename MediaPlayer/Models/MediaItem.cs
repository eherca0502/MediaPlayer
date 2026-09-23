using System.IO;

namespace MediaPlayer.Models
{
    public class MediaItem
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public MediaItem(string filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}