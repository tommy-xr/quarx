using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework.Storage;

namespace Sxe.Library.Utilities
{
    public static class FileSystem
    {

        public static FileStream OpenFile(string name, FileMode mode, FileAccess access)
        {
#if XBOX
            name = StorageContainer.TitleLocation + Path.DirectorySeparatorChar+ name;
            if (access != FileAccess.Read)
                access = FileAccess.Read;
                //throw new IOException("Can only read files on Xbox!");
#endif
            FileStream fs = new FileStream(name, mode, access);
            return fs;
        }
    }
}
