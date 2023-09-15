using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine.Input;

using Microsoft.Xna.Framework;

namespace Quarx
{
    public class PuzzleContainer : CompositePanel
    {
        const int iconSizeX = 100;
        const int iconSizeY = 100;

        const int paddingX = 10;
        const int paddingY = 10;


        PuzzleIcon[,] icons;

        int currentSelectionX = 0;
        int currentSelectionY = 0;

        int currentAddPuzzle = 0;

        public PuzzleIcon Selected
        {
            get { return icons[currentSelectionX, currentSelectionY]; }
        }

        public int SelectedIndex
        {
            get
            {
                return currentSelectionX + currentSelectionY * icons.GetLength(0);
            }
        }

        public void AddPuzzle(PuzzleDescription description)
        {
            int width = icons.GetLength(0);
            int height = icons.GetLength(1);

            int yIndex = currentAddPuzzle / width;
            int xIndex = currentAddPuzzle % width;

            icons[xIndex, yIndex].PuzzleDescription = description;

            currentAddPuzzle++;
        }

        public void SetEnabled(int i)
        {
            int width = icons.GetLength(0);
            int height = icons.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (y * width + x < i)
                    {
                        icons[x, y].Enabled = true;
                    }
                    else
                    {
                        icons[x, y].Enabled = false;
                    }
                }
            }
        }

        public PuzzleContainer()
        {
            InitializeComponent();
            //CreateIcons();


        }

        public void CreateIcons()
        {
            //Figure out how many icons we can fit in here
            int numIconsX = this.Size.X / (iconSizeX + paddingX);
            int numIconsY = this.Size.Y / (iconSizeY + paddingY);

            //Now, figure out how much padding space we have
            int xPadding = (this.Size.X - (numIconsX * iconSizeX) - ((numIconsX-1)*paddingX)) / 2;
            int yPadding = (this.Size.Y - (numIconsY * iconSizeY) - ((numIconsY-1)*paddingY)) / 2;

            icons = new PuzzleIcon[numIconsX, numIconsY];

            for (int x = 0; x < numIconsX; x++)
            {
                for (int y = 0; y < numIconsY; y++)
                {
                    icons[x, y] = new PuzzleIcon();
                    icons[x, y].Location = new Point(xPadding + x * (iconSizeX + paddingX),
                        yPadding + y * (iconSizeY+paddingY));
                    this.Panels.Add(icons[x, y]);
                }
            }

            Selected.Over(0);


        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            MenuEventArgs menuEvent = inputEvent as MenuEventArgs;

            if (menuEvent != null)
            {
                Selected.Leave(inputEvent.PlayerIndex);

                int oldSelectedX = currentSelectionX;
                int oldSelectedY = currentSelectionY;

                if (menuEvent.MenuEventType == MenuEventType.Right)
                {
                    currentSelectionX++;
                    if (currentSelectionX >= icons.GetLength(0))
                    {
                        if (MoveDown())
                            currentSelectionX = 0;
                        else
                            currentSelectionX = icons.GetLength(0) - 1;

                        
                    }
                }
                else if (menuEvent.MenuEventType == MenuEventType.Left)
                {
                    currentSelectionX--;
                    if (currentSelectionX < 0)
                    {
                        if (MoveUp())
                            currentSelectionX = icons.GetLength(0) - 1;
                        else
                            currentSelectionX = 0;
                    }
                }
                else if (menuEvent.MenuEventType == MenuEventType.Down)
                {
                    MoveDown();
                }
                else if (menuEvent.MenuEventType == MenuEventType.Up)
                {
                    MoveUp();
                }

                if (!Selected.Enabled)
                {
                    currentSelectionX = oldSelectedX;
                    currentSelectionY = oldSelectedY;
                }


                Selected.Over(menuEvent.PlayerIndex);
            }

            return base.HandleEvent(inputEvent);
        }

        bool MoveUp()
        {
            if (currentSelectionY == 0)
                return false;

            currentSelectionY--;
            return true;
        }

        bool MoveDown()
        {
            if (currentSelectionY == icons.GetLength(1) - 1)
                return false;

            currentSelectionY++;
            return true;
        }

        void InitializeComponent()
        {
        }
    }
}
