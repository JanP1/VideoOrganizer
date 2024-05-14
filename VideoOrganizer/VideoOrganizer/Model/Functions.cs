using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoOrganizer.Model
{
    class Functions
    {
        public List<string> GetMediaFiles(string directoryPath)
        {
            List<string> mp3mp4Files = new List<string>();

            // Get files in the directory with .mp3 extension
            string[] mp3Files = Directory.GetFiles(directoryPath, "*.mp3");
            mp3mp4Files.AddRange(mp3Files);

            // Get files in the directory with .mp4 extension
            string[] mp4Files = Directory.GetFiles(directoryPath, "*.mp4");
            mp3mp4Files.AddRange(mp4Files);

            return mp3mp4Files;
        }
    }
}
