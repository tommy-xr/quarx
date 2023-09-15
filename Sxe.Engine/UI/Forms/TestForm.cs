using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.UI.Forms
{
    public class TestForm : Form
    {
        private Panel panel1;
    
        public TestForm()
        {
            InitializeComponent();
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = null;
            this.panel1.CanDrag = false;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(161, 176);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(50, 50);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);

        }
    }
}
