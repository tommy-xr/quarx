using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine.Input;

namespace Quarx
{
    public class MusicSelect : MessageScreen
    {
        private Sxe.Engine.UI.Menu.MenuGroup menuGroup1;
        private QuarxOption quarxOption1;
        private QuarxOption quarxOption2;
        private QuarxOption quarxOption3;
        private HelpTicker helpTicker1;

        private HelpTickerLabel musicLabel = new HelpTickerLabel("Pick your favorite Quarx tune!");
        private HelpTickerLabel punishLabel = new HelpTickerLabel("Set the maximum amount of punishes that can be stored.");
        private HelpTickerLabel roundsLabel = new HelpTickerLabel("Set the number of rounds that need to be won.");

        private TestPlayerOptions optionsScreen;
        public TestPlayerOptions OptionsScreen
        {
            get { return optionsScreen; }
            set { optionsScreen = value; }
        }

        bool singlePlayerOnly = false;
        public bool SinglePlayerOnly
        {
            get { return singlePlayerOnly; }
            set { singlePlayerOnly = value; this.UpdateSinglePlayer(); }
        }


        void UpdateSinglePlayer()
        {
            if (this.singlePlayerOnly)
            {
                this.quarxOption2.Enabled = false;
                this.quarxOption3.Enabled = false;

                this.quarxOption2.Values.Clear();
                this.quarxOption2.Values.Add("Multi Only");
                this.quarxOption2.SetSelected(0);

                this.quarxOption3.Values.Clear();
                this.quarxOption3.Values.Add("Not Available");
                this.quarxOption3.SetSelected(0);
            }
        }


        public MusicSelect()
        {
            InitializeComponent();
            quarxOption1.Values.Add("Toped");
            quarxOption1.Values.Add("Alienz");
            quarxOption1.Values.Add("None");

            for (int i = 1; i <= 8; i++)
                quarxOption2.Values.Add(i.ToString());

            for (int i = 1; i <= 3; i++)
                quarxOption3.Values.Add(i.ToString());

            quarxOption2.SetSelected(7);
            quarxOption3.SetSelected(2);
        }


        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            ControllerChangedEventArgs changedEvent = inputEvent as ControllerChangedEventArgs;
            if (changedEvent != null)
            {
                if (Input.Controllers[changedEvent.PlayerIndex].IsKeyJustPressed("menu_back"))
                {
                    optionsScreen.GlobalSettings = null;
                    this.optionsScreen.Reset();
                    this.ExitScreen();
                }
                else if (Input.Controllers.IsKeyJustPressed("menu_select"))
                {

                    GlobalSettings globalSettings = new GlobalSettings();

                    int maxStorePunish = 8;
                    int numRounds = 3;
                    if(!singlePlayerOnly)
                    {
                        maxStorePunish = Int32.Parse(quarxOption2.Selected);
                        numRounds = Int32.Parse(quarxOption3.Selected);
                    }

                    globalSettings.MaxStorePunish = maxStorePunish;
                    globalSettings.NumberOfRounds = numRounds;
                    globalSettings.MusicType = this.quarxOption1.Selected;
                    this.optionsScreen.GlobalSettings = globalSettings;
                    this.ExitScreen();
                }
            }

            return base.HandleEvent(inputEvent);
        }

        void InitializeComponent()
        {
            this.menuGroup1 = new Sxe.Engine.UI.Menu.MenuGroup();
            this.quarxOption1 = new Quarx.QuarxOption();
            this.quarxOption2 = new Quarx.QuarxOption();
            this.quarxOption3 = new Quarx.QuarxOption();
            this.helpTicker1 = new Quarx.HelpTicker();
            // 
            // menuGroup1
            // 
            this.menuGroup1.BackgroundPath = "Options\\subsetupback";
            this.menuGroup1.ControlType = Sxe.Engine.UI.Menu.MenuControlType.Vertical;
            this.menuGroup1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.menuGroup1.Location = new Microsoft.Xna.Framework.Point(245, 75);
            this.menuGroup1.Panels.Add(this.quarxOption1);
            this.menuGroup1.Panels.Add(this.quarxOption2);
            this.menuGroup1.Panels.Add(this.quarxOption3);
            this.menuGroup1.Parent = this;
            this.menuGroup1.Size = new Microsoft.Xna.Framework.Point(312, 323);
            this.menuGroup1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.menuGroup1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.menuGroup1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.menuGroup1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // quarxOption1
            // 
            this.quarxOption1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.quarxOption1.BackgroundPath = null;
            this.quarxOption1.Caption = null;
            this.quarxOption1.FontPath = null;
            this.quarxOption1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.quarxOption1.Location = new Microsoft.Xna.Framework.Point(31, 65);
            this.quarxOption1.Parent = this.menuGroup1;
            this.quarxOption1.Size = new Microsoft.Xna.Framework.Point(275, 50);
            this.quarxOption1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption1.ButtonOver += new System.EventHandler<System.EventArgs>(this.quarxOption1_ButtonOver);
            // 
            // quarxOption2
            // 
            this.quarxOption2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.quarxOption2.BackgroundPath = null;
            this.quarxOption2.Caption = null;
            this.quarxOption2.FontPath = null;
            this.quarxOption2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.quarxOption2.Location = new Microsoft.Xna.Framework.Point(31, 145);
            this.quarxOption2.Parent = this.menuGroup1;
            this.quarxOption2.Size = new Microsoft.Xna.Framework.Point(275, 50);
            this.quarxOption2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption2.ButtonOver += new System.EventHandler<System.EventArgs>(this.quarxOption2_ButtonOver);
            // 
            // quarxOption3
            // 
            this.quarxOption3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.quarxOption3.BackgroundPath = null;
            this.quarxOption3.Caption = null;
            this.quarxOption3.FontPath = null;
            this.quarxOption3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.quarxOption3.Location = new Microsoft.Xna.Framework.Point(31, 225);
            this.quarxOption3.Parent = this.menuGroup1;
            this.quarxOption3.Size = new Microsoft.Xna.Framework.Point(275, 50);
            this.quarxOption3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption3.ButtonOver += new System.EventHandler<System.EventArgs>(this.quarxOption3_ButtonOver);
            // 
            // helpTicker1
            // 
            this.helpTicker1.BackgroundPath = null;
            this.helpTicker1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.helpTicker1.Location = new Microsoft.Xna.Framework.Point(245, 450);
            this.helpTicker1.Parent = this;
            this.helpTicker1.Size = new Microsoft.Xna.Framework.Point(312, 25);
            this.helpTicker1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.helpTicker1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.helpTicker1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.helpTicker1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.helpTicker1.TickerColor = new Microsoft.Xna.Framework.Graphics.Color(0, 0, 255, 255);
            // 
            // MusicSelect
            // 
            this.Panels.Add(this.menuGroup1);
            this.Panels.Add(this.helpTicker1);
            this.PresenceMode = Microsoft.Xna.Framework.GamerServices.GamerPresenceMode.ConfiguringSettings;

        }

        private void quarxOption1_ButtonOver(object sender, EventArgs e)
        {
            this.helpTicker1.Add(this.musicLabel);
        }

        private void quarxOption2_ButtonOver(object sender, EventArgs e)
        {
            this.helpTicker1.Add(this.punishLabel);
        }

        private void quarxOption3_ButtonOver(object sender, EventArgs e)
        {
            this.helpTicker1.Add(this.roundsLabel);
        }
    }
}
