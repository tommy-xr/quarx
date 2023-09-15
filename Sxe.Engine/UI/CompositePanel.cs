using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Sxe.Engine.Input;

#if !XBOX
using System.ComponentModel.Design;
using Sxe.Design;
#endif

namespace Sxe.Engine.UI
{




    /// <summary>
    /// Base class for user-made controls
    /// </summary>
#if !XBOX
    [Designer(typeof(PanelDesigner), typeof(IDesigner))]
    [Designer(typeof(PanelContainerRootDesigner), typeof(IRootDesigner))]
#endif
    public class CompositePanel : PanelContainer
    {



    }


}
