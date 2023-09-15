using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace Sxe.Engine.Input
{
    /// <summary>
    /// Interface for an object that can take text strings from input
    /// </summary>
    public interface ITextHandler
    {
        /// <summary>
        /// The maximum characters the text handler can accept
        /// </summary>
        int MaxCharacters { get; }

        //Called when an input string from text is sent to the text handler
        string TextValue { get; set; }

        //Called when a key such as enter or tab is pressed
        void SpecialKeyPressed(Keys key);
    }
}
