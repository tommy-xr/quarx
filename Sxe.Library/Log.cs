using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Sxe.Library
{

    /// <summary>
    /// Class for logging data to a text file
    /// </summary>
    public class Log : IDisposable
    {
        FileStream fs;
        StreamWriter sw;

        public Log(string logName)
        {
            fs = new FileStream(logName, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs);
        }

        public void DebugPrint(string sz)
        {
#if DEBUG
            Print(sz);
#endif
        }

        public void Print(string sz)
        {
                sw.Write(sz + Environment.NewLine);
                sw.Flush();
        }

        public void Dispose()
        {
            sw.Close();
            fs.Close();

            sw.Dispose();
            fs.Dispose();
        }
    }
}
