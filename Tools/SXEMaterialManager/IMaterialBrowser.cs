using System;
using System.Collections.Generic;
using System.Text;

namespace SXEMaterialManager
{
    public class MaterialBrowserInfo
    {
        public string RelativePath;
        public int MaterialIndex;

        public MaterialBrowserInfo(string path, int index)
        {
            RelativePath = path;
            MaterialIndex = index;
        }
    }

    public interface IMaterialBrowser
    {
        void SetMaterials(MaterialBrowserInfo[] info, string currentDirectory);
        void ClearMaterials();
    }
}
