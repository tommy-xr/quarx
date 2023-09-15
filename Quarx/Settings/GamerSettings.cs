using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine;

namespace Quarx
{
    public class GamerSettings
    {
        IAnarchyGamer gamer;
        int option1;
        int option2;
        QuarxGameSettings settings;
        
        public IAnarchyGamer Gamer
        {
            get { return gamer; }
            set { gamer = value; }
        }

        public int Option1
        {
            get { return option1; }
            set { option1 = value; }
        }

        public int Option2
        {
            get { return option2; }
            set { option2 = value; }
        }

        public QuarxGameSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


    }
}
