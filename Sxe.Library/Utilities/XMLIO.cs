using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

//#if !XBOX
using System.Xml.Serialization;

#if !XBOX
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
#endif

namespace Sxe.Library.Utilities
{
    /// <summary>
    /// Utility function to save and restore classes from XML files
    /// Only works with classes that are serializable (no multi-dimensional arrays)
    /// </summary>
    public static class XmlIO
    {
        public static void Save<T>(T saveObject, string fileName, bool xnaFriendly)
        {
            //Stream stream = File.Create(fileName);
            

            //if (!xnaFriendly)
            //{
            string directory = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

                FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, saveObject);
                stream.Close();
            //}
            //else
            //{
            //    XmlWriterSettings settings = new XmlWriterSettings();
            //    settings.NewLineChars = Environment.NewLine;
            //    settings.NewLineOnAttributes = true;
            //    settings.NewLineHandling = NewLineHandling.Replace;

            //    string directory = Path.GetDirectoryName(fileName);
            //    if (!Directory.Exists(directory))
            //        Directory.CreateDirectory(directory);

            //    XmlWriter writer = XmlWriter.Create(fileName, settings);
            //    IntermediateSerializer.Serialize<T>(writer, saveObject, null);
            //    writer.Close();
            //}
            
        }

        /// <summary>
        /// Loads settings from a file
        /// </summary>
        /// <param name="filename">The filename to load</param>
        public static T Load<T>(string fileName, bool xnaFriendly)
        {
            Stream stream = File.OpenRead(fileName);
            //if (!xnaFriendly)
            //{
               XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            //}
            //else
            //{
            //    XmlReader reader = XmlReader.Create(stream);
            //    return (T)IntermediateSerializer.Deserialize<T>(reader, null);
            //}
        }
    }
}
//#endif