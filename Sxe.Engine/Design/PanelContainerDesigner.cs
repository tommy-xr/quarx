using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

using Sxe.Engine.UI;

using Drawing = System.Drawing;
using XnaDrawing = Microsoft.Xna.Framework;

namespace Sxe.Design
{
    /// <summary>
    /// Base class for a designer for panel containers
    /// </summary>
    public class PanelContainerDesigner : PanelDesigner
    {
        public virtual PanelContainer PanelContainer
        {
            get { return this.Panel as PanelContainer; }
        }


        /// <summary>
        /// Called when a panel is dropped into this panelContainer
        /// Handles the logic of makign the panel a child of this panel, and 
        /// removing it as a child from another container
        /// </summary>
        /// <param name="droppedDesigner"></param>
        public override void DropPanel(PanelDesigner droppedDesigner)
        {
            //We want to make the panel a child of this component
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            IComponentChangeService cs = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            if (host != null)
            {
                using (DesignerTransaction trans = host.CreateTransaction("test child"))
                {
                    if (cs != null)
                    {
                        cs.OnComponentChanging(droppedDesigner.Panel, null);
                        cs.OnComponentChanging(this.Panel, null);
                    }

                    PanelContainer container = this.Panel as PanelContainer;
                    if (container != null)
                    {
                        droppedDesigner.Panel.Location = new XnaDrawing.Point(
                            droppedDesigner.Panel.AbsoluteLocation.X - container.AbsoluteLocation.X,
                            droppedDesigner.Panel.AbsoluteLocation.Y - container.AbsoluteLocation.Y);
                        container.Panels.Add(droppedDesigner.Panel);
                    }

                    trans.Commit();
                }
            }

            base.DropPanel(droppedDesigner);
        }

        /// <summary>
        /// This is overridden so we can check for hitting our children first!
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal override DesignerHitResult GetHitTest(int x, int y, IList ignoreObjects)
        {
           
            IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
            //Check and see if the hit test succeeds for any of our child panels
            if (host != null)
            {
                for(int i = this.PanelContainer.Panels.Count - 1; i >=0; i--)
                {
                    Sxe.Engine.UI.Panel panel = this.PanelContainer.Panels[i];
                    bool ignore = false;
                    if (ignoreObjects != null)
                        if (ignoreObjects.Contains(panel))
                            ignore = true;

                    if (!ignore)
                    {
                        PanelDesigner designer = host.GetDesigner(panel) as PanelDesigner;
                        if (designer != null)
                        {
                            //panel.BackColor = XnaDrawing.Graphics.Color.Red;
                            DesignerHitResult result = designer.GetHitTest(x, y);
                            if (result.HitObject != null)
                                return result;
                        }
                    }
                }
            }

            return base.GetHitTest(x, y, ignoreObjects);
        }
    }



}
