using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sxe.Engine;
using Sxe.Engine.UI;

namespace Quarx.AI
{
    public class QuarxAIGamer : BaseAIGamer
    {
        string name;
        UIImage image;

        public override Sxe.Engine.UI.UIImage GamerIcon
        {
            get
            {
                if (image == null)
                    image = new UIImage(AnarchyGamer.ComputerIcon);
                return image;
            }
        }

        public override string GamerTag
        {
            get
            {
                return "[C] " + name;
            }
        }

        public QuarxAIGamer()
        {
            name = AnarchyGamer.GetRandomName(true);
            //name = Sxe.Engine.Utilities.Utilities.GetRandomFromList(gamerNames);
        }

        public override void OnSignOut()
        {
            AnarchyGamer.ReturnName(name);
            base.OnSignOut();
        }

        


    }
}
