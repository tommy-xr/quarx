using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXEMaterialManager
{
    /// <summary>
    /// Class to store, handle, and return Texture2Ds
    /// </summary>
    public class TextureManager
    {
        public static string[] VALID_EXTENSIONS = new string[] { ".png", ".bmp", ".jpg", ".dds" };
        Dictionary<string, Texture2D> dictPathToTexture;
        IGraphicsDeviceService GraphicsDevice;
        ServiceContainer services;
        ILogService Log;

        public TextureManager(ServiceContainer inServices)
        {
            
            services = inServices;
            dictPathToTexture = new Dictionary<string, Texture2D>();

            Log = (ILogService)services.GetService(typeof(ILogService));
            GraphicsDevice = (IGraphicsDeviceService)services.GetService(typeof(IGraphicsDeviceService));
        }


        /// <summary>
        /// Called when the list of files changes. Loads any texture2D that needs to be loaded
        /// </summary>
        public void Load(string relPath, string basePath)
        {
                //Check if dictionary already has an entry for this
                string wholePath = basePath + Path.DirectorySeparatorChar + relPath;
                if (!dictPathToTexture.ContainsKey(relPath))
                {
                    //Check if the file extension is a valid image
                    if (IsValidName(relPath))
                    {
                        //Load a texture 2D and cache it for later use
                        try
                        {
                            Texture2D tex = Texture2D.FromFile(GraphicsDevice.GraphicsDevice,
                                wholePath);
                            dictPathToTexture.Add(relPath, tex);
                        }
                        catch
                        {
                            Log.Print("[Warning] Could not load texture: " + relPath);
                        }
                    }
                }

            
        }

        public Texture2D GetTextureByPath(string relPath)
        {
            if (dictPathToTexture.ContainsKey(relPath))
            {
                return dictPathToTexture[relPath];
            }

            return null;
        }

        /// <summary>
        /// Returns true if this is a valid image name, false otherwise
        /// </summary>
        bool IsValidName(string name)
        {
            for (int i = 0; i < VALID_EXTENSIONS.Length; i++)
            {
                if (name.Contains(VALID_EXTENSIONS[i]))
                    return true;
            }

            return false;
        }


    }
}
