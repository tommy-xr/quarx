using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Library.Utilities;

namespace WinFormsContentLoading
{
    /// <summary>
    /// Class that manages settings specific to the editor
    /// </summary>
    public class EditorSettings
    {
        string contentProjectPath;
        public string ContentProjectPath
        {
            get { return contentProjectPath; }
            set { contentProjectPath = value; }
        }

        string contentDirectoryPath;
        public string ContentDirectoryPath
        {
            get { return contentDirectoryPath; }
            set { contentDirectoryPath = value; }
        }

        string gameExecutablePath;
        public string GameExecutablePath
        {
            get { return gameExecutablePath; }
            set { gameExecutablePath = value; }
        }

        #region Load/Save

        public void Save(string fileName)
        {
            XmlIO.Save<EditorSettings>(this, fileName, true);
        }

        public static EditorSettings Load(string fileName)
        {
            return XmlIO.Load<EditorSettings>(fileName, true);
        }

        //Add render target size and everything

        #endregion
    }
}
