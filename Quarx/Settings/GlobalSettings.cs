using System;
using System.Collections.Generic;
using System.Text;

namespace Quarx
{
    public class GlobalSettings
    {
        string musicType;
        int numberOfRounds = 3;
        int maxPunish = 8;


        public string MusicType
        {
            get { return musicType; }
            set { musicType = value; }
        }

        public int NumberOfRounds
        {
            get { return numberOfRounds; }
            set { numberOfRounds = value; }
        }

        public int MaxStorePunish
        {
            get { return maxPunish; }
            set { maxPunish = value; }
        }

    }
}
