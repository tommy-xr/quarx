using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Sxe.Library.Services;

namespace Sxe.Engine
{
    public class Log : ILogService, IDisposable
    {

        FileStream fs;
        StreamWriter sw;

        public Log(string fileName)
        {
            fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs);
        }

        public void Print(string text)
        {
            sw.WriteLine(text);
            sw.Flush();
        }

        public void Close()
        {
            sw.Close();
            fs.Close();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool value)
        {
            if (value)
            {
                Close();
            }
        }
    }
}
