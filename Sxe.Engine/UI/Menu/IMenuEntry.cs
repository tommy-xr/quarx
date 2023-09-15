using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.UI
{


    public enum MenuDirection
    {
        Left = 0,
        Right, 
        Up, 
        Down
    }

    public enum FocusAllowMode
    {
        AllowAll =0,
        AllowSpecified,
        AllowNone
    }

    /// <summary>
    /// This is an interface for anything that can be a menu object
    /// </summary>
    public interface IMenuEntry : IButton
    {

        bool Enabled { get; }

        bool HandleEvent(Input.InputEventArgs args); //called when input events occur

        bool AllowPlayerIndex(int playerIndex);

        //bool CanLeave { get; } //whether focus can leave the control or not

    }
}
