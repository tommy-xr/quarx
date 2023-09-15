using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Editor.UI;
using Sxe.Library.Editor;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Editor
{
    public class EditorScreen : FormScreen
    {
        SplitPanel splitter;
        SplitPanel splitter2;

        EditorMenuPanel menuPanel;

        Panel mainPanel;
        public Panel MainPanel
        {
            get { return mainPanel; ; }
        }



        //public EditorScreen(IGameScreenService service, ContentManager content)
        //    : base(service, content)
        //{
        //}

        //public override void Initialize(IGameScreenService service, Microsoft.Xna.Framework.Content.ContentManager content)
        //{


        //    base.Initialize(service, content);

        //    EditorMenu data = content.Load<EditorMenu>("editor_menu");



        //    int panelSize = 50;

        //    mainPanel = new Panel();
        //    mainPanel.Parent = this.Panel;
        //    mainPanel.Location = new Point(0, 50);
        //    mainPanel.Size = new Point(this.Panel.Size.X, this.Panel.Size.Y - 50);

        //    //splitter = new SplitPanel();
        //    //splitter.Parent = this.Panel;
        //    //splitter.Location = new Point(0, 50);
        //    //splitter.Size = new Point(this.Panel.Size.X, this.Panel.Size.Y - 50);
        //    //splitter.Orientation = SplitPanelOrientation.Vertical;
        //    //splitter.SetSplitter(100);

        //    //ImageBrowser browser = new ImageBrowser();
        //    //browser.Parent = splitter.Panel1;
        //    //browser.Location = Point.Zero;
        //    //browser.Size = splitter.Panel1.Size;

        //    Form f = new Form(service.Schemes);
        //    this.Forms.AddForm(f);
        //    f.Visible = true;
        //    f.Location = new Point(10, 10);
        //    f.Size = new Point(300, 300);

        //    menuPanel = new EditorMenuPanel(this, data);
    

        //    //splitter2 = new SplitPanel();
        //    //splitter2.Parent = splitter.Panel2;
        //    //splitter2.Orientation = SplitPanelOrientation.Vertical;
        //    //splitter2.Location = new Point(0, 0);
        //    //splitter2.Size = splitter.Panel2.Size;
        //    //splitter2.Panel1.BackColor = Color.Green;
        //    //splitter2.Panel2.BackColor = Color.Gray;
        //    //splitter2.ResetSplitter(true);

        //    IScheme scheme = service.Schemes.LoadSchemeFromFile("Data/editor_scheme.res");

        //    this.Panel.ApplyScheme(scheme);
        //}
    }
}
