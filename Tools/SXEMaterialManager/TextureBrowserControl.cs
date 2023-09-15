using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SXEMaterialManager
{
    public class TextureBrowserControl :GraphicsDeviceControl, IMaterialBrowser
    {
        ContentManager content;
        SpriteBatch sprites;
        SpriteFont font;

        TextureManager texManager;
        MaterialBrowserInfo [] currentInfo;
        string currentDirectory;


        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            texManager = new TextureManager(Services);
        }


        public void SetMaterials(MaterialBrowserInfo[] info, string currentDirectory)
        {
            currentInfo = info;
            //Loop through and make sure our texture manager has all these loaded
            for (int i = 0; i < info.Length; i++)
                texManager.Load(info[i].RelativePath, currentDirectory);
            

        }

        public void ClearMaterials()
        {
            currentInfo = null;
        }

        protected override void Initialize()
        {
  
            content = new ContentManager(Services);
            sprites = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("Content/Arial");


            //Hook idle event to redraw
            Application.Idle += delegate { Invalidate(); };
        }

        protected override void Draw()
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            sprites.Begin();
            sprites.DrawString(font, "No materials selected", Vector2.Zero, Color.White);

            if (currentInfo != null)
            {
                for (int i = 0; i < currentInfo.Length; i++)
                {
                    Texture2D tex = texManager.GetTextureByPath(currentInfo[i].RelativePath);

                    if(tex != null)
                    sprites.Draw(tex, new Rectangle(10, 10, 50, 50), Color.White);
                }
            }
            sprites.End();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            p.X -= this.Left;
            p.Y -= this.Top;

            base.OnMouseClick(e);
        }
    }
}
