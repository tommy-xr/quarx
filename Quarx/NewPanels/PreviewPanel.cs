using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public class PreviewPanel : CompositePanel
    {
        private Panel panel1;

        public PreviewPanel()
        {
            InitializeComponent();
        }

        public void SetTexture(Texture2D tex)
        {
            if (panel1.Image == null)
                panel1.Image = new UIImage();
            panel1.Image.Value = tex;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            //panel1.Image = new UIImage();
            base.LoadContent(content);
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel1.BackgroundPath = null;
            this.panel1.CanDrag = false;
            this.panel1.Enabled = true;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(15, 22);
            this.panel1.Name = "";
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(50, 46);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.Visible = true;
            // 
            // PreviewPanel
            // 
            this.BackgroundPath = "previewbox";
            this.Panels.Add(this.panel1);
            this.Size = new Microsoft.Xna.Framework.Point(75, 75);

        }
    }
}
