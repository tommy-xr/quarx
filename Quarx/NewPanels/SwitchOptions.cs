using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine;
using Sxe.Engine.UI;
using Sxe.Engine.Input;

using Microsoft.Xna.Framework.Graphics;

namespace Quarx.NewPanels
{
    public class SwitchOptions : CompositePanel, IMenuEntry
    {
        private PlayerOptions playerOptions1;
        private BoardGamerTag boardGamerTag1;
        private Panel panel1;
        IAnarchyGamer gamer;
        int allowedPlayerIndex = -1;
        IMenuEntry currentSelection = null;
        private PlayerSelectionPanel playerSelectionPanel1;

        bool ready = false;

        public int AllowedPlayerIndex
        {
            get { return allowedPlayerIndex; }
            set { allowedPlayerIndex = value; }
        }

        public bool PlayerSelectionPanelVisible
        {
            get { return this.playerSelectionPanel1.Visible; }
            set { this.playerSelectionPanel1.Visible = value; }
        }

        public bool Ready
        {
            get { return ready; }
            set 
            { 
                ready = value;
                panel1.Visible = value;
            }
        }

        public IAnarchyGamer Gamer
        {
            get { return gamer; }
            set 
            {
                gamer = value;
                SetGamer(gamer);
            }
        }

        public int Option1
        {
            get
            {
                if (currentSelection == playerOptions1)
                    return playerOptions1.Speed;

                return 0;
            }
        }

        public int Option2
        {
            get
            {
                if (currentSelection == playerOptions1)
                    return playerOptions1.Isotopes;

                return 0;
            }
        }
        bool IMenuEntry.AllowPlayerIndex(int playerIndex)
        {
            if (currentSelection == null)
                return false;
            else if (allowedPlayerIndex == -1)
                return true;
            else if (allowedPlayerIndex == playerIndex)
                return true;

            return false;

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Check if any of our games are no longer gamers
            //If so, they can leave
            int leaveIndex = -1;
            for (int i = 0; i < this.playerSelectionPanel1.Count; i++)
            {
                int index = this.playerSelectionPanel1.GetItem(i);

                IAnarchyGamer gamer = AnarchyGamer.Gamers.GetGamerByPlayerIndex(index);
                if (gamer == null)
                    leaveIndex = index;
            }

            if (leaveIndex >= 0)
                this.Leave(leaveIndex);

            base.Update(gameTime);
        }


        //bool IMenuEntry.CanMove(MenuDirection direction)
        //{
        //    if (currentSelection == null)
        //        return true;
        //    else
        //        return currentSelection.CanMove(direction);
        //}

        bool IMenuEntry.HandleEvent(InputEventArgs args)
        {
            if (currentSelection == null)
                return false;
            else
            {
                MenuEventArgs menuEvent = args as MenuEventArgs;

                if (menuEvent.MenuEventType == MenuEventType.Select && !ready)
                {
                    Audio.PlayCue("player_ready");
                    Ready = true;
                    return true;
                }
                else if (menuEvent.MenuEventType == MenuEventType.Back && ready)
                {
                    Ready = false;
                    return true;
                }
                else if (menuEvent.MenuEventType == MenuEventType.Back && !ready)
                {
                    if (gamer != null)
                    {
                        if (!gamer.IsPlayer)
                        {
                            AnarchyGamer.Gamers.Remove(gamer);
                            return true;
                        }
                    }
                }

                if (ready)
                    return false;

                return currentSelection.HandleEvent(args);
            }
        }

        public void Leave(int index)
        {
            this.playerSelectionPanel1.RemovePlayer(index);

            //if (this.currentSelection != null)
            //{
                //Only call leave if there are no people still on here
                if(this.playerSelectionPanel1.Count <= 0)
                    this.playerOptions1.Leave(index);
            //}

        }

        void IButton.Over(int index)
        {
            //if (currentSelection != null)
                this.playerOptions1.Over(index);

            this.playerSelectionPanel1.AddPlayer(index);
        }

        void IButton.PerformClick(int playerIndex)
        {
        }

        void SetGamer(IAnarchyGamer gamer)
        {
            if (gamer == null)
            {
                currentSelection = null;
                this.boardGamerTag1.Visible = false;
                this.playerOptions1.Visible = false;
                this.BackColor = Color.White;
                this.Ready = false;

                //Make all players that were on here leave, fo sho!
                //If there was a player on here, we force them to leave
                //This clears highlighting of the text that would stay there, because if we just clear
                //the player selection panel, we aren't changing anything about the player options
                if (this.playerSelectionPanel1.Count > 0)
                    this.playerOptions1.Leave(this.playerSelectionPanel1.GetItem(0));

                this.playerSelectionPanel1.Clear();
            }
            else if (gamer.IsPlayer)
            {
                currentSelection = this.playerOptions1;
                this.boardGamerTag1.Visible = true;
                this.playerOptions1.Visible = true;
                this.playerOptions1.Setup(true);

                this.BackColor = Color.TransparentWhite;
                boardGamerTag1.Gamer = gamer;
            }
            else
            {
                //Current selection = AI shiz
                currentSelection = this.playerOptions1;
                this.boardGamerTag1.Visible = true;
                this.playerOptions1.Visible = true;
                this.playerOptions1.Setup(false);
                this.BackColor = Color.TransparentWhite;
                boardGamerTag1.Gamer = gamer;
            }
        }
    
        public SwitchOptions()
        {
            InitializeComponent();

            this.playerOptions1.Visible = false;
            panel1.Visible = false;
         
        }

        void InitializeComponent()
        {
            this.playerOptions1 = new Quarx.NewPanels.PlayerOptions();
            this.boardGamerTag1 = new Quarx.BoardGamerTag();
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.playerSelectionPanel1 = new Quarx.PlayerSelectionPanel();
            // 
            // playerOptions1
            // 
            this.playerOptions1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.playerOptions1.BackgroundPath = "Options\\slidersback";
            this.playerOptions1.CanDrag = false;
            this.playerOptions1.Enabled = true;
            this.playerOptions1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.playerOptions1.Location = new Microsoft.Xna.Framework.Point(53, 0);
            this.playerOptions1.Name = "";
            this.playerOptions1.Parent = this;
            this.playerOptions1.Size = new Microsoft.Xna.Framework.Point(500, 75);
            this.playerOptions1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.playerOptions1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.playerOptions1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.playerOptions1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.playerOptions1.Visible = true;
            // 
            // boardGamerTag1
            // 
            this.boardGamerTag1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.boardGamerTag1.BackgroundPath = null;
            this.boardGamerTag1.CanDrag = false;
            this.boardGamerTag1.Enabled = true;
            this.boardGamerTag1.Gamer = null;
            this.boardGamerTag1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.boardGamerTag1.Location = new Microsoft.Xna.Framework.Point(44, 22);
            this.boardGamerTag1.Name = "";
            this.boardGamerTag1.Parent = this;
            this.boardGamerTag1.Size = new Microsoft.Xna.Framework.Point(164, 51);
            this.boardGamerTag1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.boardGamerTag1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.boardGamerTag1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.boardGamerTag1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.boardGamerTag1.Visible = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel1.BackgroundPath = "readybright";
            this.panel1.CanDrag = false;
            this.panel1.Enabled = true;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(250, 1);
            this.panel1.Name = "";
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(296, 71);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.Visible = true;
            // 
            // playerSelectionPanel1
            // 
            this.playerSelectionPanel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.playerSelectionPanel1.BackgroundPath = null;
            this.playerSelectionPanel1.CanDrag = false;
            this.playerSelectionPanel1.Enabled = true;
            this.playerSelectionPanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.playerSelectionPanel1.Location = new Microsoft.Xna.Framework.Point(2, 2);
            this.playerSelectionPanel1.Name = "";
            this.playerSelectionPanel1.Parent = this;
            this.playerSelectionPanel1.Size = new Microsoft.Xna.Framework.Point(47, 75);
            this.playerSelectionPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.playerSelectionPanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.playerSelectionPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.playerSelectionPanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.playerSelectionPanel1.Visible = true;
            // 
            // SwitchOptions
            // 
            this.Panels.Add(this.playerOptions1);
            this.Panels.Add(this.boardGamerTag1);
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.playerSelectionPanel1);
            this.Size = new Microsoft.Xna.Framework.Point(550, 75);

        }
    }
}
