using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sxe.Engine.Input
{



    /// <summary>
    /// Fat interface for all types of input events
    /// </summary>
    public abstract class InputEventArgs : EventArgs
    {
        //TODO: Can we get rid of sender?? I dont even know what this is for now
        object sender;
        public object Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        int index;
        public int PlayerIndex
        {
            get { return index; }
            set { index = value; }
        }

        bool isSignedIn;
        public bool IsSignedIn
        {
            get { return isSignedIn; }
            set { isSignedIn = value; }
        }

        public virtual void Reset()
        {
            sender = null;
        }
    }

    public enum KeyEventType 
    {
        Press
    }

    /// <summary>
    /// This input event gets fired if the state of a controller changes
    /// </summary>
    public class ControllerChangedEventArgs : InputEventArgs
    {
    }

    public class KeyEventArgs :InputEventArgs
    {
        KeyEventType type;
        Keys key;
        string keyString;
        bool isSpecialKey;
        bool isShiftPressed;
        bool isCtrlPressed;
        

        public KeyEventType KeyEventType
        {
            get { return type; }
            set { type = value; }
        }

        public Keys Key
        {
            get { return key; }
            set { key = value; }
        }

        public string KeyString
        {
            get { return keyString; }
            set { keyString = value; }
        }

        public bool IsSpecialKey
        {
            get { return isSpecialKey; }
            set { isSpecialKey = value; }
        }

        public bool IsShiftPressed
        {
            get { return isShiftPressed; }
            set { isShiftPressed = value; }
        }

        public bool IsCtrlPressed
        {
            get { return isCtrlPressed; }
            set { isCtrlPressed = value; }
        }


        
    }

    public enum MouseEventType
    {
        Move = 0,
        Click,
        Unclick
    }

    public class MouseEventArgs : InputEventArgs
    {
        MouseEventType type;
        Point mousePosition;
        bool leftButtonPressed;

        public MouseEventType MouseEventType
        {
            get { return type; }
            set { type = value; }
        }

        public Point Position
        {
            get { return mousePosition; }
            set { mousePosition = value; }
        }

        public bool LeftButtonPressed
        {
            get { return leftButtonPressed; }
            set { leftButtonPressed = value; }
        }
    }

    public enum MenuEventType
    {
        Up = 0,
        Down,
        Left, 
        Right,
        Select,
        Back
    }

    public class MenuEventArgs : InputEventArgs
    {
        MenuEventType type;

        public MenuEventType MenuEventType
        {
            get { return type; }
            set { type = value; }
        }
    }

}
