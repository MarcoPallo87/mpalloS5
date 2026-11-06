using System;
using System.Collections.Generic;
using System.Text;

namespace mpalloS5.Utils
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath (string filename)
        {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

        }
    }
}
