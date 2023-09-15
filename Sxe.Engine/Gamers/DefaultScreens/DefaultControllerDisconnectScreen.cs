using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine
{
    public class DefaultControllerDisconnectScreen : BaseControllerDisconnectScreen 
    {
        private Label label1;
        private Label label2;
        private Panel panel1;

        //private Texture2D gradientTexture;

        public DefaultControllerDisconnectScreen()
        {
            InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);
            //gradientTexture = content.Load<Texture2D>("gradient");
            this.panel1.Image = LoadImage(content, "gradient");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            label2.Caption = "";
            for (int i = 0; i < DisconnectedList.Count; i++)
            {
                label2.Caption += DisconnectedList[i].GamerTag + "\n";
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.label1 = new Sxe.Engine.UI.Label();
            this.label2 = new Sxe.Engine.UI.Label();
            // 
            // panel1
            // 
            this.panel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel1.BackgroundPath = "gradient";
            this.panel1.CanDrag = false;
            this.panel1.Enabled = true;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(1, 182);
            this.panel1.Name = "";
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(799, 176);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.Visible = true;
            // 
            // label1
            // 
            this.label1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.BackgroundPath = null;
            this.label1.CanDrag = false;
            this.label1.Caption = "A controller is disconnected!";
            this.label1.Enabled = true;
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Calibri11";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label1.Location = new Microsoft.Xna.Framework.Point(10, 193);
            this.label1.Name = "";
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(184, 27);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            this.label1.Visible = true;
            // 
            // label2
            // 
            this.label2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.BackgroundPath = null;
            this.label2.CanDrag = false;
            this.label2.Caption = "";
            this.label2.Enabled = true;
            this.label2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.FontPath = "Calibri11";
            this.label2.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label2.Location = new Microsoft.Xna.Framework.Point(98, 219);
            this.label2.Name = "";
            this.label2.Parent = this;
            this.label2.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label2.Size = new Microsoft.Xna.Framework.Point(311, 107);
            this.label2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            this.label2.Visible = true;
            // 
            // DefaultControllerDisconnectScreen
            // 
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.label1);
            this.Panels.Add(this.label2);

        }
    }
}
