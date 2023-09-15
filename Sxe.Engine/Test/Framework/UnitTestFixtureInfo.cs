using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Sxe.Engine.Test.Framework
{
    /// <summary>
    /// Stores the information for a test case
    /// </summary>
    public class UnitTestFixtureInfo
    {

        #region Fields
        Type testType;
        MethodInfo setupMethod;
        MethodInfo tearDownMethod;
        List<UnitTestCaseInfo> testCases;

        #endregion

        #region Properties
        public Type TestType
        {
            get { return testType; }
        }
        public MethodInfo Setup
        {
            get { return setupMethod; }
        }
        public MethodInfo TearDown
        {
            get { return tearDownMethod; }
        }
        public List<UnitTestCaseInfo> TestCases
        {
            get { return testCases; }
        }
        #endregion Properties

        public UnitTestFixtureInfo(Type inType, MethodInfo setup, MethodInfo tearDown)
        {
            testType = inType;
            setupMethod = setup;
            tearDownMethod = tearDown;

           

            testCases = new List<UnitTestCaseInfo>();
        }

        public void AddTestCase(MethodInfo mi)
        {
            UnitTestCaseInfo info = new UnitTestCaseInfo(mi);
            testCases.Add(info);
        }

        /// <summary>
        /// Runs all test cases
        /// </summary>
        public void Run()
        {
            object testObject = System.Activator.CreateInstance(testType);

            foreach (UnitTestCaseInfo ti in testCases)
            {
                if(setupMethod != null)
                setupMethod.Invoke(testObject, null);


                ti.Run(testObject);

                if(tearDownMethod != null)
                tearDownMethod.Invoke(testObject, null);
            }
        }




        
    }
}
