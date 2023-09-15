using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SXE.Library.Services;

namespace SXE.Engine
{
    public class Log : ILogService
    {

        FileStream fs;
        StreamWriter sw;

        public Log(string filename)
        {
            fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs);
        }

        public void Print(string sz)
        {
            sw.WriteLine(sz);
            sw.Flush();
        }

        public void Close()
        {
            sw.Close();
            fs.Close();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
