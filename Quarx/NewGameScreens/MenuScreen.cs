using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine;
using Sxe.Engine.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Content;

namespace Quarx.GameScreens
{
    public class MenuScreen : BaseScreen
    {
        private Sxe.Engine.UI.Menu.MenuGroup menuGroup1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Panel panel4;
        private Sxe.Engine.UI.Menu.MenuGroup menuGroup2;
        private Sxe.Engine.UI.Menu.MenuGroup menuGroup3;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;
        private Button button13;
        private LightningBackgroundPanel lightningBackgroundPanel1;
        private Button button14;
        private Panel panel1;
        private Panel panel2;
        private Button button15;
        private Button button16;
        private Button button17;
        private Button button18;
        private GamerSignInBox gamerSignInBox1;
        private SignInModule signInModule1;
        private Button button4;

        private List<string> buyOptions = new List<string>() { "Sure!", "Nah, I'm good." };
        private bool showMarketPlace = false;
        private Panel panel3;
        private HelpTicker helpTicker1;
        private PlayerIndex marketIndex = PlayerIndex.One;

        HelpTickerLabel helpSinglePlayer;
        HelpTickerLabel helpMultiplayer;
        HelpTickerLabel helpHowToPlay;
        HelpTickerLabel helpHighScores;
        HelpTickerLabel helpExit;
        HelpTickerLabel helpQuick;
        HelpTickerLabel helpCustom;
        HelpTickerLabel helpPuzzle;
        HelpTickerLabel helpEasy;
        HelpTickerLabel helpMedium;
        HelpTickerLabel helpHard;
       // HelpTickerLabel help

        public MenuScreen()
        {
            InitializeComponent();
            this.ActiveControl = menuGroup1;

            helpSinglePlayer = new HelpTickerLabel("Play Quarx alone and compete for a high score or conquer some puzzles!");
            helpMultiplayer = new HelpTickerLabel("Take on some friends or practice against a computer opponent!");
            helpHowToPlay = new HelpTickerLabel("New to Quarx? Get briefed here!");
            helpHighScores = new HelpTickerLabel("See who's a Quarx All-Star here.");
            helpExit = new HelpTickerLabel("Press this if you can't take the heat...");
            helpQuick = new HelpTickerLabel("Jump right into Quarx here, and try to achieve a high score!");
            helpCustom = new HelpTickerLabel("Play Quarx on your own terms!");
            helpPuzzle = new HelpTickerLabel("Rack your brain and solve some diabolical puzzles!");
            helpEasy = new HelpTickerLabel("Easy - for the faint of heart.");
            helpMedium = new HelpTickerLabel("Medium - challenging, yet tasteful.");
            helpHard = new HelpTickerLabel("Hard - for intense puzzle veterans.");
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            button1.OverImage = GetAnimatedButtonPressImage(content);
            button2.OverImage = GetAnimatedButtonPressImage(content);
            button3.OverImage = GetAnimatedButtonPressImage(content);
            button4.OverImage = GetAnimatedButtonPressImage(content);
            button14.OverImage = GetAnimatedButtonPressImage(content);

            button5.OverImage = GetAnimatedSubButton(content);
            button6.OverImage = GetAnimatedSubButton(content);
            button18.OverImage = GetAnimatedSubButton(content);

            button11.OverImage = GetAnimatedSubButton(content);
            button12.OverImage = GetAnimatedSubButton(content);
            button13.OverImage = GetAnimatedSubButton(content);

            //button1.ClickImage = GetAnimatedButtonPressImage(content);
            //button2.ClickImage = GetAnimatedButtonPressImage(content);
            //button3.ClickImage = GetAnimatedButtonPressImage(content);
            //button4.ClickImage = GetAnimatedButtonPressImage(content);
            //button14.ClickImage = GetAnimatedButtonPressImage(content);

            //button5.ClickImage = GetAnimatedSubButton(content);
            //button6.ClickImage = GetAnimatedSubButton(content);
            //button18.ClickImage = GetAnimatedSubButton(content);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (this.showMarketPlace == true && !Guide.IsVisible)
            {
                Guide.ShowMarketplace(this.marketIndex);
                this.showMarketPlace = false;
            }

            this.panel3.Visible = Guide.IsTrialMode;

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }



        void InitializeComponent()
        {
            this.menuGroup1 = new Sxe.Engine.UI.Menu.MenuGroup();
            this.button1 = new Sxe.Engine.UI.Button();
            this.button2 = new Sxe.Engine.UI.Button();
            this.button3 = new Sxe.Engine.UI.Button();
            this.button14 = new Sxe.Engine.UI.Button();
            this.button4 = new Sxe.Engine.UI.Button();
            this.menuGroup2 = new Sxe.Engine.UI.Menu.MenuGroup();
            this.button5 = new Sxe.Engine.UI.Button();
            this.button6 = new Sxe.Engine.UI.Button();
            this.button18 = new Sxe.Engine.UI.Button();
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel4 = new Sxe.Engine.UI.Panel();
            this.menuGroup3 = new Sxe.Engine.UI.Menu.MenuGroup();
            this.button11 = new Sxe.Engine.UI.Button();
            this.button12 = new Sxe.Engine.UI.Button();
            this.button13 = new Sxe.Engine.UI.Button();
            this.button15 = new Sxe.Engine.UI.Button();
            this.button16 = new Sxe.Engine.UI.Button();
            this.button17 = new Sxe.Engine.UI.Button();
            this.button7 = new Sxe.Engine.UI.Button();
            this.button8 = new Sxe.Engine.UI.Button();
            this.button9 = new Sxe.Engine.UI.Button();
            this.button10 = new Sxe.Engine.UI.Button();
            this.lightningBackgroundPanel1 = new Quarx.LightningBackgroundPanel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.gamerSignInBox1 = new Quarx.GamerSignInBox();
            this.signInModule1 = new Sxe.Engine.SignInModule();
            this.panel3 = new Sxe.Engine.UI.Panel();
            this.helpTicker1 = new Quarx.HelpTicker();
            // 
            // menuGroup1
            // 
            this.menuGroup1.BackgroundPath = "Menu\\menubase";
            this.menuGroup1.ControlType = Sxe.Engine.UI.Menu.MenuControlType.Vertical;
            this.menuGroup1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.menuGroup1.Location = new Microsoft.Xna.Framework.Point(80, 275);
            this.menuGroup1.Panels.Add(this.button1);
            this.menuGroup1.Panels.Add(this.button2);
            this.menuGroup1.Panels.Add(this.button3);
            this.menuGroup1.Panels.Add(this.button14);
            this.menuGroup1.Panels.Add(this.button4);
            this.menuGroup1.Parent = this;
            this.menuGroup1.Size = new Microsoft.Xna.Framework.Point(290, 280);
            this.menuGroup1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.menuGroup1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.menuGroup1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(-200, 0);
            this.menuGroup1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // button1
            // 
            this.button1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.button1.BackgroundPath = null;
            this.button1.ClickImagePath = "buttonpressed";
            this.button1.DefaultImagePath = "buttonnormal";
            this.button1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button1.FontName = null;
            this.button1.FontPath = null;
            this.button1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button1.Location = new Microsoft.Xna.Framework.Point(72, 22);
            this.button1.OverCue = "button_move";
            this.button1.OverImagePath = "buttonhighlighted";
            this.button1.Parent = this.menuGroup1;
            this.button1.PressCue = "button_press";
            this.button1.Size = new Microsoft.Xna.Framework.Point(192, 53);
            this.button1.Text = null;
            this.button1.TextImagePath = "singleplayer";
            this.button1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button1.ButtonOver += new System.EventHandler(this.button1_ButtonOver);
            this.button1.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button1_ButtonPressed);
            // 
            // button2
            // 
            this.button2.BackgroundPath = null;
            this.button2.ClickImagePath = "buttonpressed";
            this.button2.DefaultImagePath = "buttonnormal";
            this.button2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button2.FontName = null;
            this.button2.FontPath = null;
            this.button2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button2.Location = new Microsoft.Xna.Framework.Point(69, 70);
            this.button2.OverCue = "button_move";
            this.button2.OverImagePath = "buttonhighlighted";
            this.button2.Parent = this.menuGroup1;
            this.button2.PressCue = "button_press";
            this.button2.Size = new Microsoft.Xna.Framework.Point(192, 53);
            this.button2.Text = null;
            this.button2.TextImagePath = "multiplayer";
            this.button2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button2.ButtonOver += new System.EventHandler(this.button2_ButtonOver);
            this.button2.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button2_ButtonPressed);
            // 
            // button3
            // 
            this.button3.BackgroundPath = null;
            this.button3.ClickImagePath = "buttonpressed";
            this.button3.DefaultImagePath = "buttonnormal";
            this.button3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button3.FontName = null;
            this.button3.FontPath = null;
            this.button3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button3.Location = new Microsoft.Xna.Framework.Point(71, 120);
            this.button3.OverCue = "button_move";
            this.button3.OverImagePath = "buttonhighlighted";
            this.button3.Parent = this.menuGroup1;
            this.button3.PressCue = "button_press";
            this.button3.Size = new Microsoft.Xna.Framework.Point(192, 54);
            this.button3.Text = null;
            this.button3.TextImagePath = "howtoplay";
            this.button3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button3.ButtonOver += new System.EventHandler(this.button3_ButtonOver);
            this.button3.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button3_ButtonPressed);
            // 
            // button14
            // 
            this.button14.BackgroundPath = null;
            this.button14.ClickImagePath = "buttonpressed";
            this.button14.DefaultImagePath = "buttonnormal";
            this.button14.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button14.FontName = null;
            this.button14.FontPath = null;
            this.button14.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button14.Location = new Microsoft.Xna.Framework.Point(69, 170);
            this.button14.OverCue = null;
            this.button14.OverImagePath = "buttonhighlighted";
            this.button14.Parent = this.menuGroup1;
            this.button14.PressCue = null;
            this.button14.Size = new Microsoft.Xna.Framework.Point(192, 53);
            this.button14.Text = null;
            this.button14.TextImagePath = "highscores";
            this.button14.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button14.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button14.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button14.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button14.ButtonOver += new System.EventHandler(this.button14_ButtonOver);
            this.button14.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button14_ButtonPressed);
            // 
            // button4
            // 
            this.button4.BackgroundPath = null;
            this.button4.ClickImagePath = "buttonpressed";
            this.button4.DefaultImagePath = "buttonnormal";
            this.button4.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button4.FontName = null;
            this.button4.FontPath = null;
            this.button4.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button4.Location = new Microsoft.Xna.Framework.Point(69, 220);
            this.button4.OverCue = null;
            this.button4.OverImagePath = "buttonhighlighted";
            this.button4.Parent = this.menuGroup1;
            this.button4.PressCue = null;
            this.button4.Size = new Microsoft.Xna.Framework.Point(192, 53);
            this.button4.Text = null;
            this.button4.TextImagePath = "exit";
            this.button4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button4.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button4.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button4.ButtonOver += new System.EventHandler(this.button4_ButtonOver);
            this.button4.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button4_ButtonPressed);
            // 
            // menuGroup2
            // 
            this.menuGroup2.BackgroundPath = "singlesubbackground";
            this.menuGroup2.ControlType = Sxe.Engine.UI.Menu.MenuControlType.Vertical;
            this.menuGroup2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.menuGroup2.Location = new Microsoft.Xna.Framework.Point(360, 235);
            this.menuGroup2.Panels.Add(this.button5);
            this.menuGroup2.Panels.Add(this.button6);
            this.menuGroup2.Panels.Add(this.button18);
            this.menuGroup2.Parent = this;
            this.menuGroup2.Size = new Microsoft.Xna.Framework.Point(204, 222);
            this.menuGroup2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.menuGroup2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.2000000");
            this.menuGroup2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.menuGroup2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.2000000");
            this.menuGroup2.Visible = false;
            this.menuGroup2.MenuCancel += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.menuGroup2_MenuCancel);
            // 
            // button5
            // 
            this.button5.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.button5.BackgroundPath = null;
            this.button5.ClickImagePath = "subbuttonpressed";
            this.button5.DefaultImagePath = "subbuttonnormal";
            this.button5.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button5.FontName = null;
            this.button5.FontPath = null;
            this.button5.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button5.Location = new Microsoft.Xna.Framework.Point(35, 40);
            this.button5.OverCue = "button_move";
            this.button5.OverImagePath = "subbuttonhighlighted";
            this.button5.Parent = this.menuGroup2;
            this.button5.PressCue = "button_press";
            this.button5.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button5.Text = null;
            this.button5.TextImagePath = "quickgame";
            this.button5.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button5.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button5.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button5.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button5.ButtonOver += new System.EventHandler(this.button5_ButtonOver);
            this.button5.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button5_ButtonPressed);
            // 
            // button6
            // 
            this.button6.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.button6.BackgroundPath = null;
            this.button6.ClickImagePath = "subbuttonpressed";
            this.button6.DefaultImagePath = "subbuttonnormal";
            this.button6.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button6.FontName = null;
            this.button6.FontPath = null;
            this.button6.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button6.Location = new Microsoft.Xna.Framework.Point(33, 88);
            this.button6.OverCue = "button_move";
            this.button6.OverImagePath = "subbuttonhighlighted";
            this.button6.Parent = this.menuGroup2;
            this.button6.PressCue = "button_press";
            this.button6.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button6.Text = null;
            this.button6.TextImagePath = "customgame";
            this.button6.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button6.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button6.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button6.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button6.ButtonOver += new System.EventHandler(this.button6_ButtonOver);
            this.button6.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button6_ButtonPressed);
            // 
            // button18
            // 
            this.button18.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.button18.BackgroundPath = null;
            this.button18.ClickImagePath = "subbuttonpressed";
            this.button18.DefaultImagePath = "subbuttonnormal";
            this.button18.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button18.FontName = null;
            this.button18.FontPath = null;
            this.button18.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button18.Location = new Microsoft.Xna.Framework.Point(33, 136);
            this.button18.OverCue = "button_move";
            this.button18.OverImagePath = "subbuttonhighlighted";
            this.button18.Parent = this.menuGroup2;
            this.button18.PressCue = "button_press";
            this.button18.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button18.Text = null;
            this.button18.TextImagePath = "puzzle";
            this.button18.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button18.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button18.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button18.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button18.ButtonOver += new System.EventHandler(this.button18_ButtonOver);
            this.button18.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button18_ButtonPressed);
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "buttons";
            this.panel1.Location = new Microsoft.Xna.Framework.Point(130, 245);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(161, 70);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-100, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            // 
            // panel4
            // 
            this.panel4.BackgroundPath = "quarx";
            this.panel4.Location = new Microsoft.Xna.Framework.Point(282, 18);
            this.panel4.Parent = this;
            this.panel4.Size = new Microsoft.Xna.Framework.Point(284, 166);
            this.panel4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // menuGroup3
            // 
            this.menuGroup3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.menuGroup3.BackgroundPath = "quicksubbackground";
            this.menuGroup3.ControlType = Sxe.Engine.UI.Menu.MenuControlType.Vertical;
            this.menuGroup3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.menuGroup3.Location = new Microsoft.Xna.Framework.Point(550, 235);
            this.menuGroup3.Panels.Add(this.button11);
            this.menuGroup3.Panels.Add(this.button12);
            this.menuGroup3.Panels.Add(this.button13);
            this.menuGroup3.Parent = this;
            this.menuGroup3.Size = new Microsoft.Xna.Framework.Point(223, 256);
            this.menuGroup3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.menuGroup3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.2000000");
            this.menuGroup3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.menuGroup3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.2000000");
            this.menuGroup3.Visible = false;
            this.menuGroup3.MenuCancel += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.menuGroup3_MenuCancel);
            // 
            // button11
            // 
            this.button11.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.button11.BackgroundPath = null;
            this.button11.ClickImagePath = "subbuttonpressed";
            this.button11.DefaultImagePath = "subbuttonnormal";
            this.button11.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button11.FontName = null;
            this.button11.FontPath = null;
            this.button11.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button11.Location = new Microsoft.Xna.Framework.Point(36, 17);
            this.button11.OverCue = "button_move";
            this.button11.OverImagePath = "subbuttonhighlighted";
            this.button11.Parent = this.menuGroup3;
            this.button11.PressCue = "button_press";
            this.button11.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button11.Text = null;
            this.button11.TextImagePath = "easy";
            this.button11.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button11.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button11.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button11.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button11.ButtonOver += new System.EventHandler(this.button11_ButtonOver);
            this.button11.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button11_ButtonPressed);
            // 
            // button12
            // 
            this.button12.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.button12.BackgroundPath = null;
            this.button12.ClickImagePath = "subbuttonpressed";
            this.button12.DefaultImagePath = "subbuttonnormal";
            this.button12.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button12.FontName = null;
            this.button12.FontPath = null;
            this.button12.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button12.Location = new Microsoft.Xna.Framework.Point(59, 99);
            this.button12.OverCue = "button_move";
            this.button12.OverImagePath = "subbuttonhighlighted";
            this.button12.Parent = this.menuGroup3;
            this.button12.PressCue = "button_press";
            this.button12.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button12.Text = null;
            this.button12.TextImagePath = "medium";
            this.button12.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button12.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button12.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button12.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button12.ButtonOver += new System.EventHandler(this.button12_ButtonOver);
            this.button12.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button12_ButtonPressed);
            // 
            // button13
            // 
            this.button13.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.button13.BackgroundPath = null;
            this.button13.ClickImagePath = "subbuttonpressed";
            this.button13.DefaultImagePath = "subbuttonnormal";
            this.button13.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button13.FontName = null;
            this.button13.FontPath = null;
            this.button13.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button13.Location = new Microsoft.Xna.Framework.Point(32, 185);
            this.button13.OverCue = "button_move";
            this.button13.OverImagePath = "subbuttonhighlighted";
            this.button13.Parent = this.menuGroup3;
            this.button13.PressCue = "button_press";
            this.button13.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button13.Text = null;
            this.button13.TextImagePath = "hard";
            this.button13.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button13.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button13.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button13.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button13.ButtonOver += new System.EventHandler(this.button13_ButtonOver);
            this.button13.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button13_ButtonPressed);
            // 
            // button15
            // 
            this.button15.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.button15.BackgroundPath = null;
            this.button15.ClickImagePath = "subbuttonpressed";
            this.button15.DefaultImagePath = "subbuttonnormal";
            this.button15.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button15.FontName = null;
            this.button15.FontPath = null;
            this.button15.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button15.Location = new Microsoft.Xna.Framework.Point(23, 21);
            this.button15.OverCue = "button_move";
            this.button15.OverImagePath = "subbuttonhighlighted";
            this.button15.Parent = null;
            this.button15.PressCue = "button_press";
            this.button15.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button15.Text = null;
            this.button15.TextImagePath = "quickgame";
            this.button15.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button15.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button15.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button15.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button15.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button15_ButtonPressed);
            // 
            // button16
            // 
            this.button16.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.button16.BackgroundPath = null;
            this.button16.ClickImagePath = "subbuttonpressed";
            this.button16.DefaultImagePath = "subbuttonnormal";
            this.button16.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button16.FontName = null;
            this.button16.FontPath = null;
            this.button16.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button16.Location = new Microsoft.Xna.Framework.Point(25, 69);
            this.button16.OverCue = "button_move";
            this.button16.OverImagePath = "subbuttonhighlighted";
            this.button16.Parent = null;
            this.button16.PressCue = "button_press";
            this.button16.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button16.Text = null;
            this.button16.TextImagePath = "quickgame";
            this.button16.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button16.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button16.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button16.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button16.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button16_ButtonPressed);
            // 
            // button17
            // 
            this.button17.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.button17.BackgroundPath = null;
            this.button17.ClickImagePath = "subbuttonpressed";
            this.button17.DefaultImagePath = "subbuttonnormal";
            this.button17.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button17.FontName = null;
            this.button17.FontPath = null;
            this.button17.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button17.Location = new Microsoft.Xna.Framework.Point(25, 122);
            this.button17.OverCue = "button_move";
            this.button17.OverImagePath = "subbuttonhighlighted";
            this.button17.Parent = null;
            this.button17.PressCue = "button_press";
            this.button17.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button17.Text = null;
            this.button17.TextImagePath = "quickgame";
            this.button17.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button17.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button17.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button17.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button17.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.button17_ButtonPressed);
            // 
            // button7
            // 
            this.button7.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.button7.BackgroundPath = null;
            this.button7.ClickImagePath = "subbuttonpressed";
            this.button7.DefaultImagePath = "subbuttonnormal";
            this.button7.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button7.FontName = null;
            this.button7.FontPath = null;
            this.button7.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button7.Location = new Microsoft.Xna.Framework.Point(33, 53);
            this.button7.OverCue = null;
            this.button7.OverImagePath = "subbuttonhighlighted";
            this.button7.Parent = null;
            this.button7.PressCue = null;
            this.button7.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button7.Text = null;
            this.button7.TextImagePath = "quickgame";
            this.button7.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button7.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button7.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button7.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // button8
            // 
            this.button8.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.button8.BackgroundPath = null;
            this.button8.ClickImagePath = "subbuttonpressed";
            this.button8.DefaultImagePath = "subbuttonnormal";
            this.button8.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button8.FontName = null;
            this.button8.FontPath = null;
            this.button8.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button8.Location = new Microsoft.Xna.Framework.Point(32, 52);
            this.button8.OverCue = null;
            this.button8.OverImagePath = "subbuttonhighlighted";
            this.button8.Parent = null;
            this.button8.PressCue = null;
            this.button8.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button8.Text = null;
            this.button8.TextImagePath = "quickgame";
            this.button8.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button8.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button8.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button8.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // button9
            // 
            this.button9.BackgroundPath = null;
            this.button9.ClickImagePath = "subbuttonpressed";
            this.button9.DefaultImagePath = "subbuttonnormal";
            this.button9.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button9.FontName = null;
            this.button9.FontPath = null;
            this.button9.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button9.Location = new Microsoft.Xna.Framework.Point(33, 112);
            this.button9.OverCue = null;
            this.button9.OverImagePath = "subbuttonhighlighted";
            this.button9.Parent = null;
            this.button9.PressCue = null;
            this.button9.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button9.Text = null;
            this.button9.TextImagePath = "customgame";
            this.button9.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button9.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button9.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button9.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // button10
            // 
            this.button10.BackgroundPath = null;
            this.button10.ClickImagePath = "subbuttonpressed";
            this.button10.DefaultImagePath = "subbuttonnormal";
            this.button10.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button10.FontName = null;
            this.button10.FontPath = null;
            this.button10.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button10.Location = new Microsoft.Xna.Framework.Point(33, 112);
            this.button10.OverCue = null;
            this.button10.OverImagePath = "subbuttonhighlighted";
            this.button10.Parent = null;
            this.button10.PressCue = null;
            this.button10.Size = new Microsoft.Xna.Framework.Point(140, 50);
            this.button10.Text = null;
            this.button10.TextImagePath = "customgame";
            this.button10.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button10.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button10.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button10.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // lightningBackgroundPanel1
            // 
            this.lightningBackgroundPanel1.AtomBrightImagePath = "Menu\\atomsbright";
            this.lightningBackgroundPanel1.AtomNormalImagePath = "Menu\\atomsnormal";
            this.lightningBackgroundPanel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.lightningBackgroundPanel1.BackgroundPath = null;
            this.lightningBackgroundPanel1.LightningPath1 = "Menu\\lightning";
            this.lightningBackgroundPanel1.LightningPath2 = "Menu\\lightning2";
            this.lightningBackgroundPanel1.LightningPath3 = "Menu\\lightning3";
            this.lightningBackgroundPanel1.LightningPath4 = "Menu\\lightning4";
            this.lightningBackgroundPanel1.Location = new Microsoft.Xna.Framework.Point(0, -1);
            this.lightningBackgroundPanel1.Parent = this;
            this.lightningBackgroundPanel1.Size = new Microsoft.Xna.Framework.Point(960, 600);
            this.lightningBackgroundPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.lightningBackgroundPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel2
            // 
            this.panel2.BackgroundPath = "menugamertagbox";
            this.panel2.Location = new Microsoft.Xna.Framework.Point(452, 75);
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(138, 50);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // gamerSignInBox1
            // 
            this.gamerSignInBox1.BackgroundPath = "signin\\signinback";
            this.gamerSignInBox1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.gamerSignInBox1.Location = new Microsoft.Xna.Framework.Point(660, 49);
            this.gamerSignInBox1.Parent = this;
            this.gamerSignInBox1.Size = new Microsoft.Xna.Framework.Point(200, 150);
            this.gamerSignInBox1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.gamerSignInBox1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.gamerSignInBox1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.gamerSignInBox1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // signInModule1
            // 
            this.signInModule1.BackgroundPath = null;
            this.signInModule1.Location = new Microsoft.Xna.Framework.Point(148, 50);
            this.signInModule1.Parent = this;
            this.signInModule1.Size = new Microsoft.Xna.Framework.Point(50, 50);
            this.signInModule1.TemporaryOnly = false;
            this.signInModule1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.signInModule1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel3
            // 
            this.panel3.BackgroundPath = "buynow";
            this.panel3.Location = new Microsoft.Xna.Framework.Point(678, 187);
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(117, 50);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // helpTicker1
            // 
            this.helpTicker1.BackgroundPath = null;
            this.helpTicker1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.helpTicker1.Location = new Microsoft.Xna.Framework.Point(389, 511);
            this.helpTicker1.Parent = this;
            this.helpTicker1.Size = new Microsoft.Xna.Framework.Point(491, 25);
            this.helpTicker1.TickerColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.helpTicker1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.helpTicker1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.helpTicker1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.helpTicker1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // MenuScreen
            // 
            this.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.BackgroundPath = "Menu\\back";
            this.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.MusicCue = "MenuMusic";
            this.Panels.Add(this.lightningBackgroundPanel1);
            this.Panels.Add(this.panel3);
            this.Panels.Add(this.panel4);
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.menuGroup1);
            this.Panels.Add(this.menuGroup2);
            this.Panels.Add(this.menuGroup3);
            this.Panels.Add(this.gamerSignInBox1);
            this.Panels.Add(this.signInModule1);
            this.Panels.Add(this.helpTicker1);
            this.Size = new Microsoft.Xna.Framework.Point(960, 600);
            this.TransitionOffTime = System.TimeSpan.Parse("00:00:00.2500000");
            this.TransitionOnTime = System.TimeSpan.Parse("00:00:00.2500000");

        }



        //        this.Panels.Add(this.lightningBackgroundPanel1);
        //this.Panels.Add(this.menuGroup1);
        //this.Panels.Add(this.panel4);
        //this.Panels.Add(this.menuGroup2);
        //this.Panels.Add(this.menuGroup3);
        //this.Panels.Add(this.gamerTagBox1);

        //This happens when singleplayer button is pressed
        private void button1_ButtonPressed(object sender, ButtonPressEventArgs e)
        {

            //this.GameScreenService.AddScreen(new TestPlayerOptions());
            menuGroup2.Show();
            this.ActiveControl = menuGroup2;
        }


        private void button5_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            menuGroup3.Show();
            this.ActiveControl = menuGroup3;
        }

        private void menuGroup2_MenuCancel(object sender, ButtonPressEventArgs e)
        {
            menuGroup2.Hide();
            this.GameScreenService.Audio.PlayCue("menu_back");
            this.ActiveControl = menuGroup1;
        }

        private void menuGroup3_MenuCancel(object sender, ButtonPressEventArgs e)
        {
            menuGroup3.Hide();
            this.GameScreenService.Audio.PlayCue("menu_back");
            this.ActiveControl = menuGroup2;
        }

        private void button6_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            //SingleOptionsScreen optionsScreen = new SingleOptionsScreen();
            //optionsScreen.GameScreen = new SinglePlayerGame();
            //optionsScreen.GameScreen.ClickPlayerIndex = e.PlayerIndex;
            TestPlayerOptions screen = new TestPlayerOptions();
            screen.SinglePlayerIndex = e.PlayerIndex;
            this.GameScreenService.AddScreen(screen);
            //GameScreenService.AddScreen(screen);
        }

        private void button11_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            SinglePlayerGame screen = new SinglePlayerGame();
            //screen.Settings = QuarxGameSettings.Easy;
            screen.ClickPlayerIndex = e.PlayerIndex;

            //Easy settings here
            screen.Settings = RoundSettings.CreateFromQuarxSettings(QuarxGameSettings.Easy, e.PlayerIndex);
            string music = GetRandomMusic();
            //screen.Settings.GlobalSettings.MusicType = music;
            screen.MusicCue = music;

            GameScreenService.AddScreen(screen);
        }

        private void button2_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            this.GameScreenService.AddScreen(new TestPlayerOptions());
            //menuGroup4.Show();
            //this.ActiveControl = menuGroup4;
            //SingleOptionsScreen screen = new SingleOptionsScreen();
            //screen.GameScreen = new TwoPlayerGame();
            //GameScreenService.AddScreen(screen);
        }

        private void button3_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            HowToPlayScreen screen = new HowToPlayScreen();
            GameScreenService.AddScreen(screen);
        }

        private void button4_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            GameScreenService.Game.Exit();
        }

        private void button12_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            SinglePlayerGame mediumSinglePlayer = new SinglePlayerGame();
            mediumSinglePlayer.ClickPlayerIndex = e.PlayerIndex;

            mediumSinglePlayer.Settings = RoundSettings.CreateFromQuarxSettings(QuarxGameSettings.Medium, e.PlayerIndex);

            string music = GetRandomMusic();
            //mediumSinglePlayer.Settings.GlobalSettings.MusicType = music;
            mediumSinglePlayer.MusicCue = music;

            //mediumSinglePlayer.Settings = QuarxGameSettings.Medium;
            GameScreenService.AddScreen(mediumSinglePlayer);
        }

        private void button13_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            SinglePlayerGame hardSinglePlayer = new SinglePlayerGame();
            //hardSinglePlayer.Settings = QuarxGameSettings.Hard;
            hardSinglePlayer.Settings = RoundSettings.CreateFromQuarxSettings(QuarxGameSettings.Hard, e.PlayerIndex);

            string music = GetRandomMusic();
            //hardSinglePlayer.Settings.GlobalSettings.MusicType = music;
            hardSinglePlayer.MusicCue = music;


            GameScreenService.AddScreen(hardSinglePlayer);
        }

        private void button14_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            GameScreenService.AddScreen(new HighscoreScreen());
        }



        private void button15_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            TestPlayerOptions screen = new TestPlayerOptions();
            screen.SinglePlayerIndex = e.PlayerIndex;
            this.GameScreenService.AddScreen(screen);
            //SingleOptionsScreen screen = new SingleOptionsScreen();
            //screen.GameScreen = new TwoPlayerGame();
            //GameScreenService.AddScreen(screen);
        }

        private void button16_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            SingleOptionsScreen screen = new SingleOptionsScreen();
            screen.GameScreen = new ThreePlayerGame();
            GameScreenService.AddScreen(screen);
        }

        private void button17_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            SingleOptionsScreen screen = new SingleOptionsScreen();
            screen.GameScreen = new FourPlayerGame();
            GameScreenService.AddScreen(screen);
        }

        private void button18_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            if (Guide.IsTrialMode)
            {
                XboxMessageInfo buyMessage = new XboxMessageInfo(this.buyOptions)
                {
                    Message = "Sorry, this option is not available in trial mode.\n\n Would you like to go to the marketplace to purchase the full version of Quarx?",
                    MessageBoxIcon = MessageBoxIcon.Warning,
                    PlayerIndex = (PlayerIndex)e.PlayerIndex,
                    Title = "Buy Me!"
                };

                buyMessage.Completed += BuyCallback;

                this.GameScreenService.XboxMessage.Add(buyMessage);

            }
            else
            {

                PuzzleSelectionScreen puzzleSelection = new PuzzleSelectionScreen();
                puzzleSelection.Gamer = AnarchyGamer.Gamers.GetGamerByPlayerIndex(e.PlayerIndex);
                GameScreenService.AddScreen(puzzleSelection);
            }
        }

        private void BuyCallback(object sender, XboxMessageEventArgs args)
        {
            if (args.Result.HasValue)
            {
                if (args.Result.Value == 0)
                {
                    XboxMessageInfo info = sender as XboxMessageInfo;
                    if (info != null)
                    {
              
                        GameScreenService.XboxMessage.ShowMarketPlace(info.PlayerIndex);
                        //this.showMarketPlace = true;
                        //this.marketIndex = info.PlayerIndex;
                    }
                }
            }
        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            if (Guide.IsTrialMode)
            {
                ControllerChangedEventArgs controllerEventArgs = inputEvent as ControllerChangedEventArgs;
                if (controllerEventArgs != null)
                {
                    if (controllerEventArgs.IsSignedIn && this.Input.Controllers[(controllerEventArgs.PlayerIndex)].IsKeyJustPressed("y_button"))
                    {
                        GameScreenService.XboxMessage.ShowMarketPlace((PlayerIndex)controllerEventArgs.PlayerIndex);
                        //this.showMarketPlace = true;
                        //this.marketIndex = (PlayerIndex)controllerEventArgs.PlayerIndex;
                    }
                }

            }

            return base.HandleEvent(inputEvent);
        }

        private string GetRandomMusic()
        {
            Random rand = new Random();
            if (rand.NextDouble() < 0.5)
                return Resources.Alienz;
            else
                return Resources.Toped;
        }


        private AnimatedImage GetAnimatedButtonPressImage(ContentManager content)
        {
            return AnimatedImage.Load(content, "buttonhighlighted", 0, 3, 150);
        }

        private AnimatedImage GetAnimatedSubButton(ContentManager content)
        {
            return AnimatedImage.Load(content, "subbuttonhighlighted", 0, 3, 150);
        }

        private void button1_ButtonOver(object sender, EventArgs e)
        {
            this.helpTicker1.Add(this.helpSinglePlayer);
        }

        private void button2_ButtonOver(object sender, EventArgs e)
        {
            this.helpTicker1.Add(this.helpMultiplayer);
        }

        private void button3_ButtonOver(object sender, EventArgs e)
        {
            this.helpTicker1.Add(this.helpHowToPlay);
        }

        private void button14_ButtonOver(object sender, EventArgs e)
        {
            this.helpTicker1.Add(this.helpHighScores);
        }

        private void button4_ButtonOver(object sender, EventArgs e)
        {
            this.helpTicker1.Add(this.helpExit);
        }

        private void button5_ButtonOver(object sender, EventArgs e)
        {
            this.helpTicker1.Add(this.helpQuick);
        }

        private void button6_ButtonOver(object sender, EventArgs e)
        {
            this.helpTicker1.Add(this.helpCustom);
        }

        private void button18_ButtonOver(object sender, EventArgs e)
        {
            this.helpPuzzle.Text = "Rack your brain and solve some diabolical puzzles!";
           
            if(Guide.IsTrialMode)
                this.helpPuzzle.Text += " [Not available in Trial Mode]";


            this.helpTicker1.Add(this.helpPuzzle);
        }

        private void button11_ButtonOver(object sender, EventArgs e)
        {
            //this.helpTicker1.Add(this.helpEasy);
        }

        private void button12_ButtonOver(object sender, EventArgs e)
        {
            //this.helpTicker1.Add(this.helpMedium);
        }

        private void button13_ButtonOver(object sender, EventArgs e)
        {
            //this.helpTicker1.Add(this.helpHard);
        }

    }
}