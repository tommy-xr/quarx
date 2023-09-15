using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using Sxe.Engine.UI;

namespace Sxe.Engine.Test.Framework
{
    public class GraphicalTestInfo
    {
        Type testType;
        string category;

        public Type TestType
        {
            get { return testType; }
        }
        public string Category
        {
            get { return category; }
        }

        public GraphicalTestInfo(Type type, string testCategory)
        {
            testType = type;
            category = testCategory;
        }

        public void Run(IGameScreenService service,  ContentManager content)
        {
            //Instantiate the type

            object test = System.Activator.CreateInstance(testType);
            TestScreen testScreen = test as TestScreen;
            //testScreen.Initialize(service,content);

            //Then switch to its game screen

            service.AddScreen(testScreen);


        }
    }
}
