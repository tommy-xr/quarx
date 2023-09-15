#if !XBOX

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;



namespace Sxe.Engine.Input
{
    public class KeyboardMouseGameController : XboxGameController
    {
        string[] keyboardKeys;

        public KeyboardMouseGameController(IServiceProvider services, ContentManager content, int playerIndex)
            : base(services, content, playerIndex)
        {
            keyboardKeys = Enum.GetNames(typeof(Keys));

            //Add all the keyboard keys to the commands the controller knows about
            for (int i = 0; i < keyboardKeys.Length; i++)
            {
                keyboardKeys[i] = keyboardKeys[i].ToLower(CultureInfo.CurrentCulture);
                AddKey(keyboardKeys[i].ToLower(CultureInfo.CurrentCulture));
            }

            AddKey("mousebutton1");
            AddKey("mousebutton2");
            AddKey("mouse_x");
            AddKey("mouse_y");

            Bind("mouse_x", "cursor_x");
            Bind("mouse_y", "cursor_y");
            Bind("mousebutton1", "cursor_leftclick");
            Bind("mousebutton2", "cursor_rightclick");

            Bind("down", "menu_down");
            Bind("up", "menu_up");
            Bind("left", "menu_left");
            Bind("right", "menu_right");
            Bind("a", "menu_select");
            Bind(Microsoft.Xna.Framework.Input.Keys.Escape.ToString().ToLower(), "menu_back");


        }

        public override void ControllerUpdate(GameTime gameTime)
        {

            base.ControllerUpdate(gameTime);

            if (!this.Connected)
            {

                //Loop through all keys, set current states to 0.0f for all keyboard keys
                for (int i = 0; i < keyboardKeys.Length; i++)
                {
                    KeyInfo k = GetKeyInfoFromName(keyboardKeys[i]);

                    SetValue(k.Name, 0.0f);
                    //if (k.ActionIndex != -1)
                    //{
                    //    SetCurrentActionState(k.ActionIndex, 0.0f);
                    //    //CurrentActionState[k.ActionIndex] = 0.0f;
                    //}
                }

                Keys[] pressedKeys = Input.Keyboard.GetPressedKeys();

                for (int i = 0; i < pressedKeys.Length; i++)
                {
                    SetValue(pressedKeys[i].ToString().ToLower(CultureInfo.CurrentCulture), 1.0f);
                }

                //Handle mouse stuff
                float mouseX = Input.Mouse.AbsoluteMousePos.X;
                float mouseY = Input.Mouse.AbsoluteMousePos.Y;

                SetValue("mouse_x", mouseX);
                SetValue("mouse_y", mouseY);


                float val = 1.0f;
                if (!Input.Mouse.IsLeftButtonDown())
                    val = 0.0f;

                SetValue("mousebutton1", val);

                val = 1.0f;
                if (!Input.Mouse.IsRightButtonDown())
                    val = 0.0f;
                SetValue("mousebutton2", val);
            }

            //throw new Exception("The method or operation is not implemented.");
        }



    }
}
#endif