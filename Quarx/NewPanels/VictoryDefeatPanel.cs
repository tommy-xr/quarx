using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx
{
    public class VictoryDefeatPanel : CompositePanel
    {

        UIImage victoryImage;
        UIImage defeatImage;

        public bool Victorious
        {
            set
            {
                if (value == true)
                    this.Image = victoryImage;
                else
                    this.Image = defeatImage;
            }
        }
    
        public VictoryDefeatPanel()
        {
            InitializeComponent();
        }

        void InitializeComponent()
        {
            // 
            // VictoryDefeatPanel
            // 
            this.Size = new Microsoft.Xna.Framework.Point(220, 50);

        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);
            victoryImage = LoadImage(content, "victory");
            defeatImage = LoadImage(content, "defeat");

            this.Image = victoryImage;
        }
    }
}
