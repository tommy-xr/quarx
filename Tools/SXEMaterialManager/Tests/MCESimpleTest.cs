using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using NUnit.Framework;

namespace SXEMaterialManager.Tests
{
    [TestFixture]
    public class MCESimpleTest
    {
        MaterialCollectionEditor mce;


        [TestFixtureSetUp]
        public void Setup()
        {
            mce = MaterialCollectionEditor.CreateNew("TestContent", null, null);
        }

        [Test]
        public void AddCategoryTest()
        {



        }

        [Test]
        public void AddDirectoryTest()
        {
            //Add a directory from testContent

           
        }


        [TestFixtureTearDown]
        public void TearDown()
        {
        }
    }
}
