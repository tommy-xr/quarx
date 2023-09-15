using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;


using System.IO;

namespace Quarx
{

    public class AtomClusterDescription
    {
        BlockColor color1;
        BlockColor color2;

        public BlockColor Color1
        {
            get { return color1; }
            set { color1 = value; }
        }

        public BlockColor Color2
        {
            get { return color2; }
            set { color2 = value; }
        }
    }
    /// <summary>
    /// The description of a p uzzle
    /// </summary>
    public class PuzzleDescription
    {
        string levelName;
        string iconName;
        Texture2D icon;

        Tile[,] tiles = new Tile[8, 18];
        List<AtomClusterDescription> atomClusterList = new List<AtomClusterDescription>();
        int height;

        public string LevelName
        {
            get { return levelName; }
        }

        public string IconName
        {
            get { return iconName; }
        }

        public Texture2D Icon
        {
            get { return icon; }
        }

        public List<AtomClusterDescription> AtomClusters
        {
            get { return atomClusterList; }
        }

        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        public void SetTile(Tile tile, int x, int y)
        {
            tiles[x, y] = tile;
        }

        public void Save(string puzzleName, string icon, StreamWriter sw)
        {
            sw.WriteLine(puzzleName);
            sw.WriteLine(icon);


            for (int y = 17; y >= 0; y--)
            {
                string line = "";
                for (int x = 0; x < 8; x++)
                {
                    Tile tile = this.GetTile(x, y);
                    if (tile == null || tile.IsBlock == false)
                        line += ".";
                    else
                    {
                        string addChar = "";
                        switch (tile.Color)
                        {
                            case BlockColor.Blue:
                                addChar = "b";
                                break;
                            case BlockColor.Red:
                                addChar = "r";
                                break;
                            case BlockColor.Yellow:
                                addChar = "y";
                                break;
                        }

                        if (tile.Type == BlockType.Isotope)
                            addChar = addChar.ToUpper();

                        line += addChar;
                    }
                }
                sw.WriteLine(line);
            }

            for (int i = 0; i < atomClusterList.Count; i++)
            {
                string sz = "";
                sz += GetColorFromType(atomClusterList[i].Color1);
                sz += GetColorFromType(atomClusterList[i].Color2);
                sw.WriteLine(sz);  
            }


        }

        char GetColorFromType(BlockColor color)
        {
            switch (color)
            {
                case BlockColor.Blue:
                    return 'b';
                case BlockColor.Red:
                    return 'r';
                case BlockColor.Yellow:
                    return 'y';
            }
            return ' ';
        }

        public PuzzleDescription()
        {

        }

        public PuzzleDescription(string fileName, ContentManager content)
        {
            //Get filestream
            FileStream fs = new FileStream(
                Path.Combine(StorageContainer.TitleLocation, fileName),
                FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            //Read level name
            levelName = sr.ReadLine();

            iconName = sr.ReadLine();
            //TODO: Bring this back if we decide we want icons
            //if(iconName != null && iconName != String.Empty)
            //icon = content.Load<Texture2D>(iconName);

            //Read board
            int boardHeight = 18;

            //Allocate room for hte tiles
            int xLength = 8;
            //tiles = new Tile[xLength, boardHeight];
            for (int y = boardHeight - 1; y >= 0; y--)
            {
                string text = sr.ReadLine();
                for (int x = 0; x < xLength; x++)
                {
                    tiles[x, y] = new Tile();
                    
                    switch (text[x])
                    {
                        case 'r':
                            tiles[x, y].IsBlock = true;
                            tiles[x, y].Color = BlockColor.Red;
                            tiles[x, y].Type = BlockType.Atom;
                            break;
                        case 'b':
                            tiles[x, y].IsBlock = true;
                            tiles[x, y].Color = BlockColor.Blue;
                            tiles[x, y].Type = BlockType.Atom;
                            break;
                        case 'y':
                            tiles[x, y].IsBlock = true;
                            tiles[x, y].Color = BlockColor.Yellow;
                            tiles[x, y].Type = BlockType.Atom;
                            break;
                        case 'R':
                            tiles[x, y].IsBlock = true;
                            tiles[x, y].Color = BlockColor.Red;
                            tiles[x, y].Type = BlockType.Isotope;
                            break;
                        case 'B':
                            tiles[x, y].IsBlock = true;
                            tiles[x, y].Color = BlockColor.Blue;
                            tiles[x, y].Type = BlockType.Isotope;
                            break;
                        case 'Y':
                            tiles[x, y].IsBlock = true;
                            tiles[x, y].Color = BlockColor.Yellow;
                            tiles[x, y].Type = BlockType.Isotope;
                            break;
                    }
                }

            }

            //Now, read the data for the list

            //atomClusterList = new List<AtomClusterDescription>();
            atomClusterList.Clear();
            while (!sr.EndOfStream)
            {
                BlockColor [] colors = new BlockColor[2];

                string sz = sr.ReadLine();
                sz = sz.Trim();
                sz = sz.ToLower();

                for (int i = 0; i < colors.Length; i++)
                {
                    if (sz[i] == 'r')
                        colors[i] = BlockColor.Red;
                    else if (sz[i] == 'y')
                        colors[i] = BlockColor.Yellow;
                    else if (sz[i] == 'b')
                        colors[i] = BlockColor.Blue;
                    else
                        throw new Exception("Couldn't read line of atom cluster list.");
                }

                AtomClusterDescription description = new AtomClusterDescription();
                description.Color1 = colors[0];
                description.Color2 = colors[1];

                atomClusterList.Add(description);
            }

            sr.Close();
            fs.Close();
            
        }


    }
}
