using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Engine.UI
{
    /// <summary>
    /// Encapsulates the logic of what selections are allowed
    /// </summary>
    public class MenuSelectionCriteria
    {
        bool allowAll = true;
        List<int> allowedPlayerIndices = new List<int>();
        
        /// <summary>
        /// If true, all players will be allowed to select this menu item
        /// Otherwise, only the players in AllowedIndices will be allowed
        /// </summary>
        public bool AllowAll
        {
            get { return allowAll; }
            set { allowAll = value; }
        }

        /// <summary>
        /// The allowed player indices for this control
        /// </summary>
        public List<int> AllowedIndices
        {
            get { return allowedPlayerIndices; }
        }

        

    }
}
