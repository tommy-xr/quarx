using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Engine
{
    public static class GlobalContent
    {
        private static ContentManager globalContent;
        private static Dictionary<string, object> nameToObject = new Dictionary<string,object>();

        public static void Initialize(IServiceProvider services)
        {
            globalContent = new ContentManager(services, "Content");
        }

        public static void LoadContent(string fileName)
        {
            fileName = fileName.ToLower();

            if(!nameToObject.ContainsKey(fileName))
            {
                nameToObject.Add(fileName, globalContent.Load<object>(fileName));
            }
        }

        public static bool HasContentItem(string fileName)
        {
            fileName = fileName.ToLower();
            return nameToObject.ContainsKey(fileName);
        }

        public static T GetContent<T>(string fileName)
        {
            fileName = fileName.ToLower();
            if (!HasContentItem(fileName))
                return default(T);

            return (T)nameToObject[fileName];
        }

        public static void Dispose(bool disposing)
        {
            globalContent.Dispose();
        }
    }
}
