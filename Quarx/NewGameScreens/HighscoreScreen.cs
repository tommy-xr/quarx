using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx
{
    class HighscoreScreen : MessageScreen
    {
        private Sxe.Engine.UI.Menu.MenuGroup menuGroup1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Quarx.NewPanels.Jewel jewel1;
        private HighScorePanel highScorePanel1;

        private QuarxDifficulty currentList = QuarxDifficulty.Easy;
    
        public HighscoreScreen()
        {
            InitializeComponent();
        }

        public override void Activate()
        {
            HighScores.Load(this.GameScreenService.Storage);

           base.Activate();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            switch (currentList)
            {
                case QuarxDifficulty.Easy:
                    this.highScorePanel1.SelectedList = HighScores.Easy;
                    break;
                case QuarxDifficulty.Medium:
                    this.highScorePanel1.SelectedList = HighScores.Medium;
                    break;
                case QuarxDifficulty.Hard:
                    this.highScorePanel1.SelectedList = HighScores.Hard;
                    break;
                case QuarxDifficulty.Custom:
                    this.highScorePanel1.SelectedList = HighScores.Custom;
                    break;
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        void InitializeComponent()
        {
            this.highScorePanel1 = new Quarx.HighScorePanel();
            this.menuGroup1 = new Sxe.Engine.UI.Menu.MenuGroup();
            this.button1 = new Sxe.Engine.UI.Button();
            this.button2 = new Sxe.Engine.UI.Button();
            this.button3 = new Sxe.Engine.UI.Button();
            this.button4 = new Sxe.Engine.UI.Button();
            this.jewel1 = new Quarx.NewPanels.Jewel();
            // 
            // highScorePanel1
            // 
            this.highScorePanel1.BackgroundPath = "highscoresbackground";
            this.highScorePanel1.CanDrag = false;
            this.highScorePanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.highScorePanel1.Location = new Microsoft.Xna.Framework.Point(80, 62);
            this.highScorePanel1.Parent = this;
            this.highScorePanel1.SelectedList = null;
            this.highScorePanel1.Size = new Microsoft.Xna.Framework.Point(625, 450);
            this.highScorePanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.highScorePanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.2000000");
            this.highScorePanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.highScorePanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.2000000");
            // 
            // menuGroup1
            // 
            this.menuGroup1.BackgroundPath = null;
            this.menuGroup1.CanDrag = false;
            this.menuGroup1.ControlType = Sxe.Engine.UI.Menu.MenuControlType.Vertical;
            this.menuGroup1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.menuGroup1.Location = new Microsoft.Xna.Framework.Point(96, 182);
            this.menuGroup1.Panels.Add(this.button1);
            this.menuGroup1.Panels.Add(this.button2);
            this.menuGroup1.Panels.Add(this.button3);
            this.menuGroup1.Panels.Add(this.button4);
            this.menuGroup1.Panels.Add(this.jewel1);
            this.menuGroup1.Parent = this;
            this.menuGroup1.Size = new Microsoft.Xna.Framework.Point(92, 304);
            this.menuGroup1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.menuGroup1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.menuGroup1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.menuGroup1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.menuGroup1.MenuCancel += new System.EventHandler<ButtonPressEventArgs>(this.menuGroup1_MenuCancel);
            // 
            // button1
            // 
            this.button1.BackgroundPath = null;
            this.button1.CanDrag = false;
            this.button1.ClickImagePath = "easybright";
            this.button1.DefaultImagePath = "easydull";
            this.button1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button1.FontName = null;
            this.button1.FontPath = null;
            this.button1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button1.Location = new Microsoft.Xna.Framework.Point(5, 10);
            this.button1.OverCue = null;
            this.button1.OverImagePath = "easybright";
            this.button1.Parent = this.menuGroup1;
            this.button1.PressCue = null;
            this.button1.Size = new Microsoft.Xna.Framework.Point(81, 40);
            this.button1.Text = null;
            this.button1.TextImagePath = null;
            this.button1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button1.ButtonOver += new System.EventHandler(this.button1_ButtonOver);
            // 
            // button2
            // 
            this.button2.BackgroundPath = null;
            this.button2.CanDrag = false;
            this.button2.ClickImagePath = "mediumbright";
            this.button2.DefaultImagePath = "mediumdull";
            this.button2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button2.FontName = null;
            this.button2.FontPath = null;
            this.button2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button2.Location = new Microsoft.Xna.Framework.Point(5, 59);
            this.button2.OverCue = null;
            this.button2.OverImagePath = "mediumbright";
            this.button2.Parent = this.menuGroup1;
            this.button2.PressCue = null;
            this.button2.Size = new Microsoft.Xna.Framework.Point(81, 40);
            this.button2.Text = null;
            this.button2.TextImagePath = null;
            this.button2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button2.ButtonOver += new System.EventHandler(this.button2_ButtonOver);
            // 
            // button3
            // 
            this.button3.BackgroundPath = null;
            this.button3.CanDrag = false;
            this.button3.ClickImagePath = "hardbright";
            this.button3.DefaultImagePath = "harddull";
            this.button3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button3.FontName = null;
            this.button3.FontPath = null;
            this.button3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button3.Location = new Microsoft.Xna.Framework.Point(5, 105);
            this.button3.OverCue = null;
            this.button3.OverImagePath = "hardbright";
            this.button3.Parent = this.menuGroup1;
            this.button3.PressCue = null;
            this.button3.Size = new Microsoft.Xna.Framework.Point(81, 40);
            this.button3.Text = null;
            this.button3.TextImagePath = null;
            this.button3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button3.ButtonOver += new System.EventHandler(this.button3_ButtonOver);
            // 
            // button4
            // 
            this.button4.BackgroundPath = null;
            this.button4.CanDrag = false;
            this.button4.ClickImagePath = "custombright";
            this.button4.DefaultImagePath = "customdull";
            this.button4.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button4.FontName = null;
            this.button4.FontPath = null;
            this.button4.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button4.Location = new Microsoft.Xna.Framework.Point(6, 151);
            this.button4.OverCue = null;
            this.button4.OverImagePath = "custombright";
            this.button4.Parent = this.menuGroup1;
            this.button4.PressCue = null;
            this.button4.Size = new Microsoft.Xna.Framework.Point(81, 40);
            this.button4.Text = null;
            this.button4.TextImagePath = null;
            this.button4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button4.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button4.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button4.ButtonOver += new System.EventHandler(this.button4_ButtonOver);
            // 
            // jewel1
            // 
            this.jewel1.BackgroundPath = null;
            this.jewel1.CanDrag = false;
            this.jewel1.JewelType = Quarx.NewPanels.JewelType.B;
            this.jewel1.Location = new Microsoft.Xna.Framework.Point(2, 240);
            this.jewel1.Parent = this.menuGroup1;
            this.jewel1.Size = new Microsoft.Xna.Framework.Point(29, 29);
            this.jewel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.jewel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // HighscoreScreen
            // 
            this.Panels.Add(this.highScorePanel1);
            this.Panels.Add(this.menuGroup1);
            this.PresenceMode = Microsoft.Xna.Framework.GamerServices.GamerPresenceMode.WastingTime;
            this.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);

        }


        private void button1_ButtonOver(object sender, EventArgs e)
        {
            this.currentList = QuarxDifficulty.Easy;
            //highScorePanel1.SelectedList = HighScores.Easy;
        }

        private void button2_ButtonOver(object sender, EventArgs e)
        {
            this.currentList = QuarxDifficulty.Medium;
           //highScorePanel1.SelectedList = HighScores.Medium;
        }

        private void button3_ButtonOver(object sender, EventArgs e)
        {
            this.currentList = QuarxDifficulty.Hard;
            //highScorePanel1.SelectedList = HighScores.Hard;
        }

        private void button4_ButtonOver(object sender, EventArgs e)
        {
            this.currentList = QuarxDifficulty.Custom;
            //highScorePanel1.SelectedList = HighScores.Custom;
        }

        private void menuGroup1_MenuCancel(object sender, EventArgs e)
        {
            this.ExitScreen();
        }
    }
}
