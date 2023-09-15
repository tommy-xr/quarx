using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Sxe.Design
{
    /// <summary>
    /// Gives the result of cl
    /// </summary>
    internal struct DesignerHitResult
    {
        Panel hitPanel;
        public Panel HitPanel
        {
            get { return hitPanel; }
            set { hitPanel = value; }
        }

        object hitObject;
        public object HitObject
        {
            get { return hitObject; }
            set { hitObject = value; }
        }

    }
}
