using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;

namespace Sxe.Engine.Test.Framework
{
    /// <summary>
    /// Searches through assembly for all graphical test cases
    /// </summary>
    public class GraphicalTestSuite
    {
        const string DefaultCategory = "General";

        List<GraphicalTestInfo> testList;

        public List<GraphicalTestInfo> Tests
        {
            get { return testList; }
        }

        public GraphicalTestSuite()
        {
            testList = new List<GraphicalTestInfo>();

            InitializeTestCases();
        }

        void InitializeTestCases()
        {
#if !XBOX
            AppDomain currentApp = AppDomain.CurrentDomain;
            foreach(Assembly assembly in currentApp.GetAssemblies())
            {
            
#else
                Assembly assembly = Assembly.GetExecutingAssembly();
#endif
                foreach (Type t in assembly.GetTypes())
                {
                    //Check if the type is a test fixture
                    if (t.BaseType == typeof(UI.TestScreen))
                    {

                        //Look through attributes and see if there is a custom category
                        string category = DefaultCategory;
                        foreach (object attribute in t.GetCustomAttributes(typeof(GraphicalTestCategory), false))
                        {
                            GraphicalTestCategory graphicalAttribute = attribute as GraphicalTestCategory;
                            category = graphicalAttribute.Name;
                            
                        }

                        GraphicalTestInfo testInfo = new GraphicalTestInfo(t, category);
                        testList.Add(testInfo);

                    }
                }
#if !XBOX
            }
#endif
        }


    }
}
