using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;

namespace Sxe.Engine.Test.Framework
{
    /// <summary>
    /// Searches through assembly for all test cases
    /// </summary>
    public class UnitTestSuite
    {
     

        List<UnitTestFixtureInfo> testInfo;

        public List<UnitTestFixtureInfo> Tests
        {
            get { return testInfo; }
        }

        public UnitTestSuite()
        {
            testInfo = new List<UnitTestFixtureInfo>();

            InitializeTestCases();
        }

        public void RunAll()
        {
            foreach (UnitTestFixtureInfo info in testInfo)
                info.Run();
        }

        void InitializeTestCases()
        {

            AppDomain currentApp = AppDomain.CurrentDomain;
#if XBOX
            Assembly assembly = Assembly.GetExecutingAssembly();
#else

            foreach(Assembly assembly in currentApp.GetAssemblies())
            {
#endif
                foreach (Type t in assembly.GetTypes())
                {
                    //Check if the type is a test fixture
                    if (t.GetCustomAttributes(typeof(SxeTestFixture), false).Length > 0)
                    {


                        //Now see if we can find a test setup and teardown
                        MethodInfo setup = null;
                        MethodInfo teardown = null;

                        foreach (MethodInfo mi in t.GetMethods())
                        {
                            if (DoesMethodContainAttribute<SxeTestSetup>(mi))
                                setup = mi;
                            else if (DoesMethodContainAttribute<SxeTestTearDown>(mi))
                                teardown = mi;
                        }

                        UnitTestFixtureInfo ti = new UnitTestFixtureInfo(t, setup, teardown);

                        foreach (MethodInfo mi in t.GetMethods())
                        {
                            if (DoesMethodContainAttribute<SxeTest>(mi))
                                ti.AddTestCase(mi);
                        }

                        testInfo.Add(ti);

                    }
                }
#if !XBOX
            }
#endif
        }

        bool DoesMethodContainAttribute<T>(MethodInfo mi)
        {
            object[] attributes = mi.GetCustomAttributes(false);
            foreach (object obj in attributes)
            {
                if (obj is T)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
