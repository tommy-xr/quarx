using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Content.MultiModel
{
    /// <summary>
    /// Class for combining multiple models into a single vertex buffer, and atlasing the textures
    /// </summary>
    public class MultiModelCollection
    {
        string filename;

        //FxCop will be pissed..
        public MultiModelInfo[] ModelInfo;

        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }

    }

    public class MultiModelInfo
    {
        string name;
        Matrix transform;
        string importer;
        string processor;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Matrix Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        public string Importer
        {
            get { return importer; }
            set { importer = value; }
        }

        public string Processor
        {
            get { return processor; }
            set { processor = value; }
        }
    }
}
