using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using Sxe.Engine.Test.Framework;
using Sxe.Library;

namespace Sxe.Engine.Test.Cases.Unit
{
    [SxeTestFixture]
    public class FastTextureSpaceTest
    {
        [SxeTest]
        public void SimpleTest()
        {
            //Test border case - if size is 0, make sure we have no children
            FastTextureSpace space = new FastTextureSpace(0, 1, 1);
            Assert.AreEqual(0, space.Size);

            //This should not have children either
            FastTextureSpace space2 = new FastTextureSpace(1, 1, 1);
            Assert.AreEqual(1, space2.Size);


            FastTextureSpace space3 = new FastTextureSpace(2, 2, 1);
            Assert.AreEqual(2, space3.Size);

        }


        [SxeTest]
        public void BasicTest()
        {
            FastTextureSpace space = new FastTextureSpace(4, 4, 1);
            //Get the full size node from it
            FastTextureSpace.FastTextureSpaceNode node = space.GetFreeNode(4);
            //Verify that it is proper size and stuff
            Assert.AreEqual(4, node.Size);
            Assert.AreEqual(new Vector2(0.0f), node.TexturePosition);
            Assert.AreEqual(new Vector2(1.0f), node.TextureSize);
            Assert.AreEqual(new Rectangle(0, 0, 4, 4), node.Rectangle);
            node.Occupy();

            //Verify that we can't get another
            Assert.IsNull(space.GetFreeNode(4));
            Assert.IsNull(space.GetFreeNode(2));
            Assert.IsNull(space.GetFreeNode(1));
            Assert.IsNull(space.GetFreeNode(0));
        }
    }
}
