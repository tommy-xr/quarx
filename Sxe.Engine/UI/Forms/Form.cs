using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI.Forms
{
    public class Form : BaseScreen
    {
        private FormTitleBarPanel formTitleBarPanel1;
        

        public Form()
        {
            InitializeComponent();

            formTitleBarPanel1.Location = new Point(0, 0);
            formTitleBarPanel1.Size = new Point(this.Size.X, formTitleBarPanel1.Size.Y);
        }

        void InitializeComponent()
        {
            this.formTitleBarPanel1 = new Sxe.Engine.UI.FormTitleBarPanel();
            // 
            // formTitleBarPanel1
            // 
            this.formTitleBarPanel1.BackgroundPath = null;
            this.formTitleBarPanel1.CanDrag = false;
            this.formTitleBarPanel1.Icon = null;
            this.formTitleBarPanel1.Location = new Microsoft.Xna.Framework.Point(167, 199);
            this.formTitleBarPanel1.Parent = this;
            this.formTitleBarPanel1.Size = new Microsoft.Xna.Framework.Point(50, 50);
            this.formTitleBarPanel1.Text = "";
            this.formTitleBarPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.formTitleBarPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // Form
            // 
            this.Panels.Add(this.formTitleBarPanel1);

        }
    }
}
