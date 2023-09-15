using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;

using Sxe.Engine.UI;

namespace Sxe.Engine.Test
{
    public enum TestResult
    {
        Unknown,
        Pass,
        Fail
    }


    /// <summary>
    /// Stores the information related to each test case
    /// </summary>
    public class UnitTestCaseInfo
    {
        #region Fields
        MethodInfo testMethod;
        TestResult testResult = TestResult.Pass;
        string consoleOut = "";
        string errorOut = "";
        #endregion

        #region Properties
        public MethodInfo TestMethod
        {
            get { return testMethod; }
        }
        public TestResult Result
        {
            get { return testResult; }
        }
        public string ConsoleOutput
        {
            get { return consoleOut; } 
        }
        public string Errors
        {
            get { return errorOut; }
        }
        #endregion

        public UnitTestCaseInfo(MethodInfo method)
        {
            testMethod = method;
        }

        public void Run(object invoker)
        {
            errorOut = "[No exceptions were thrown.]";

            StringWriter writer = new StringWriter();

            TextWriter oldOut = Console.Out;
           
            Console.SetOut(writer);
            //TODO: add this back in
            //writer.NewLine = TextBox.NewLine;

            testResult = TestResult.Pass;
            try
            {
                testMethod.Invoke(invoker, null);
            }
            catch (Exception ex)
            {
                testResult = TestResult.Fail;
                errorOut = ex.InnerException.Message + "\n" + ex.InnerException.StackTrace;
            }
            finally
            {


                consoleOut = writer.ToString();
                
            }
            Console.SetOut(oldOut);

        }

    }
}
