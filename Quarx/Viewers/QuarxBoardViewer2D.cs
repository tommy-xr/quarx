using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Quarx
{
    public class QuarxBoardViewer2D : IBoardViewer
    {
        const int IconSize = 16;

        Texture2D isotope;
        Texture2D circle;
        Texture2D grid;
        BaseGameModel model;
        SpriteBatch sb;

        RenderTarget2D target;
        Texture2D outTexture;

        GraphicsDevice device;

        public Texture2D Texture
        {
            get { return outTexture; }
        }

        public BaseGameModel Model
        {
            get { return model; }
            set { model = value; }
        }

        public QuarxGameBoard Board
        {
            get { return model.Board; }
            //get { return board; }
            //set { board = value; }
        }

        public Texture2D PreviewTexture
        {
            get { return null; }
        }

        public Texture2D GetPreviewTexture(BlockColor color1, BlockColor color2)
        {
            return null;
        }

        public QuarxBoardViewer2D(GraphicsDevice inDevice, ContentManager content, int boardWidth, int boardHeight)
        {
            device = inDevice;
            sb = new SpriteBatch(device);
            Load(content, boardWidth, boardHeight);
        }

        void Load(ContentManager content, int boardWidth, int boardHeight)
        {
            circle = content.Load<Texture2D>("circle");
            grid = content.Load<Texture2D>("grid");
            isotope = content.Load<Texture2D>("isotope");

            target = new RenderTarget2D(device, boardWidth * IconSize, boardHeight * IconSize, 1, SurfaceFormat.Color);

        }


        public void Draw(GameTime gameTime)
        {
            device.SetRenderTarget(0, target);
            
            sb.Begin();
            for (int x = 0; x < Board.Width; x++)
            {
                for (int y = 0; y < Board.Height; y++)
                {
                    int location = Board.Height - y - 1;
                    Rectangle rectangle = new Rectangle(x * IconSize, location * IconSize, IconSize, IconSize);

                    sb.Draw(grid, rectangle, Color.White);

                    Block block = Board[x, y];
                    if (block != null)
                    {
                        Block blockModel =Board[ x, y];
                        Color color = GetColorFromType(block.Color);
                        color = new Color(color.R, color.G, color.B, block.Alpha);
                        //if(blockModel.UserControlled)
                         //   color = Color.Black;
                        Texture2D tex = circle;
                        if (block.Type == BlockType.Isotope)
                            tex = isotope;

                        sb.Draw(tex,rectangle , color  );
                    }
                }
            }
            sb.End();

            device.SetRenderTarget(0, null);
            outTexture = target.GetTexture();
        }

        Color GetColorFromType(BlockColor color)
        {
            switch (color)
            {
                default:
                    return Color.White;
                case BlockColor.Blue:
                    return Color.Blue;
                case BlockColor.Red:
                    return Color.Red;
                case BlockColor.Yellow:
                    return Color.Yellow;
            }
        }


    }
}
