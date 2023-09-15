using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Sxe.Library.Utilities
{
    public static class FileUtilities
    {
        public static bool FileExists(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (fi.Exists)
                return true;

            return false;
        }

        /// <summary>
        /// Returns a whole path for an object, when you don't know its extension
        /// </summary>
        /// <param name="path">path to object</param>
        /// <param name="extensions">valid extensions</param>
        /// <returns></returns>
        public static string FindExtension(string path, string [] extensions)
        {
            foreach (string extension in extensions)
            {
                if(FileUtilities.FileExists(path + extension))
                    return path + extension;
            }

           throw new Exception("Could not find file: " + path);
        }
    }
}
