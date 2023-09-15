using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System.Drawing.Design;

using EnvDTE;

using Sxe.Engine.UI;
namespace Sxe.Design
{

    [ToolboxItemFilter("AnarchyUI", ToolboxItemFilterType.Require)]
    public class PanelContainerRootDesigner : ComponentDesigner, IRootDesigner, IToolboxUser
    {
        private DesignerView m_view;

        protected virtual PanelContainer PanelContainer
        {
            get { return (PanelContainer)Component; }
        }

        /// <summary>
        /// Our implementation of IRootDesigner.SupportedTechologies.  Our designer
        /// uses Windows Forms as the UI technology, so we just return that enum value.
        /// </summary>
        ViewTechnology[] IRootDesigner.SupportedTechnologies
        {
            get
            {
                return new ViewTechnology[] { ViewTechnology.WindowsForms };
            }
        }

        /// <summary>
        /// Our implementation of IRootDesigner.GetView.  This expects a windows
        /// forms view technogy.  Our view is just a Windows Forms control that
        /// has a paint handler that knows how to draw shapes.
        /// </summary>
        /// <param name="technology"></param>
        /// <returns></returns>
        object IRootDesigner.GetView(ViewTechnology technology)
        {
            if (technology != ViewTechnology.WindowsForms)
            {
                throw new ArgumentException("technology");
            }

            // Note that we store off a single instance of the
            // view.  Don't always create a new object here, because
            // it is possible that someone will call this multiple times.
            //
            if (m_view == null)
            {
                m_view = new DesignerView(this);
            }
            return m_view;
        }

        /// <summary>
        /// For now, we'll simply accept all items. I don't think this even gets called.
        /// </summary>
        bool IToolboxUser.GetToolSupported(System.Drawing.Design.ToolboxItem item)
        {
            return true;
        }
        
        /// <summary>
        /// Called when the user selects a control
        /// </summary>
        /// <param name="item"></param>
        void IToolboxUser.ToolPicked(System.Drawing.Design.ToolboxItem item)
        {
            m_view.InvokeToolboxItem(item, 10, 10);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(m_view != null)
                m_view.Dispose();
            }
            base.Dispose(disposing);
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
        }




        private class DesignerView : GraphicsDeviceControl
        {
            private readonly static string AnarchyDataName = "AnarchyUIData";

            private PanelContainerRootDesigner screenDesigner;
            private ContentManager content;
            private SpriteBatch spriteBatch;
            private GameTime gameTime;
            private SpriteFont font;

            private IDesignerHost host;
            private IToolboxService toolBoxService;
            private ISelectionService selectionService;
            private IComponentChangeService changeService;
            private IMenuCommandService menuCommandService;
            private IDesignerSerializationService designerSerialization;

            private MenuCommand[] m_menuCommands;

            private DragObject currentDrag;
            Sxe.Engine.UI.Panel lastDragPanel; 
            Sxe.Engine.UI.Panel lastHitPanel;
            object lastHitObject;
            System.Drawing.Pen pen;

            private ICollection m_currentSelection;

            string projectPath;
            int counter = 0;
            string output;

            #region Services

            private IDesignerHost DesignerHost
            {
                get
                {
                    if (host == null && screenDesigner != null)
                    {
                        host = (IDesignerHost)screenDesigner.GetService(typeof(IDesignerHost));
                    }
                    return host;
                }
            }

            private IDesignerSerializationService DesignerSerialization
            {
                get
                {
                    if (designerSerialization == null && screenDesigner != null)
                    {
                        designerSerialization = (IDesignerSerializationService)screenDesigner.GetService(typeof(IDesignerSerializationService));
                    }
                    return designerSerialization;
                }
            }

            private IMenuCommandService MenuCommandService
            {
                get
                {
                    if (menuCommandService == null && screenDesigner != null)
                    {
                        menuCommandService = (IMenuCommandService)screenDesigner.GetService(typeof(IMenuCommandService));

                        menuCommandService.AddVerb(new DesignerVerb("HEY YOU", Test));
                    }
                    return menuCommandService;
                }
            }

            void Test(object sender, EventArgs args)
            {
                MessageBox.Show("HEY!");
            }

            private IToolboxService ToolboxService
            {
                get
                {
                    if (toolBoxService == null && screenDesigner != null)
                    {
                        toolBoxService = (IToolboxService)screenDesigner.GetService(typeof(IToolboxService));
                    }
                    return toolBoxService;
                }
               
            }

            private IComponentChangeService ComponentChangeService
            {
                get
                {
                    if (changeService == null && screenDesigner != null)
                        changeService = (IComponentChangeService)screenDesigner.GetService(typeof(IComponentChangeService));
                    return changeService;
                }
            }

            private ISelectionService SelectionService
            {
                get
                {
                    if (selectionService == null && screenDesigner != null)
                    {
                        selectionService = (ISelectionService)screenDesigner.GetService(typeof(ISelectionService));
                    }
                    return selectionService;
                }
            }

            #endregion

            protected override Viewport Viewport
            {
                get
                {
                    if(screenDesigner.PanelContainer == null)
                    return base.Viewport;

                    Viewport view = new Viewport();
                    view.X = 0;
                    view.Y = 0;
                    view.Width = (int)screenDesigner.PanelContainer.Size.X;
                    view.Height = (int)screenDesigner.PanelContainer.Size.Y;
                    view.MinDepth = 0;
                    view.MaxDepth = 1;
                    return view;
                }
            }


            public DesignerView(PanelContainerRootDesigner designer)
            {
                screenDesigner = designer;
                AllowDrop = true;




            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (content != null)
                    {
                        content.Dispose();
                        content = null;
                    }

                    if (spriteBatch != null)
                    {
                        spriteBatch.Dispose();
                        spriteBatch = null;
                    }

                    ISelectionService ss = SelectionService;
                    if (ss != null)
                    {
                        ss.SelectionChanged -= new EventHandler(OnSelectionChanged);
                    }


                    IComponentChangeService cs = ComponentChangeService;
                    if (cs != null)
                    {
                        cs.ComponentChanged -= new ComponentChangedEventHandler(OnComponentChanged);
                        cs.ComponentChanging -= new ComponentChangingEventHandler(OnComponentChanging);
                        cs.ComponentRemoved -= new ComponentEventHandler(OnComponentRemoved);
                    }

                    if (m_menuCommands != null && MenuCommandService != null)
                    {
                        foreach (MenuCommand mc in m_menuCommands)
                        {
                            MenuCommandService.RemoveCommand(mc);
                        }
                        m_menuCommands = null;
                    }
                }
                base.Dispose(disposing);
            }


            protected override void Initialize()
            {
     

                spriteBatch = new SpriteBatch(GraphicsDevice);
                gameTime = new GameTime(TimeSpan.FromSeconds(0),
                    TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(0));

               

                //Try and find the directory of the project
                DTE dte = (DTE)screenDesigner.GetService(typeof(DTE));
                string directory = null;
                if (dte != null)
                {
                    object[] projects = (object[])dte.ActiveSolutionProjects;
                    Project proj = projects[0] as Project;
                    string path = Path.GetDirectoryName(proj.FullName);
                    projectPath = path;

                    //Add a bin/x86/Debug/ to the path
                    //TODO: Make this more robust!
                    directory = path.ToString() + "/bin/x86/Debug/Content/";
                }
                content = new ContentManager(this.Services, directory);
                font = content.Load<SpriteFont>("Calibri11");

                //We need to let our panel initialize
                if (screenDesigner.PanelContainer != null)
                    screenDesigner.PanelContainer.LoadContent(content);

                ISelectionService ss = SelectionService;
                if (ss != null)
                {
                    ss.SetSelectedComponents(new object[] { screenDesigner.Component }, SelectionTypes.Replace);
                    ss.SelectionChanged += new EventHandler(OnSelectionChanged);
                }

                IComponentChangeService cs = ComponentChangeService;
                if (cs != null)
                {
                    cs.ComponentChanged += new ComponentChangedEventHandler(OnComponentChanged);
                    cs.ComponentChanging += new ComponentChangingEventHandler(OnComponentChanging);
                    cs.ComponentRemoved += new ComponentEventHandler(OnComponentRemoved);
                }

                IMenuCommandService mc = MenuCommandService;
                if (mc != null)
                {
                    m_menuCommands = new MenuCommand[]
                    {
                        new MenuCommand(new EventHandler(OnMenuCut), StandardCommands.Cut),
                        new MenuCommand(new EventHandler(OnMenuCopy), StandardCommands.Copy),
                        new ImmediateMenuCommand(new EventHandler(OnMenuPasteStatus), new EventHandler(OnMenuPaste), StandardCommands.Paste),
                        new MenuCommand(new EventHandler(OnMenuDelete), StandardCommands.Delete)
                    };

                    foreach (MenuCommand command in m_menuCommands)
                    {
                        mc.AddCommand(command);
                    }
                }

                base.Initialize();

              
            }

            void OnComponentRemoved(object sender, ComponentEventArgs args)
            {
                //We need to clean up and remove the Panel from its parent's collection - if it is indeed a panel!
                Sxe.Engine.UI.Panel panel = args.Component as Sxe.Engine.UI.Panel;
                if (panel != null)
                {
                    PanelContainer parent = panel.Parent as PanelContainer;
                    if (parent != null)
                    {
                        parent.Panels.Remove(panel);
                    }
                }
                Invalidate();
            }

            void OnComponentChanged(object sender, ComponentChangedEventArgs args)
            {
                //if (args.Member.Name == "BackgroundPath")
                bool hasReloadAttribute = false;
                foreach (Attribute attr in args.Member.Attributes)
                {
                    if (attr.GetType() == typeof(ReloadContentAttribute))
                    {
                        hasReloadAttribute = true;
                        break;
                    }
                }

                if(hasReloadAttribute)
                {
                    //Load content
                    Sxe.Engine.UI.Panel panel = args.Component as Sxe.Engine.UI.Panel;
                    if (panel != null && content != null)
                        panel.LoadContent(content);
                  
                }

                Invalidate();

            }

            public void OnComponentChanging(object sender, ComponentChangingEventArgs args)
            {
            }

            /// <summary>
            /// This is called to create a given toolbox item at a given location
            /// </summary>
            public void InvokeToolboxItem(System.Drawing.Design.ToolboxItem item, int x, int y)
            {
                    //User has clicked and we need to create a new item
                    if (DesignerHost != null)
                    {
                        using (DesignerTransaction trans = host.CreateTransaction("Creating " + item.DisplayName))
                        {
                            //Most components are just 1 component, but there is the 
                            //possibility that some can return multiple components
                            IComponent[] newComponents = item.CreateComponents(DesignerHost);

                            PanelContainer pc = DesignerHost.RootComponent as PanelContainer;
                            IComponentChangeService cs = (IComponentChangeService)this.DesignerHost.GetService(typeof(IComponentChangeService));

                            PropertyDescriptor panelsProperty = TypeDescriptor.GetProperties(pc)["Panels"];

                            //Let the component change service know we are going to be adding stuff
                            cs.OnComponentChanging(pc, panelsProperty);

                            //System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(x, y, 50, 50);

                            foreach (IComponent comp in newComponents)
                            {
                                if (comp is Sxe.Engine.UI.Panel)
                                {
                                    Sxe.Engine.UI.Panel panel = comp as Sxe.Engine.UI.Panel;

                                    //Tell the panel to load content
                                    if (content != null)
                                        panel.LoadContent(content);

                                    pc.Panels.Add(panel);

                                    //if (pc.Panels.Count == 1)
                                    //{
                                    //    Invalidate();
                                    //}
                                    Invalidate();
                                }

                                PropertyDescriptor location = TypeDescriptor.GetProperties(comp)["Location"];
                                if(location != null && location.PropertyType == typeof(Microsoft.Xna.Framework.Point))
                                {
                                    location.SetValue(comp, new Point(x, y));
                                }

                                //PropertyDescriptor boundsProp = TypeDescriptor.GetProperties(comp)["BoundingBox"];
                                //if (boundsProp != null && boundsProp.PropertyType == typeof(System.Drawing.Rectangle))
                                //{
                                //    boundsProp.SetValue(comp, bounds);
                                //    bounds.X += 10;
                                //    bounds.Y += 10;
                                //}
                            }

                            trans.Commit();

                            //Let the toolbox serivce know we created this guy
                            IToolboxService ts = ToolboxService;
                            if (ts != null)
                            {
                                ts.SelectedToolboxItemUsed();
                            }

                            //Select the new items
                            ISelectionService ss = SelectionService;
                            if (ss != null)
                            {
                                ss.SetSelectedComponents(newComponents, SelectionTypes.Replace);
                            }
                        }



                    }
                
            }

            private void OnSelectionChanged(object sender, EventArgs args)
            {
                 IDesignerHost host = DesignerHost;
                //Check the previous components
                if (m_currentSelection != null)
                {
                    foreach(Component panel in m_currentSelection)
                    {
                        if (host != null)
                        {
                            PanelDesigner designer = host.GetDesigner(panel) as PanelDesigner;
                            if (designer != null)
                            {
                                designer.IsSelected = false;
                            }
                        }

                    }
                    
                }

                m_currentSelection = ((ISelectionService)sender).GetSelectedComponents();

                if (m_currentSelection != null)
                {
                    foreach (Component panel in m_currentSelection)
                    {
                        //Invalide shape bounds
                        if (host != null)
                        {
                            PanelDesigner designer = host.GetDesigner(panel) as PanelDesigner;
                            if (designer != null)
                            {
                                designer.IsSelected = true;
                            }
                        }
                    }
                }

                Invalidate();


                //TODO: Update menu commands
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                if(pen == null)
                    pen = new System.Drawing.Pen(System.Drawing.Color.Black);

                //Draw around our main screen

                int x0 = screenDesigner.PanelContainer.AbsoluteLocation.X;
                int y0 = screenDesigner.PanelContainer.AbsoluteLocation.Y;
                int x1 = x0 + screenDesigner.PanelContainer.Size.X;
                int y1 = y0 + screenDesigner.PanelContainer.Size.Y;

                e.Graphics.DrawLine(pen, x0, y0, x1, y0);
                e.Graphics.DrawLine(pen, x1, y0, x1, y1);
                e.Graphics.DrawLine(pen, x0, y1, x1, y1);
                e.Graphics.DrawLine(pen, x0, y0, x0, y1);

                IDesignerHost host = DesignerHost;
                ISelectionService ss = SelectionService;
                if (m_currentSelection != null && DesignerHost != null
                    && ss != null)
                {
                    foreach (object component in m_currentSelection)
                    {
                        if (component is IComponent)
                        {
                            PanelDesigner designer = host.GetDesigner((IComponent)component) as PanelDesigner;
                            if (designer != null)
                            {
                                bool primary = (component == ss.PrimarySelection);
                                designer.DrawAdornments(e.Graphics, primary);
                            }
                        }
                    }
                }

      

                
            }

            void OnMenuCut(object sender, EventArgs args)
            {
            }

            void OnMenuCopy(object sender, EventArgs args)
            {
                //Serialize the object using the designer serialization service
                IDesignerSerializationService ds = DesignerSerialization;
                if (ds != null)
                {
                    object serializedObject = ds.Serialize(m_currentSelection);
                    DataObject data = new DataObject(AnarchyDataName, serializedObject);
                    Clipboard.SetDataObject(data, true);
                }

            }

            void OnMenuDelete(object sender, EventArgs args)
            {
                IDesignerHost host = DesignerHost;
                if (host != null && m_currentSelection != null && m_currentSelection.Count > 0)
                {
                    using (DesignerTransaction trans = host.CreateTransaction("Delete " + m_currentSelection.Count.ToString() + " component(s)"))
                    {
                        foreach (object o in m_currentSelection)
                        {
                            host.DestroyComponent((IComponent)o);
                        }

                        trans.Commit();
                    }
                }
            }

            void OnMenuPasteStatus(object sender, EventArgs args)
            {
                MenuCommand pasteCommand = sender as MenuCommand;
                bool enabled = false;

                //Check if we have data on the clipboard
                IDataObject data = Clipboard.GetDataObject();
                if (data != null && data.GetDataPresent(AnarchyDataName))
                    enabled = true;

                pasteCommand.Enabled = enabled;
            }

            /// <summary>
            /// Handle the paste command
            /// </summary>
            void OnMenuPaste(object sender, EventArgs args)
            {
                IDesignerHost host = DesignerHost;
                if (host != null)
                {
                    using (DesignerTransaction trans = host.CreateTransaction("Paste"))
                    {
                        IDesignerSerializationService ds = DesignerSerialization;
                        if (ds != null)
                        {
                            //Get data from clipboard
                            IDataObject data = Clipboard.GetDataObject();
                            if (data != null && data.GetDataPresent(AnarchyDataName))
                            {
                                //Loop through all the objects in the clipboard and do shiz
                                ICollection objects = ds.Deserialize(data.GetData(AnarchyDataName));
                                foreach (object o in objects)
                                {
                                    if (o is IComponent)
                                    {
                                        host.Container.Add((IComponent)o);
                                    }

                                    Sxe.Engine.UI.Panel panel = o as Sxe.Engine.UI.Panel;
                                    if (panel != null)
                                    {
                                        PanelContainer panelParent = null;
                                        if(panel.Parent != null)
                                            panelParent = panel.Parent as PanelContainer;

                                        if (panelParent != null)
                                        {
                                            panelParent.Panels.Add(panel);
                                        }
                                        else
                                        {
                                            screenDesigner.PanelContainer.Panels.Add(panel);
                                        }
                                        
                                    }
                                }

                                //Add all items to the selection
                                ISelectionService ss = SelectionService;
                                if (ss != null)
                                {
                                    ss.SetSelectedComponents(objects, SelectionTypes.Replace);
                                }

                                Invalidate();
                            }

                        }
                        trans.Commit();
                    }
                }
            }

            /// <summary>
            /// This lets us override the default drag behavior
            /// </summary>
            /// <param name="drgevent"></param>
            protected override void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)
            {
                base.OnDragEnter(drgevent);

                currentDrag = drgevent.Data.GetData(typeof(DragObject)) as DragObject;
                
                //This is an item we have dragging
                if (currentDrag != null)
                {
                    //Lets make sure that we are dragging from our control onto our control
                    if (currentDrag.DragControl != this)
                    {
                        //Nope, this shouldn't happen though.. but we sure can't allow it
                        currentDrag = null;
                        drgevent.Effect = DragDropEffects.None;
                    }
                    else
                    {
                        drgevent.Effect = DragDropEffects.Move;
                    }
                }
                else
                {
                    //So we don't have a current drag object... But maybe this object is a tool box object
                    IToolboxService ts = ToolboxService;
                    if (ts != null && ts.IsToolboxItem(drgevent.Data, DesignerHost))
                    {
                        //Ok, we have a toolbox item that we are dragging... lets do this
                        drgevent.Effect = DragDropEffects.Copy;

                        output = "BUTT";
                        Invalidate();
                        
                    }

                }
            }

            protected override void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)
            {
                base.OnDragOver(drgevent);

                DragObject action = currentDrag;
                IDesignerHost host = DesignerHost;


                System.Drawing.Point actualCoordinates = PointToClient(
                    new System.Drawing.Point(drgevent.X, drgevent.Y));

                //if (lastDragPanel != null)
                //    if (lastDragPanel.Image != null)
                //        lastDragPanel.Image.Color = Color.White;

                lastDragPanel = null;

                if (host != null && action != null)
                {
                    //output = screenDesigner.PanelContainer.Panels.ToString();

                    //foreach (Sxe.Engine.UI.Panel panel in screenDesigner.PanelContainer.Panels)
                    for(int i = screenDesigner.PanelContainer.Panels.Count - 1;
                        i >= 0; i--)
                    {
                        Sxe.Engine.UI.Panel panel = screenDesigner.PanelContainer.Panels[i];
                        PanelDesigner pd = host.GetDesigner(panel) as PanelDesigner;
                        if (pd != null)
                        {

                            DesignerHitResult hitResult = pd.GetHitTest(actualCoordinates.X, actualCoordinates.Y, action.Panels);

                            if (hitResult.HitObject != null)
                            {
                                if (currentDrag == null || !(action.Panels.Contains(hitResult.HitPanel)))
                                {
                                    lastDragPanel = hitResult.HitPanel;

                                    //if (lastDragPanel.BackColor != null)
                                    //    lastDragPanel.BackColor = Microsoft.Xna.Framework.Graphics.Color.Green;

                                    Invalidate();


                                    break;
                                }
                            }
                        }
                    }


             

                    //Resize bounds and shiz
                    int xDelta = actualCoordinates.X - action.MouseStartX;
                    int yDelta = actualCoordinates.Y - action.MouseStartY;


                    if (host != null)
                    {
                        //throw new Exception(action.Panels.Count.ToString());
                        foreach (Component component in action.Panels)
                        {

                            PanelDesigner designer = host.GetDesigner(component) as PanelDesigner;
                            if (designer != null)
                            {
                                //designer.IsDragging = true;
                                designer.Drag(action.HitObject, xDelta, yDelta);
                            }
                        }
                    }
                }

                Invalidate();

            }

            protected override void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)
            {
                //Do we have a control we are dragging?
                if (currentDrag != null)
                {
                    IDesignerHost host = DesignerHost;
                    if(DesignerHost != null)
                    {
                        foreach (Component component in currentDrag.Panels)
                        {
                            PanelDesigner designer = host.GetDesigner(component) as PanelDesigner;
                            if (designer != null)
                                designer.EndDrag();

                           
                            Sxe.Engine.UI.Panel oldParent = designer.Panel.Parent;

                            //Let the panel know we dragged it onto them
                            if (lastDragPanel != null)
                            {
                                PanelDesigner dropDesigner = host.GetDesigner(lastDragPanel) as PanelDesigner;

                                //lastDragPanel.BackColor = Color.Yellow;

                                dropDesigner.DropPanel(designer);
                            }
                            //If we drag a panel that has a different parent, we need to make them a child
                            //of the root again. This is like if you drag a child out of a panel back into the open.
                            //The only thing is, we have to check if the parent is being dragged as well
                            else
                            {
                                IComponentChangeService cs = ComponentChangeService;
                                if (cs != null)
                                {
                                    using (DesignerTransaction trans = DesignerHost.CreateTransaction("Reassigning"))
                                    {
                                        cs.OnComponentChanging(designer.Panel, null);
                                        cs.OnComponentChanging(designer.Panel.Parent, null);
                                        cs.OnComponentChanging(screenDesigner.PanelContainer, null);

                                        //If this had an old parent, we need to convert the parents coordinates to ours

                                        if(designer.Panel.Parent != null)
                                        {
                                            designer.Panel.Location = new Point(
                                                designer.Panel.Location.X + designer.Panel.Parent.AbsoluteLocation.X - this.screenDesigner.PanelContainer.AbsoluteLocation.X,
                                                designer.Panel.Location.Y + designer.Panel.Parent.AbsoluteLocation.Y - this.screenDesigner.PanelContainer.AbsoluteLocation.Y);
                                        }

                                        Invalidate();
                                        

                                        if (!screenDesigner.PanelContainer.Panels.Contains(designer.Panel)
                                            && !currentDrag.Panels.Contains(designer.Panel.Parent))
                                        {
                                            screenDesigner.PanelContainer.Panels.Add(designer.Panel);
                                        }
                                        trans.Commit();
                                    }
                                }
                            }
                        }
                    }
                    currentDrag = null;
                }
                //We have a toolbox item, probably
                else
                {
                    IToolboxService ts = ToolboxService;
                    if (ts != null && ts.IsToolboxItem(drgevent.Data, DesignerHost))
                    {
                        System.Drawing.Design.ToolboxItem item = ts.DeserializeToolboxItem(drgevent.Data);
                        System.Drawing.Point point = new System.Drawing.Point(drgevent.X, drgevent.Y);
                        //Put the point in our control space
                        point = PointToClient(point);
                        InvokeToolboxItem(item, point.X, point.Y);
                    }
                }

                //Clear the last drag panel
                if (lastDragPanel != null)
                    lastDragPanel = null;

                base.OnDragDrop(drgevent);
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);

                ISelectionService ss = SelectionService;
                if (ss != null)
                {
                    object hitObject = null;

                    if (lastHitPanel != null)
                    {
                        hitObject = lastHitPanel;
                    }
                    else
                    {
                        IDesignerHost host = DesignerHost;
                        if (host != null)
                        {
                            hitObject = host.RootComponent;
                        }
                    }
                

                //Are there any controls underneath us?
                    if(hitObject != null)
                        ss.SetSelectedComponents(new object[] { hitObject }, SelectionTypes.Replace);
                }
                
            }

            /// <summary>
            /// Override default MouseMove functionality, so we can detect stuff
            /// </summary>
            /// <param name="e"></param>
            protected override void  OnMouseMove(MouseEventArgs e)
            {

                base.OnMouseMove(e);

                //if (lastHitPanel != null && lastHitPanel.Image != null)
                //    lastHitPanel.Image.Color = Color.White;

                lastHitPanel = null;
                lastHitObject = null;
                lastDragPanel = null; 

                //Loop through all panels and see if we hit one
                IDesignerHost host = DesignerHost;
                if (host != null)
                {
                    for(int i = screenDesigner.PanelContainer.Panels.Count - 1;
                        i >= 0; i--)
                    //foreach (Sxe.Engine.UI.Panel panel in screenDesigner.PanelContainer.Panels)
                    {
                        Sxe.Engine.UI.Panel panel = screenDesigner.PanelContainer.Panels[i];
                        PanelDesigner ps = host.GetDesigner(panel) as PanelDesigner;
                        if (ps != null)
                        {
                            
                            DesignerHitResult hitResult = ps.GetHitTest(e.X, e.Y);
                            if (hitResult.HitObject != null)
                            {
                                lastHitPanel = hitResult.HitPanel;
                                lastHitObject = hitResult.HitObject;

                                //if(panel.Image != null)
                                //panel.Image.Color = Color.Red;
                                Cursor = ps.GetCursor(lastHitObject);
                                break;
                            }
                        }

                    }
                }


                if (lastHitPanel == null)
                {
                    Cursor = Cursors.Default;
                }
                else
                {
                    //If the cursor is down, lets start some drag action contraption
                    if (e.Button == MouseButtons.Left)
                    {
                        

                        this.currentDrag = new DragObject(this,
                            lastHitObject, e.X, e.Y);
                        if (lastHitPanel.Parent != null)
                            lastDragPanel = lastHitPanel.Parent;

                       

                        //Add all objects to the selection
                        ISelectionService ss = SelectionService;
                        if (ss != null)
                        {
                            ICollection selected = ss.GetSelectedComponents();
                            foreach (Component obj in selected)
                            {
                                if (obj != null)
                                {
                                    PanelDesigner designer = host.GetDesigner(obj) as PanelDesigner;
                                    if (designer != null)
                                    {
                                        currentDrag.Panels.Add(obj);
                                        designer.StartDrag();
                                    }
                                }

                                
                            }
                        }

                        DoDragDrop(currentDrag, DragDropEffects.Move);
                    }
                }

           
                //Invalidate();
            }

            protected override void Draw()
            {
                base.Draw();

                if (screenDesigner.PanelContainer != null && spriteBatch != null)
                {
                    screenDesigner.PanelContainer.Draw(spriteBatch, gameTime);

                    //string str = proj.FullName + counter.ToString();

                    string str = counter.ToString();

                    //if (currentDrag != null)
                        //str = "CurrentDrag: " + currentDrag.Panels.Count.ToString();
                        str = screenDesigner.PanelContainer.Panels.Count.ToString();

                  
                        spriteBatch.Begin();

                        if (currentDrag != null)
                            output = "Current drag not null";

                    if(output != null)
                    spriteBatch.DrawString(font, output, new Vector2(10, 10), Color.White);

                //spriteBatch.DrawString(font, this.DisplayRectangle.ToString(), new Vector2(10, 50), Color.White);
                    //DTE dte = (DTE)screenDesigner.GetService(typeof(DTE));
                    //if (dte != null)
                    //{
                    //    object[] projects = (object[])dte.ActiveSolutionProjects;
                    //    int i = 1;

                    //    Project topProject = projects[0] as Project;

                    //        foreach (Project project in topProject.Collection)
                    //        {
                    //            foreach (ProjectItem items in project.ProjectItems)
                    //            {


                                  
                    //                //items.Nam
                    //                if(items.ContainingProject != null)
                    //                {
                    //                    if(items.ContainingProject.Name == "Content")
                    //                    {

                    //                       // items.
                    //                        //if(items.Object != null)
                    //                        spriteBatch.DrawString(font, items.Name.ToString(), new Vector2(1, i), Color.White);
                    //                        i += 10;
                    //                        foreach (Property prop in items.Properties)
                    //                        {
                    //                            spriteBatch.DrawString(font, "++" + prop.Name, new Vector2(1, i), Color.White);
                    //                            i += 10;

                    //                        }

     
                                
                    //                        foreach (ProjectItem item in items.ProjectItems)
                    //                        {
                    //                            spriteBatch.DrawString(font, "**" + item.Name, new Vector2(1, i), Color.White);
                    //                            i += 10;

                    //                            foreach (Property prop in item.Properties)
                    //                            {
                    //                                spriteBatch.DrawString(font, "----" + prop.Name, new Vector2(1, i), Color.White);
                    //                                i += 10;

                    //                            }
                    //                        }

                                            
                    //                        //foreach (Property prop in items.Properties)
                    //                        //{
                    //                        //    spriteBatch.DrawString(font, "--" + prop.Value.ToString(), new Vector2(1, i), Color.White);
                    //                        //    i += 10;
                    //                        //}
                    //                    }
                    //                }

                    //            }
                    //        }

                            //Project proj = project as Project;

                            //if (output != null)
                                //spriteBatch.DrawString(font, project.FileName.ToString(), new Vector2(1, i), Color.White);

            
                        spriteBatch.End();
                    
                }
                counter++;
            }

                
            
        }

        /// <summary>
        /// Class to hold information for supporting drag and drop stuff
        /// </summary>
        private class DragObject
        {
            private Control dragControl;
            public Control DragControl
            {
                get { return dragControl; }
                //set { dragControl = value; }
            }

            private object hitObject;
            public object HitObject
            {
                get { return hitObject; }
                //set { hitObject = value; }
            }

            int mouseX;
            int mouseY;

            public int MouseStartX
            {
                get { return mouseX; }
            }

            public int MouseStartY
            {
                get { return mouseY; }
            }

            ArrayList panels = new ArrayList();
            public ArrayList Panels
            {
                get { return panels; }
            }

            

            public DragObject(Control inControl,object inObject, int inX, int inY)
            {
                dragControl = inControl;
                hitObject = inObject;
                mouseX = inX;
                mouseY = inY;
            }
        }


        private class ImmediateMenuCommand : MenuCommand
        {
            private EventHandler statusHandler;

            public ImmediateMenuCommand(EventHandler inStatusHandler, EventHandler invokeHandler, CommandID id)
                : base(invokeHandler, id)
            {
                statusHandler = inStatusHandler;
            }

            /// <summary>
            /// This lets the development environment know something about the command. I dont' know what.
            /// I just know that we can update our status here...
            /// </summary>
            public override int OleStatus
            {
                get
                {
                    if (statusHandler != null)
                        statusHandler(this, EventArgs.Empty);

                    return base.OleStatus;
                }
            }
        }
    }
}
