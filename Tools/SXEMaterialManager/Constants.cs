using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SXEMaterialManager
{
    public static class Constants
    {
    
        public const string DefaultMaterialDirectory = "DefaultMaterial";
        public const string DefaultTexture = "defaultDiffuse.png";
        public const string DefaultNormal = "defaultNormal.png";
        public const string DefaultSpecular = "defaultSpecular.png";
        public const string DefaultShader = "BspDefault.fx";

        public static string DefaultTexturePath = DefaultMaterialDirectory + Path.DirectorySeparatorChar + DefaultTexture;
        public static string DefaultNormalPath = DefaultMaterialDirectory + Path.DirectorySeparatorChar + DefaultNormal;
        public static string DefaultSpecularPath = DefaultMaterialDirectory + Path.DirectorySeparatorChar + DefaultSpecular;
        public static string DefaultShaderPath = DefaultMaterialDirectory + Path.DirectorySeparatorChar + DefaultShader;


    }
}
