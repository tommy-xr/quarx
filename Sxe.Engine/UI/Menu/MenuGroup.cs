using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Sxe.Engine.Input;

namespace Sxe.Engine.UI.Menu
{

    public enum MenuControlType
    {
        Horizontal = 0,
        Vertical
    }

    /// <summary>
    /// A menugroup is a frame for containg menu entries.
    /// It acts to control which entry is selected and is a logical grouping
    /// of menu objects.
    /// </summary>
    public class MenuGroup : PanelContainer, IMenuEntry
    {
        //A list of menu entries is kept internally
        //We have to keep this separate from our children because
        //not everything that is our child can be a menu entry
        List<IMenuEntry> menuEntries = new List<IMenuEntry>();
        //int selectedEntry = -1;
        MenuControlType controlType = MenuControlType.Vertical;
        //MenuGroupMode mode = MenuGroupMode.AllowSignedIn; //default is only allow signed in
        bool allowMultiSelect = false;
        bool selectFirstByDefault = true;
        int globalSelection = 0;

        //A dictionary to manage all the selected indices
        Dictionary<int, int> playerIndexToSelected = new Dictionary<int, int>();
        public event EventHandler<ButtonPressEventArgs> MenuCancel;

        //public MenuGroupMode Mode
        //{
        //    get { return mode; }
        //    set { mode = value; }
        //}

        [DefaultValue(true)]
        public bool SelectFirstByDefault
        {
            get { return selectFirstByDefault; }
            set { selectFirstByDefault = value; }
        }

        [DefaultValue(false)]
        public bool AllowMultiSelect
        {
            get { return allowMultiSelect; }
            set { allowMultiSelect = value; }
        }

        protected int GetSelectedEntry(int playerIndex)
        {
            if (!allowMultiSelect)
                return globalSelection;


            if (!playerIndexToSelected.ContainsKey(playerIndex))
            {
              
                playerIndexToSelected.Add(playerIndex, 0);
                //AdjustSelected(1, playerIndex);
            }

            return playerIndexToSelected[playerIndex];
        }

        protected void SetSelected(int playerIndex, int selected)
        {
            if (!allowMultiSelect)
                globalSelection = selected;

            IMenuEntry entry = this.GetSelected(playerIndex);
            if (entry != null)
                entry.Leave(playerIndex);

            playerIndexToSelected[playerIndex] = selected;
        }

        public IMenuEntry GetSelected(int playerIndex)
        {
            int selectedEntry = GetSelectedEntry(playerIndex);

            if (selectedEntry < 0 || selectedEntry >= menuEntries.Count)
                return null;

            return menuEntries[selectedEntry];
        }

        //public IMenuEntry Selected
        //{
        //    get
        //    {
        //        if (selectedEntry < 0 || selectedEntry >= menuEntries.Count)
        //            return null;

        //        return menuEntries[selectedEntry];
        //    }
        //}

        [DefaultValue((int)MenuControlType.Vertical)]
        public MenuControlType ControlType
        {
            get { return controlType; }
            set { controlType = value; }
        }

        public MenuGroup()
        {
            this.PanelAdded += OnPanelsChanged;
            this.PanelRemoved += OnPanelsChanged;
            this.ParentChanged += OnParentChanged;


        }

        public override void Activate()
        {

            base.Activate();
            //Should we select the first one or not?
            //The problem here is that we are assuming all the time we want the first item selected
            //While this works well for most menus, maybe we should keep it an option so we can disable it for
            //nested menus
            if (!allowMultiSelect)
            {
                if (selectFirstByDefault)
                {
                    //Do we have something selected already? This could happen if we bring a screen back. If so we better leave the old selection
                    IMenuEntry oldEntry = this.GetSelected(0);
                    if (oldEntry != null)
                        oldEntry.Leave(0);


                    this.SetSelected(0, 0);
                    IMenuEntry entry = this.GetSelected(0);

                    if (entry != null)
                        entry.Over(0);
                }
            }
            else
            {
                if (selectFirstByDefault)
                {
                    //Otherwise, loop over all the gamers, and show their selection
                    for (int i = 0; i < AnarchyGamer.Gamers.Count; i++)
                    {

                        if (AnarchyGamer.Gamers[i].IsPlayer)
                        {
                            //First, set selected to the first item. This could be illegal if the player is not allowed
                            //over that, so we'll double check and move if necessary
                            this.SetSelected(AnarchyGamer.Gamers[i].Index, 0);



                            if (!this.GetSelected(AnarchyGamer.Gamers[i].Index).AllowPlayerIndex(AnarchyGamer.Gamers[i].Index))
                                AdjustSelected(1, AnarchyGamer.Gamers[i].Index);
                            else
                                AdjustSelected(0, AnarchyGamer.Gamers[i].Index);
                        }
                    }

                    //Now, we might have called Leave() on some of these entries which might have caused problems
                    //So do another loop through AnarchyGamers, and make sure the entries know that they are overd

                    for (int i = 0; i < AnarchyGamer.Gamers.Count; i++)
                    {
                        AdjustSelected(0, AnarchyGamer.Gamers[i].Index);
                    }
                }

                //The problem now is that we might have fired leave on items that are still occupied,
                //and some items might have been activated that aren't valid. So we'll do another pass,
                //over all the menu entries - and call either over or leave depending on the outcome

                //for (int i = 0; i < menuEntries.Count; i++)
                //{
                //    if (playerIndexToSelected.ContainsValue(i))
                //    {
                //    }
                //}

            }


            //TODO: Is this ok... we are going to stop the base classes behavior of activating all our children?


            //base.Activate();
        }

        public bool AllowPlayerIndex(int index)
        {
            if (InputFilterMode == InputFilterMode.AllowAll)
                return true;

            if (InputFilterMode == InputFilterMode.AllowSignedIn)
            {
                if (AnarchyGamer.Gamers.GetGamerByPlayerIndex(index) != null)
                    return true;

                return false;
            }

            if (InputFilterMode == InputFilterMode.AllowSpecified)
            {
                if (this.AllowedPlayers.Contains(index))
                    return true;

                return false;
            }

            //This shouldn't ever happen as we only have those 3 input modes!
            return true;
        }

        public void PerformClick(int index) { }
        public void Over(int index) 
        {
            IMenuEntry selected = GetSelected(index);
            if(selected != null)
                selected.Over(index);
        
        }
        public void Leave(int index) 
        {
            //Get rid of everything on leaving
            IMenuEntry selected = GetSelected(index);
            if (selected != null)
                selected.Leave(index);


            //for (int i = 0; i < menuEntries.Count; i++)
            //    menuEntries[i].Leave(index);
        }


        /// <summary>
        /// If our parent is changed
        /// </summary>
        void OnParentChanged(object sender, EventArgs args)
        {
            //Hijack the active panel
            PanelContainer parent = this.Parent as PanelContainer;

            if (parent != null)
                parent.ActiveControl = this;

            //this.Parent = this;
        }

        void RefreshMenuEntries()
        {
            OnPanelsChanged(this, EventArgs.Empty);
        }

        void OnPanelsChanged(object sender, EventArgs args)
        {
            
            menuEntries.Clear();
            //Loop through all the children, and see if they are a menu entry
            foreach (Panel child in this.Panels)
            {
                AddEntry(child);
            }
        }

        void AddEntry(Panel child)
        {


            IMenuEntry entry = child as IMenuEntry;
            if(entry != null)
            {
                menuEntries.Add(entry);
            }
            else
            {
                MenuGroup childGroup = child as MenuGroup;
                if (childGroup != null)
                {
                    foreach (Panel panel in childGroup.Panels)
                        AddEntry(child);
                }
            }

            //if (menuEntries.Count == 1)
            //    AdjustSelected(0);

            //if (Selected == null)
            //{
            //    selectedEntry = 0;
            //    AdjustSelected(0, -1);
            //}

        
        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {

            if (!DoesEventPassFilter(inputEvent))
                return false;

            MenuEventArgs menuEvent = inputEvent as MenuEventArgs;

            //if (mode == MenuGroupMode.AllowSignedIn && !inputEvent.IsSignedIn)
            //    return false;


            if (menuEvent != null)
            {

                IMenuEntry Selected = GetSelected(menuEvent.PlayerIndex);

                //First check if our Selected handled this... If it did, just return
                if (Selected != null && Selected.HandleEvent(inputEvent))
                    return true;


                //TODO: Change adjust selected to only return true if it actually did something
                //Otherwise we want to return false as we did not handle the event

                if (menuEvent.MenuEventType == MenuEventType.Down)
                {
                    //if(Selected != null)
                    //{
                        AdjustSelected(1, menuEvent.PlayerIndex);
                        return true;
                    //}
                }
                else if (menuEvent.MenuEventType == MenuEventType.Up)
                {
                    //if(Selected != null)
                    //{
                        AdjustSelected(-1, menuEvent.PlayerIndex);
                        return true;
                    //}
                }
                else if (menuEvent.MenuEventType == MenuEventType.Select)
                {
                    if (Selected != null)
                        Selected.PerformClick(menuEvent.PlayerIndex);

                    return true;
                }
                else if (menuEvent.MenuEventType == MenuEventType.Back)
                {
                    if (MenuCancel != null)
                    {
                        ButtonPressEventArgs bpea = new ButtonPressEventArgs();
                        bpea.PlayerIndex = menuEvent.PlayerIndex;
                        MenuCancel(this, bpea);
                    }

                    return true;
                }

                //if (Selected != null)
                //    return Selected.HandleEvent(inputEvent);
                
            }

            return base.HandleEvent(inputEvent);
        }

        void AdjustSelected(int offset, int playerIndex)
        {
            IMenuEntry oldSelected = GetSelected(playerIndex);
            IMenuEntry newSelected = oldSelected;
            int newSelectedIndex = -1;

            int selectedEntry = GetSelectedEntry(playerIndex);
            //Lets try and find a new selected

            IMenuEntry Selected = GetSelected(playerIndex);

            if (offset != 0)
            {
                for (int i = selectedEntry + offset; i >= 0 && i < this.menuEntries.Count; i += offset)
                {
                    IMenuEntry tempSelected = menuEntries[i];

                    //
                    if (tempSelected != null && tempSelected.Enabled && 
                        (tempSelected.AllowPlayerIndex(playerIndex) || !allowMultiSelect))
                    {
                        newSelected = tempSelected;
                        newSelectedIndex = i;
                        //playerIndexToSelected[playerIndex] = i;
                        SetSelected(playerIndex, i);
                        break;

                    }
                }
            }
            else
            {
                if (Selected != null)
                {
                    Selected.Leave(playerIndex);
                    Selected.Over(playerIndex);
                    
                }
            }

            Selected = GetSelected(playerIndex);


            if (newSelectedIndex != -1)
            {
                selectedEntry = newSelectedIndex;

                if (oldSelected != null)
                    oldSelected.Leave(playerIndex);

                if (Selected != null)
                    Selected.Over(playerIndex);
            }

            //if (oldSelected != null)
            //    oldSelected.Leave();

            

            //selectedEntry += offset;

            //if (selectedEntry < 0)
            //    selectedEntry = 0;
            //if (selectedEntry >= menuEntries.Count)
            //    selectedEntry = menuEntries.Count - 1;

            //if (Selected != null)
            //    Selected.Over();
        }

    }
}
