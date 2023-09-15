
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sxe.Engine.Input
{

    /// <summary>
    /// Input class that implements IKeyboardInput, using the keyboard (yay)
    /// </summary>
    public class BasicKeyboardInput : InputEventGenerator, IKeyboardInput
    {
        KeyboardState lastKeyState;
        KeyboardState currentKeyState;


        KeyEventArgs keyEvent = new KeyEventArgs();


        /// <summary>
        /// Checks if the key is pressed this frame
        /// </summary>
        /// <param name="k">Key to check for</param>
        /// <returns>True if key is pressed this frame, false otherwise</returns>
        public bool IsKeyDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public Keys[] GetPressedKeys()
        {
            return currentKeyState.GetPressedKeys();
        }

        /// <summary>
        /// Checks if the key is pressed this frame, and not last frame
        /// </summary>
        /// <param name="k">Key to check for</param>
        /// <returns>True if key was just pressed, false otherwise</returns>
        public bool IsKeyJustPressed(Keys key)
        {
            return (IsKeyDown(key) && !lastKeyState.IsKeyDown(key));
        }

        public void Update(GameTime gameTime)
        {
            lastKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();


                bool shiftPressed = IsKeyDown(Keys.LeftShift) || IsKeyDown(Keys.RightShift);
                bool ctrlPressed = IsKeyDown(Keys.LeftControl) || IsKeyDown(Keys.RightControl);

                foreach (Keys k in currentKeyState.GetPressedKeys())
                {
                    if (!IsKeyJustPressed(k))
                        continue;

                    if (k == Keys.OemTilde) //ignore tilde
                    {
                        continue;
                    }
                    else
                    {
                        keyEvent.Key = k;
                        keyEvent.KeyEventType = KeyEventType.Press;
                        keyEvent.IsShiftPressed = shiftPressed;
                        keyEvent.IsCtrlPressed = ctrlPressed;
                        keyEvent.IsSpecialKey = IsSpecialKey(k);
                        keyEvent.KeyString = KeyToChar(k, shiftPressed).ToString();

                        FireEvent(keyEvent);
                  
                    } 
            }
        }


        public static bool IsSpecialKey(Keys key)
        {
            // All keys except A-Z, 0-9 and `-\[];',./= (and space) are special keys.
            // With shift pressed this also results in this keys:
            // ~_|{}:"<>? !@#$%^&*().
            int keyNum = (int)key;
            if ((keyNum >= (int)Keys.A && keyNum <= (int)Keys.Z) ||
                (keyNum >= (int)Keys.D0 && keyNum <= (int)Keys.D9) ||
                key == Keys.Space || // well, space ^^
                key == Keys.OemTilde || // `~
                key == Keys.OemMinus || // -_
                key == Keys.OemPipe || // \|
                key == Keys.OemOpenBrackets || // [{
                key == Keys.OemCloseBrackets || // ]}
                key == Keys.OemSemicolon || // ;:
                key == Keys.OemQuotes || // '"
                key == Keys.OemComma || // ,<
                key == Keys.OemPeriod || // .>
                key == Keys.OemQuestion || // /?
                key == Keys.OemPlus) // =+
                return false;

            // Else is is a special key
            return true;
        } // static bool IsSpecialKey(Keys key)

        /// <summary>
        /// Keys to char helper conversion method.
        /// Note: If the keys are mapped other than on a default QWERTY
        /// keyboard, this method will not work properly. Most keyboards
        /// will return the same for A-Z and 0-9, but the special keys
        /// might be different. Sorry, no easy way to fix this with XNA ...
        /// For a game with chat (windows) you should implement the
        /// windows events for catching keyboard input, which are much better!
        /// </summary>
        /// <param name="key">Keys</param>
        /// <returns>Char</returns>
        public static char KeyToChar(Keys key, bool shiftPressed)
        {
            // If key will not be found, just return space
            char ret = ' ';
            int keyNum = (int)key;
            if (keyNum >= (int)Keys.A && keyNum <= (int)Keys.Z)
            {
                if (shiftPressed)
                    ret = key.ToString()[0];
                else
                    ret = key.ToString().ToLower(CultureInfo.CurrentCulture)[0];
            } // if (keyNum)
            else if (keyNum >= (int)Keys.D0 && keyNum <= (int)Keys.D9 &&
                shiftPressed == false)
            {
                ret = (char)((int)'0' + (keyNum - Keys.D0));
            } // else if
            else if (key == Keys.D1 && shiftPressed)
                ret = '!';
            else if (key == Keys.D2 && shiftPressed)
                ret = '@';
            else if (key == Keys.D3 && shiftPressed)
                ret = '#';
            else if (key == Keys.D4 && shiftPressed)
                ret = '$';
            else if (key == Keys.D5 && shiftPressed)
                ret = '%';
            else if (key == Keys.D6 && shiftPressed)
                ret = '^';
            else if (key == Keys.D7 && shiftPressed)
                ret = '&';
            else if (key == Keys.D8 && shiftPressed)
                ret = '*';
            else if (key == Keys.D9 && shiftPressed)
                ret = '(';
            else if (key == Keys.D0 && shiftPressed)
                ret = ')';
            else if (key == Keys.OemTilde)
                ret = shiftPressed ? '~' : '`';
            else if (key == Keys.OemMinus)
                ret = shiftPressed ? '_' : '-';
            else if (key == Keys.OemPipe)
                ret = shiftPressed ? '|' : '\\';
            else if (key == Keys.OemOpenBrackets)
                ret = shiftPressed ? '{' : '[';
            else if (key == Keys.OemCloseBrackets)
                ret = shiftPressed ? '}' : ']';
            else if (key == Keys.OemSemicolon)
                ret = shiftPressed ? ':' : ';';
            else if (key == Keys.OemQuotes)
                ret = shiftPressed ? '"' : '\'';
            else if (key == Keys.OemComma)
                ret = shiftPressed ? '<' : ',';
            else if (key == Keys.OemPeriod)
                ret = shiftPressed ? '>' : '.';
            else if (key == Keys.OemQuestion)
                ret = shiftPressed ? '?' : '/';
            else if (key == Keys.OemPlus)
                ret = shiftPressed ? '+' : '=';

            // Return result
            return ret;
        } // KeyToChar(key)
    }
}

