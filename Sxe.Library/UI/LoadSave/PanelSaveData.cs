using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Library.UI
{
    public class PanelInfo
    {
        string name = "";
        Point position;
        Point size;

        public string Name 
        { 
            get { return name; } 
            set { name = value; }
        }
        public Point Position 
        { 
            get { return position; } 
            set { position = value; }
        }
        public Point Size 
        { 
            get { return size; } 
            set { size = value; }
        }
    
        public PanelInfo()
        {
        }
    }


}
