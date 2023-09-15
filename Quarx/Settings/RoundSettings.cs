using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine;

namespace Quarx
{
    public class RoundSettings
    {
        public static RoundSettings CreateFromQuarxSettings(QuarxGameSettings quarxSettings, int playerIndex )
        {
            RoundSettings roundSettings = new RoundSettings();
            GamerSettings gamerSettings = new GamerSettings();
            gamerSettings.Gamer = AnarchyGamer.Gamers.GetGamerByPlayerIndex(playerIndex);
            gamerSettings.Settings = quarxSettings;
            roundSettings.GamerSettings.Add(gamerSettings);
            return roundSettings;
        }


        GlobalSettings global = new GlobalSettings();
        List<GamerSettings> gamerSettings = new List<GamerSettings>();


        public GlobalSettings GlobalSettings
        {
            get { return global; }
            set { global = value; }
        }

        public List<GamerSettings> GamerSettings
        {
            get { return gamerSettings; }
        }
    }
}
