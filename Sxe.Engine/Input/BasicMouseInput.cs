
#if !XBOX

using System;
using System.Collections.Generic;


using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using XnaInput = Microsoft.Xna.Framework.Input;

using Sxe.Engine.UI;


namespace Sxe.Engine.Input
{
    /// <summary>
    /// Class that implements mouse input from the mouse yay
    /// </summary>
    public class BasicMouseInput : InputEventGenerator, IMouseInput
    {

        Game game;

        MouseState lastMouseState;
        MouseState currentMouseState;

        Point mousePos = Point.Zero;
        Point mouseDelta = Point.Zero;
        Point clampPos = Point.Zero;

        //Width and height of viewport
        int width;
        int height;
        int xOffset;
        int yOffset;

        bool freezeMouse = false;
        public bool FreezeMouse
        {
            get { return freezeMouse; }
            set { freezeMouse = value; }
        }




        public BasicMouseInput(Game inGame)
        {
            game = inGame;
            UpdateViewport();
        }



        void UpdateViewport()
        {

            width = game.GraphicsDevice.Viewport.Width;
            height = game.GraphicsDevice.Viewport.Height;
            xOffset = game.GraphicsDevice.Viewport.X;
            yOffset = game.GraphicsDevice.Viewport.Y;
            clampPos = new Point(xOffset + width / 2, yOffset + height / 2);
        }

        public Vector2 RelativeMousePos
        {
            get
            {
                //UpdateViewport();
                return new Vector2((float)(mousePos.X - xOffset) / (float)width, (float)(mousePos.Y - yOffset) / (float)height);
            }

        }

        public Point AbsoluteMousePos
        {
            get
            {
                return mousePos;
            }
        }

        public Vector2 RelativeMouseDelta
        {
            get
            {
                //UpdateViewport();
                return new Vector2((float)mouseDelta.X / (float)width, (float)mouseDelta.Y / (float)height);
            }
        }

        public Point AbsoluteMouseDelta
        {
            get
            {
                return mouseDelta;
            }
        }


        public bool IsLeftButtonJustPressed()
        {
            return (currentMouseState.LeftButton == XnaInput.ButtonState.Pressed && lastMouseState.LeftButton == XnaInput.ButtonState.Released);
        }

        public bool IsLeftButtonJustReleased()
        {
            return (currentMouseState.LeftButton == XnaInput.ButtonState.Released && lastMouseState.LeftButton == XnaInput.ButtonState.Pressed);
        }

        public bool IsRightButtonJustPressed()
        {
            return (currentMouseState.RightButton == XnaInput.ButtonState.Pressed && lastMouseState.RightButton == XnaInput.ButtonState.Released);
        }

        public bool IsLeftButtonDown()
        {
            return currentMouseState.LeftButton == XnaInput.ButtonState.Pressed;
        }

        public bool IsMiddleButtonDown()
        {
            return currentMouseState.MiddleButton == XnaInput.ButtonState.Pressed;
        }

        public bool IsRightButtonDown()
        {
            return currentMouseState.RightButton == XnaInput.ButtonState.Pressed;
        }


        public void Update(GameTime gameTime)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (freezeMouse)
            {

                mouseDelta = new Point(currentMouseState.X - clampPos.X, currentMouseState.Y - clampPos.Y);
                Mouse.SetPosition((int)clampPos.X, (int)clampPos.Y);
                mousePos = new Point(mousePos.X + mouseDelta.X, mousePos.Y + mouseDelta.Y);

                //Check if the mouse is going off the screen
                if (mousePos.X <= xOffset)
                    mousePos.X = xOffset;
                if (mousePos.X >= xOffset + width)
                    mousePos.X = xOffset + width;

                if (mousePos.Y <= yOffset)
                    mousePos.Y = yOffset;
                if (mousePos.Y >= yOffset + height)
                    mousePos.Y = yOffset + height;
            }
            else
            {
                mouseDelta = new Point(currentMouseState.X - lastMouseState.X, currentMouseState.Y - lastMouseState.Y);
                mousePos = new Point(currentMouseState.X, currentMouseState.Y);
            }



        }


    }
}

#endif