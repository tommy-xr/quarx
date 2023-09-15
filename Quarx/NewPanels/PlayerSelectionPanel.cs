using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public class PlayerSelectionPanel : CompositePanel
    {
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;

        private Panel[] panels;
        private UIImage[] images;

        private List<int> playerIndices = new List<int>();

        public int Count
        {
            get { return playerIndices.Count; }
        }

        public int GetItem(int index)
        {
            return playerIndices[index];
        }

        public void AddPlayer(int index)
        {
         

            if (index >= 0 && index < panels.Length && !playerIndices.Contains(index))
                playerIndices.Add(index);

            UpdatePanels();
        }

        public void RemovePlayer(int index)
        {

            if (playerIndices.Contains(index))
                playerIndices.Remove(index);

            UpdatePanels();
        }

        public void Clear()
        {
            playerIndices.Clear();
            UpdatePanels();
        }

        void UpdatePanels()
        {
            for (int i = 0; i < panels.Length; i++)
                panels[i].Visible = false;

            int counter = 0;
            for (int i = 0; i < playerIndices.Count; i++)
            {
                panels[counter].Visible = true;
                panels[counter].Image = images[playerIndices[i]];
                counter++;
            }

            //for (int i = 0; i < panels.Length; i++)
            //{
            //    if (playerIndices.Contains(i))
            //        panels[i].Visible = true;
            //    else
            //        panels[i].Visible = false;
            //}
        }

        public PlayerSelectionPanel()
        {
            InitializeComponent();

            panels = new Panel[4];
            panels[0] = panel1;
            panels[1] = panel2;
            panels[2] = panel3;
            panels[3] = panel4;

            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].Visible = false;
            }
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            const string first = "Options\\indicator1";
            const string second = "Options\\indicator2";
            const string third = "Options\\indicator3";
            const string fourth = "Options\\indicator4";

            images = new UIImage[4];
            images[0] = new OscillatingImage(content.Load<Texture2D>(first));
            images[1] = new OscillatingImage(content.Load<Texture2D>(second));
            images[2] = new OscillatingImage(content.Load<Texture2D>(third));
            images[3] = new OscillatingImage(content.Load<Texture2D>(fourth));
        }

        private void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.panel3 = new Sxe.Engine.UI.Panel();
            this.panel4 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "Options\\indicator1";
            this.panel1.CanDrag = false;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(1, 6);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(40, 15);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel2
            // 
            this.panel2.BackgroundPath = "Options\\indicator2";
            this.panel2.CanDrag = false;
            this.panel2.Location = new Microsoft.Xna.Framework.Point(2, 24);
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(40, 15);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel3
            // 
            this.panel3.BackgroundPath = "Options\\indicator3";
            this.panel3.CanDrag = false;
            this.panel3.Location = new Microsoft.Xna.Framework.Point(2, 42);
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(40, 15);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel4
            // 
            this.panel4.BackgroundPath = "Options\\indicator4";
            this.panel4.CanDrag = false;
            this.panel4.Location = new Microsoft.Xna.Framework.Point(2, 58);
            this.panel4.Parent = this;
            this.panel4.Size = new Microsoft.Xna.Framework.Point(40, 15);
            this.panel4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // PlayerSelectionPanel
            // 
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.panel3);
            this.Panels.Add(this.panel4);
            this.Size = new Microsoft.Xna.Framework.Point(50, 75);

        }
    }
}
