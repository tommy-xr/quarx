using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.UI
{
    /// <summary>
    /// Defines the interface for UI elements that are editable
    /// </summary>
    interface IEditable
    {
        /// <summary>
        /// Editable
        /// Returns true if the element can be edited, false otherwise
        /// </summary>
        bool Editable { get; }

        /// <summary>
        /// IsEditing
        /// Returns true if the element is being edited, false otherwise
        /// </summary>
        bool IsEditing { get; set; }
    }
}
