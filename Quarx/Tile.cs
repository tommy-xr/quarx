using System;
using System.Collections.Generic;
using System.Text;

namespace Quarx
{
    /// <summary>
    /// Tile is a way of describing a cell in a QuarxGameBoard
    /// without having to allocate memory
    /// </summary>
    public class Tile
    {
        bool isBlock;
        BlockColor color;
        BlockType type;

        public bool IsBlock
        {
            get { return isBlock; }
            set { isBlock = value; }
        }

        public BlockColor Color
        {
            get { return color; }
            set { color = value; }
        }

        public BlockType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Tile Copy()
        {
            Tile tile = new Tile();
            tile.IsBlock = this.IsBlock;
            tile.Color = this.Color;
            tile.Type = this.Type;
            return tile;
        }

    }
}
