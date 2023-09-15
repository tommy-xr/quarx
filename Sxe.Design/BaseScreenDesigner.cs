using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;

using Sxe.Engine.UI;

namespace Sxe.Design
{
    public class BaseScreenDesigner : ComponentDesigner, IRootDesigner
    {

        private class DesignerView : GraphicsDeviceControl
        {
            private BaseScreenDesigner screenDesigner;

            public DesignerView(BaseScreenDesigner designer)
            {
                screenDesigner = designer;
                AllowDrop = true;
            }

            protected override void Draw()
            {
                base.Draw();
                BaseScreen screen = screenDesigner.Component as BaseScreen;
                if (screen != null)
                {
                    screen.Draw(null);
                }
            }
        }
    }
}
