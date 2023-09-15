//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.IO;

//using Sxe.Engine.UI;
//using Sxe.Engine.Test.Framework;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;

//namespace Sxe.Engine.Test.Cases.Graphical
//{ 
//    [GraphicalTestCategory("UI")]
//    public class MenuTest : TestScreen
//    {
//        MenuGroup menuList;
        
//        public override void TestInitialize(IGameScreenService service, ContentManager content)
//        {
//            Panel.Image = new UIImage(content.Load<Texture2D>("Content/Default/UI/back2"));
//            menuList = new MenuGroup(this);
           
//            UIImage defaultImage = new UIImage(content.Load<Texture2D>("Content/TestContent/Menu/button2_default"));
//            UIImage overImage = new UIImage(content.Load<Texture2D>("Content/TestContent/Menu/button2_over"));
//            UIImage selectedImage = new UIImage(content.Load<Texture2D>("Content/TestContent/Menu/button2_down"));

//            //MenuButton mb1 = new MenuButton(Panel, new Point(5, 5), new Point(90, 100), defaultImage, defaultScheme);
//            MenuButton mb1 = new MenuButton();
//            mb1.Parent = Panel;
//            mb1.Location = new Point(5, 5);
//            mb1.Size = new Point(90, 100);
//            mb1.DefaultImage = defaultImage;
//            mb1.OverImage = overImage;
//            mb1.SelectedImage = selectedImage;
            
//            //MenuButton mb2 = new MenuButton(Panel, new Point(5, 110), new Point(90, 100), defaultImage, defaultScheme);
//            MenuButton mb2 = new MenuButton();
//            mb2.Parent = Panel;
//            mb2.Location = new Point(5, 110);
//            mb2.Size = new Point(90, 100);
//            mb2.DefaultImage = defaultImage;
//            mb2.OverImage = overImage;
//            mb2.SelectedImage = selectedImage;

//            menuList.AddMenuObject(mb1);
//            menuList.AddMenuObject(mb2);

//            object test = content.Load<object>("Content/TestContent/Menu/testpanel");



//            base.TestInitialize(service, content);
//        }
//    }

//}
