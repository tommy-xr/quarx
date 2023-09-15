using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Engine.UI
{
    public class Scheme : IScheme
    {
        class ImageData
        {
            public Texture2D texture;
            public DrawMode drawMode;

        }

        string basePath = "";
        Dictionary<string, ImageData> imageDictionary = new Dictionary<string,ImageData>();
        Dictionary<string, SpriteFont> fontDictionary = new Dictionary<string,SpriteFont>();
        Dictionary<string, Color> colorDictionary = new Dictionary<string,Color>();
        Dictionary<string, Point> pointDictionary = new Dictionary<string,Point>();
        Dictionary<string, float> floatDictionary = new Dictionary<string,float>();
        Dictionary<string, int> intDictionary = new Dictionary<string, int>();
        Dictionary<string, Texture2D> textureDictionary = new Dictionary<string, Texture2D>();

        ContentManager content;

        IScheme defaultScheme;
        public IScheme DefaultScheme
        {
            get { return defaultScheme; }
            set { defaultScheme = value; }
        }


        /// <summary>
        /// Loads a scheme from a file
        /// </summary>
        /// <param name="filename"></param>
        public Scheme(string filename, ContentManager content)
            : this(new FileStream(filename, FileMode.Open, FileAccess.Read), content)
        {
            
        }

        public Scheme(Stream stream, ContentManager inContent)
        {
            content = inContent;
            TextReader reader = new StreamReader(stream);
            //Parse the string to add data

            string sz = reader.ReadLine();
            while (sz != null)
            {
                ParseLine(sz);
                sz = reader.ReadLine();
            }

            reader.Close();
            stream.Close();
        }

        void ParseLine(string sz)
        {
#if !XBOX
            if (sz == null)
                return;

            string[] tokens;
            tokens = sz.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length <= 0)
                return;

            switch (tokens[0])
            {
                case "BASE_PATH":
                    basePath = tokens[1];
                    break;
                case "image":
                    ImageData data = new ImageData();
                    data.texture = content.Load<Texture2D>(basePath + tokens[2]);
                    imageDictionary.Add(tokens[1], data);

                    if (tokens[3] == "bordered")
                        data.drawMode = DrawMode.BordersFixed;
                    else
                        data.drawMode = DrawMode.Stretch;

                    break;
                case "texture":
                    Texture2D texture = content.Load<Texture2D>(basePath + tokens[2]);
                    textureDictionary.Add(tokens[1], texture);
                    break;
                case "point":
                    int p1 = Int32.Parse(tokens[2]);
                    int p2 = Int32.Parse(tokens[3]);
                    pointDictionary.Add(tokens[1], new Point(p1, p2));
                    break;
                case "int":
                    int i = Int32.Parse(tokens[2]);
                    intDictionary.Add(tokens[1], i);
                    break;
                case "color":
                    byte r = Byte.Parse(tokens[2]);
                    byte g = Byte.Parse(tokens[3]);
                    byte b = Byte.Parse(tokens[4]);
                    byte a = Byte.Parse(tokens[5]);
                    colorDictionary.Add(tokens[1], new Color(r, g, b, a));
                    break;
                case "font":
                    SpriteFont font = content.Load<SpriteFont>(basePath + tokens[2]);
                    fontDictionary.Add(tokens[1], font);
                    break;
                default:
                    break;
            }
#endif
        }


        public UIImage GetImage(string imageName)
        {
            if (!imageDictionary.ContainsKey(imageName))
            {
                if (defaultScheme == null)
                    return null;

                return defaultScheme.GetImage(imageName);
            }

            ImageData data = imageDictionary[imageName];
            UIImage image = new UIImage(data.texture);
            image.DrawMode = data.drawMode;
            return image;
        }

        public int GetInt(string intName)
        {
            if(!intDictionary.ContainsKey(intName))
            {
                return defaultScheme.GetInt(intName);
            }
            return intDictionary[intName];
        }

        public Point GetPoint(string pointName)
        {
            if (!pointDictionary.ContainsKey(pointName))
                return defaultScheme.GetPoint(pointName);

            return pointDictionary[pointName];
        }


        public float GetFloat(string floatName)
        {
            return 0.0f;
        }

        public Texture2D GetTexture(string textureName)
        {
            if(!textureDictionary.ContainsKey(textureName))
                return defaultScheme.GetTexture(textureName);

            return textureDictionary[textureName];
        }

        public Color GetColor(string colorName)
        {
            if (!colorDictionary.ContainsKey(colorName))
                return defaultScheme.GetColor(colorName);

            return colorDictionary[colorName];
        }
        public SpriteFont GetFont(string fontName)
        {
            if (!fontDictionary.ContainsKey(fontName))
                return defaultScheme.GetFont(fontName);

            return fontDictionary[fontName];
        }

        



        public void UnloadContent()
        {
            content.Dispose();
        }
    }
}
