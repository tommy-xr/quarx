using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine;

namespace Quarx
{
    public class GamerSignInBox : CompositePanel
    {
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;

        Panel[] panels = new Panel[4];
        Label[] labels = new Label[4];
        Panel[] gamerIcons = new Panel[4];

        UIImage[] controllers = new UIImage[4];
        UIImage defaultText;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;


        List<int> signedInGamers = new List<int>();

        public GamerSignInBox()
        {
            InitializeComponent();

            panels[0] = panel1;
            panels[1] = panel2;
            panels[2] = panel3;
            panels[3] = panel4;

            labels[0] = label1;
            labels[1] = label2;
            labels[2] = label3;
            labels[3] = label4;

            gamerIcons[0] = panel5;
            gamerIcons[1] = panel6;
            gamerIcons[2] = panel7;
            gamerIcons[3] = panel8;

        }

        void ResetGamerbox()
        {
            int currentSlot = 0;

            signedInGamers.Clear();
            //Loop through all the current gamers, get their info
            for (int i = 0; i < AnarchyGamer.Gamers.Count; i++)
            {
                if (currentSlot > 3)
                    return;

                

                IAnarchyGamer gamer = AnarchyGamer.Gamers[i];
                if (gamer.IsPlayer)
                {
                    signedInGamers.Add(AnarchyGamer.Gamers[i].Index);
                    labels[currentSlot].Caption = gamer.GamerTag;
                    labels[currentSlot].Image = null;
                    panels[currentSlot].Image = GetImageFromIndex(gamer.Index);
                    gamerIcons[currentSlot].Image = gamer.GamerIcon;
                    currentSlot++;
                }
                
            }

            //Now, loop through remaining players
            for (int i = 0; i < Input.Controllers.Count; i++)
            {
                if (currentSlot > 3)
                    return;

                if (!signedInGamers.Contains(i) && Input.Controllers[i].Connected)
                {
                    labels[currentSlot].Caption = "";
                    labels[currentSlot].Image = defaultText;
                    panels[currentSlot].Image = GetImageFromIndex(i);
                    gamerIcons[currentSlot].Image = null;
                    currentSlot++;
                }
            }

            //Clear out remaining slots, if any
            for (int i = currentSlot; i < 4; i++)
            {
                labels[i].Caption = "";
                labels[i].Image = null;
                panels[i].Image = null;
                gamerIcons[i].Image = null;
            }


        }

        private UIImage GetImageFromIndex(int controllerIndex)
        {
            if (controllerIndex < 0 || controllerIndex > 3)
                return null;

            return this.controllers[controllerIndex];

          
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ResetGamerbox();

            base.Update(gameTime);
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            for (int i = 1; i <= 4; i++)
            {
                controllers[i - 1] = LoadImage(content, "signin\\signin" + i.ToString());
            }

            defaultText = LoadImage(content, "signin\\signintext");
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.panel3 = new Sxe.Engine.UI.Panel();
            this.panel4 = new Sxe.Engine.UI.Panel();
            this.label1 = new Sxe.Engine.UI.Label();
            this.label2 = new Sxe.Engine.UI.Label();
            this.label3 = new Sxe.Engine.UI.Label();
            this.label4 = new Sxe.Engine.UI.Label();
            this.panel5 = new Sxe.Engine.UI.Panel();
            this.panel6 = new Sxe.Engine.UI.Panel();
            this.panel7 = new Sxe.Engine.UI.Panel();
            this.panel8 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "signin\\signin1";
            this.panel1.CanDrag = false;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(8, 10);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(25, 25);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel2
            // 
            this.panel2.BackgroundPath = "signin\\signin1";
            this.panel2.CanDrag = false;
            this.panel2.Location = new Microsoft.Xna.Framework.Point(8, 40);
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(25, 25);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel3
            // 
            this.panel3.BackgroundPath = "signin\\signin1";
            this.panel3.CanDrag = false;
            this.panel3.Location = new Microsoft.Xna.Framework.Point(8, 70);
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(25, 25);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel4
            // 
            this.panel4.BackgroundPath = "signin\\signin1";
            this.panel4.CanDrag = false;
            this.panel4.Location = new Microsoft.Xna.Framework.Point(8, 100);
            this.panel4.Parent = this;
            this.panel4.Size = new Microsoft.Xna.Framework.Point(25, 25);
            this.panel4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // label1
            // 
            this.label1.BackgroundPath = null;
            this.label1.CanDrag = false;
            this.label1.Caption = "";
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Calibri11";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label1.Location = new Microsoft.Xna.Framework.Point(80, 10);
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(110, 30);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            // 
            // label2
            // 
            this.label2.BackgroundPath = null;
            this.label2.CanDrag = false;
            this.label2.Caption = "";
            this.label2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.FontPath = "Calibri11";
            this.label2.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label2.Location = new Microsoft.Xna.Framework.Point(80, 40);
            this.label2.Parent = this;
            this.label2.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label2.Size = new Microsoft.Xna.Framework.Point(110, 30);
            this.label2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            // 
            // label3
            // 
            this.label3.BackgroundPath = null;
            this.label3.CanDrag = false;
            this.label3.Caption = "";
            this.label3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label3.FontPath = "Calibri11";
            this.label3.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label3.Location = new Microsoft.Xna.Framework.Point(80, 70);
            this.label3.Parent = this;
            this.label3.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label3.Size = new Microsoft.Xna.Framework.Point(110, 30);
            this.label3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label3.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            // 
            // label4
            // 
            this.label4.BackgroundPath = null;
            this.label4.CanDrag = false;
            this.label4.Caption = "";
            this.label4.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label4.FontPath = "Calibri11";
            this.label4.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label4.Location = new Microsoft.Xna.Framework.Point(80, 100);
            this.label4.Parent = this;
            this.label4.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label4.Size = new Microsoft.Xna.Framework.Point(110, 30);
            this.label4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label4.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            // 
            // panel5
            // 
            this.panel5.BackgroundPath = "signin\\signin1";
            this.panel5.CanDrag = false;
            this.panel5.Location = new Microsoft.Xna.Framework.Point(50, 10);
            this.panel5.Parent = this;
            this.panel5.Size = new Microsoft.Xna.Framework.Point(25, 25);
            this.panel5.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel5.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel6
            // 
            this.panel6.BackgroundPath = "signin\\signin1";
            this.panel6.CanDrag = false;
            this.panel6.Location = new Microsoft.Xna.Framework.Point(50, 40);
            this.panel6.Parent = this;
            this.panel6.Size = new Microsoft.Xna.Framework.Point(25, 25);
            this.panel6.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel6.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel7
            // 
            this.panel7.BackgroundPath = "signin\\signin1";
            this.panel7.CanDrag = false;
            this.panel7.Location = new Microsoft.Xna.Framework.Point(50, 70);
            this.panel7.Parent = this;
            this.panel7.Size = new Microsoft.Xna.Framework.Point(25, 25);
            this.panel7.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel7.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel8
            // 
            this.panel8.BackgroundPath = "signin\\signin1";
            this.panel8.CanDrag = false;
            this.panel8.Location = new Microsoft.Xna.Framework.Point(50, 100);
            this.panel8.Parent = this;
            this.panel8.Size = new Microsoft.Xna.Framework.Point(25, 25);
            this.panel8.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel8.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // GamerSignInBox
            // 
            this.BackgroundPath = "signin\\signinback";
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.panel3);
            this.Panels.Add(this.panel4);
            this.Panels.Add(this.label1);
            this.Panels.Add(this.label2);
            this.Panels.Add(this.label3);
            this.Panels.Add(this.label4);
            this.Panels.Add(this.panel5);
            this.Panels.Add(this.panel6);
            this.Panels.Add(this.panel7);
            this.Panels.Add(this.panel8);
            this.Size = new Microsoft.Xna.Framework.Point(200, 150);

        }
    }
}
