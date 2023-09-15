using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.Utilities.Console;
using Sxe.Engine.Test.Framework;

namespace Sxe.Engine.Test.Cases.Unit
{
    [SxeTestFixture]
    public class TestConsoleCommandParser
    {
        ConsoleCommandParser parser = new ConsoleCommandParser();

        [SxeTest]
        public void SimpleTest1()
        {
            string command = "sv_cheats 1";
            string[] output = parser.ConsoleTokenize(command);
            Assert.AreEqual("sv_cheats", output[0]);
            Assert.AreEqual("1", output[1]);
        }

        [SxeTest]
        public void SimpleTest2()
        {
            string command = "command 1 2";
            string[] output = parser.ConsoleTokenize(command);
            Assert.AreEqual("command", output[0]);
            Assert.AreEqual("1", output[1]);
            Assert.AreEqual("2", output[2]);
        }

        [SxeTest]
        public void QuoteTest1()
        {
            string command = "\"command\" \"1\" \"2\"";
            string[] output = parser.ConsoleTokenize(command);
            Assert.AreEqual("command", output[0]);
            Assert.AreEqual("1", output[1]);
            Assert.AreEqual("2", output[2]);
        }

        [SxeTest]
        public void QuoteTest2()
        {
            string command = "\"big command\" \"space1 space2 space3\"";
            string[] output = parser.ConsoleTokenize(command);
            Assert.AreEqual("big command", output[0]);
            Assert.AreEqual("space1 space2 space3", output[1]);
        }
    }
}
