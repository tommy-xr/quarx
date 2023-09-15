//using System;
//using System.Collections.Generic;
//using System.Text;

//using Sxe.Engine.UI;
//using Sxe.Engine.Test.Framework;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;

//namespace Sxe.Engine.Test.Cases.Graphical
//{ 
//    [GraphicalTestCategory("Category1")]
//    public class Test1 : TestScreen
//    {
//        public Test1()
//        {

//        }

//        public override void TestInitialize(IGameScreenService service,  ContentManager content)
//        {
//            Panel.Image = new UIImage(content.Load<Texture2D>("Content/Default/UI/back2"));


//            UIImage folderImage = new UIImage(content.Load<Texture2D>("Content/Default/UI/folderIcon"));
//            //Form form1 = new Form(new Point(100, 100), new Point(320, 400), scheme, folderImage);
//            Form form1 = new Form(service.Schemes);
//            //form1.Parent = this;
//            form1.Location = new Point(100, 100);
//            form1.Size = new Point(320, 400);
//            form1.Text = "Form 1";

//            //Form form2 = new Form(new Point(50, 50), new Point(100, 100), scheme);
//            Form form2 = new Form(service.Schemes);
//            //form2.Parent = this;
//            form2.Location = new Point(50, 50);
//            form2.Size = new Point(100, 100);
//            form2.Text = "Form";

//            //Label l1 = new Label(form1.ClientArea, new Point(10, 10), new Point(100, 40), scheme.TitleFont, Color.White, scheme);
//            Label l1 = new Label();
//            l1.Parent = form1.ClientArea;
//            l1.Location = new Point(10, 10);
//            l1.Size = new Point(100, 40);
//            l1.HorizontalAlignment = HorizontalAlignment.Center;

//            //Textbox text = new Textbox(form1.ClientArea, new Point(20, 200), new Point(100, 32),
//            //    scheme.TextBoxImage, scheme.TextBoxActiveImage, Color.White,
//            //    scheme.DefaultFont, Color.Black, scheme);

//            //TextBox text = new TextBox(form1.ClientArea, new Point(20, 200), new Point(100, 32), scheme, 100);


//            //Button b = new Button(form1.ClientArea, new Point(0, 50), new Point(100, 100),
//            //    scheme.DefaultFont, scheme.CommandButtonImage, scheme.CommandButtonOverImage, scheme.CommandButtonClickImage,
//            //    Color.White, BorderPanelMode.Resize, scheme);

//            //Checkbox c = new Checkbox(form1.ClientArea, new Point(100, 0), scheme.CheckSize, scheme.DefaultFont, "test",
//            //    Color.Black, new Point(10, 0), new Point(100, 20), scheme.CheckBoxImage, scheme.CheckBoxOverImage, scheme.CheckImage, scheme);
//            //c.Text = "Checkbox1";

//            //TextBox mll = new TextBox(form1, new Point(200, 200), new Point(100, 200), scheme, 100);


//            //ComboBox combo1 = new ComboBox(form1.ClientArea, new Point(50, 100), new Point(100, 100), scheme);
//            //combo1.Items.Add("hey");
//            //combo1.Items.Add("hey1");
//            //combo1.Items.Add("hey2");
//            //combo1.Items.Add("hey3");
//            //combo1.Items.Add("hey4");



//            //Form form3 = new Form(new Point(80, 80), new Point(200, 300), scheme);
//            //TreeView tv = new TreeView(form3.ClientArea, new Point(0, 0), new Point(170, 200), scheme);
//            //TreeNode node1 = new TreeNode();
//            //node1.Text = "Hey1";
//            //node1.Image = folderImage;
//            //TreeNode node2 = new TreeNode();
//            //node2.Text = "Hey2";
//            //TreeNode node3 = new TreeNode();
//            //node3.Text = "Child1";
//            //node3.Image = folderImage;
//            //node2.Nodes.Add(node3);
//            //TreeNode node4 = new TreeNode();
//            //node4.Text = "Child2";
//            //node3.Nodes.Add(node4);
//            //tv.Nodes.Add(node1);
//            //tv.Nodes.Add(node2);

//            //for (int i = 0; i < 15; i++)
//            //{
//            //    TreeNode nodeX = new TreeNode();
//            //    nodeX.Text = "Extra node" + i.ToString();
//            //    tv.Nodes.Add(nodeX);
//            //}


//            //Forms.AddForm(form1);
//            //Forms.AddForm(form2);
//            //Forms.AddForm(form3);

//            //Form form5 = new Form(new Point(10, 10), new Point(200, 400), scheme);
//            //PropertyGrid pg = new PropertyGrid(form5.ClientArea, Point.Zero, new Point(165, 350), scheme);

//            //Form tabForm = new Form(new Point(15, 15), new Point(200, 300), scheme);
//            //tabForm.Text = "Tab Form";
//            //TabPage tc1 = new TabPage(scheme);
//            //Label l = new Label(tc1.Panel, new Point(1, 1), new Point(50, 10), scheme.DefaultFont, Color.Black, scheme);
//            //l.Caption = "TC1";
//            //TabPage tc2 = new TabPage(scheme);
//            //Label l2 = new Label(tc2.Panel, new Point(1, 1), new Point(50, 10), scheme.DefaultFont, Color.Black, scheme);
//            //l2.Caption = "TC2";

//            //TabControl tc = new TabControl(tabForm.ClientArea, new Point(0, 0), new Point(170, 270), scheme);
//            //tc.Tabs.Add(tc1);
//            //tc.Tabs.Add(tc2);

//            //Forms.AddForm(tabForm);
//            //Forms.AddForm(form5);
//            base.TestInitialize(service,  content);
//        }
//    }

//}
