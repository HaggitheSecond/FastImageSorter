using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastImageSorter.UI.Helpers
{
    public static class DirectoryHelper
    {
        public static IEnumerable<string> GetImageFiles(string path)
        {
            return Directory.EnumerateFiles(path)
                        .Where(file => file.ToLower().EndsWith("jpg")
                                    || file.ToLower().EndsWith("jpeg")
                                    || file.ToLower().EndsWith(".png"));
        }
    }
}
