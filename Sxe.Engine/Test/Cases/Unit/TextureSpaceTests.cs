using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using Sxe.Engine.Test.Framework;
using Sxe.Library;

namespace Sxe.Engine.Test.Cases.Unit
{
    [SxeTestFixture]
    public class TextureSpaceTests
    {
        [SxeTest]
        public void SimpleTest()
        {
            //Test border case - if size is 0, make sure we have no children
            TextureSpace space = new TextureSpace(0);
            Assert.AreEqual(0, space.Size);

            //This should not have children either
            TextureSpace space2 = new TextureSpace(1);
            Assert.AreEqual(1, space2.Size);


            TextureSpace space3 = new TextureSpace(2);
            Assert.AreEqual(2, space3.Size);

        }

        [SxeTest]
        //Tests if having cells occupied works properly
        public void OccupiedTest()
        {
            TextureSpace space = new TextureSpace(4);
            space.AllowResize = true;

            //Verify that we can get some nodes out of here
            Assert.AreObjectsEqual(space.Root, space.GetFreeNode(4));
            space.Root.Subdivide();
            space.Root[0].Subdivide();
            Assert.AreObjectsEqual(space.Root[0], space.GetFreeNode(2));
            Assert.AreObjectsEqual(space.Root[0][0], space.GetFreeNode(1));
            Assert.AreObjectsEqual(null, space.GetFreeNode(0));

            //Now, grab a node of size 1, and mark as occupied
            TextureSpaceNode size2Node = space.GetFreeNode(1);
            size2Node.Occupied = true;

            //The first root node should now be occupied, but the rest shouldn't be
            Assert.AreEqual(true, space.Root[0].Occupied);
            Assert.AreEqual(false, space.Root[1].Occupied);
            Assert.AreEqual(false, space.Root[2].Occupied);
            Assert.AreEqual(false, space.Root[3].Occupied);

            //If we grab 4 more nodes of size 2, then both the first and second should be occupied
            for (int i = 0; i < 4; i++)
            {
                TextureSpaceNode tempNode = space.GetFreeNode(1);
                tempNode.Occupied = true;
            }

            Assert.AreEqual(true, space.Root[0].Occupied);
            Assert.AreEqual(true, space.Root[1].Occupied);
            Assert.AreEqual(false, space.Root[2].Occupied);
            Assert.AreEqual(false, space.Root[3].Occupied);


        }


        [SxeTest]
        public void TexturePositionSizeTests()
        {
            TextureSpace space = new TextureSpace(4);

            //Test the positions of the main 
            space.Root.Subdivide();
            Assert.AreEqual(space.Root[0].TexturePosition, Vector2.Zero);
            Assert.AreEqual(space.Root[1].TexturePosition, new Vector2(0.5f, 0.0f));
            Assert.AreEqual(space.Root[2].TexturePosition, new Vector2(0.0f, 0.5f));
            Assert.AreEqual(space.Root[3].TexturePosition, new Vector2(0.5f, 0.5f));

            //Test the sizes of the main nodes
            Vector2 expectedSize = new Vector2(0.5f);
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(expectedSize, space.Root[i].TextureSize);

            //Test the sizes of a child node

            TextureSpaceNode childNode = space.Root[1];
            childNode.Subdivide();

            Assert.AreEqual(new Vector2(0.5f, 0.0f), childNode[0].TexturePosition);
            Assert.AreEqual(new Vector2(0.75f, 0.0f), childNode[1].TexturePosition);
            Assert.AreEqual(new Vector2(0.5f, 0.25f), childNode[2].TexturePosition);
            Assert.AreEqual(new Vector2(0.75f, 0.25f), childNode[3].TexturePosition);

            expectedSize = new Vector2(0.25f);
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(expectedSize, childNode[i].TextureSize);


        }

        /// <summary>
        /// Verify that nodes give proper rectangles
        /// </summary>
        [SxeTest]
        public void TestRectangle()
        {
            TextureSpace space = new TextureSpace(8);
            space.Root.Subdivide();
            Assert.AreEqual(new Rectangle(0, 0, 4, 4), space.Root[0].Rectangle);
            Assert.AreEqual(new Rectangle(4, 0, 4, 4), space.Root[1].Rectangle);
            Assert.AreEqual(new Rectangle(0, 4, 4, 4), space.Root[2].Rectangle);
            Assert.AreEqual(new Rectangle(4, 4, 4, 4), space.Root[3].Rectangle);

            //Check out some child nodes
            space.Root[0].Subdivide();
            space.Root[0][0].Subdivide();
            space.Root[3].Subdivide();
            Assert.AreEqual(new Rectangle(0, 0, 1, 1), space.Root[0][0][0].Rectangle);

            Assert.AreEqual(new Rectangle(4, 4, 2, 2), space.Root[3][0].Rectangle);
        }

        /// <summary>
        /// Resizing occurs when a space is requested that is too large. Verify that the proper behavior occurs.
        /// </summary>
        [SxeTest]
        public void TestResizing()
        {
            TextureSpace space1 = new TextureSpace(8);
            space1.AllowResize = true;
            //Request a size of 16
            TextureSpaceNode node = space1.GetFreeNode(16);
            node.Occupied = true;

            Assert.AreEqual(16, space1.Size);
            Assert.AreEqual(16, space1.Root.MaxSize);

            ////Now, check out another one... It should resize again
            node = space1.GetFreeNode(16);
            Assert.AreEqual(32, space1.Size);
            Assert.AreEqual(32, space1.Root.MaxSize);

        }

        /// <summary>
        /// Do a basic functionality test of the texture space
        /// </summary>
        [SxeTest]
        public void TestScenario1()
        {
            TextureSpace space = new TextureSpace(4);
            space.AllowResize = true;
            //Check out a larger node, to force a resize

            TextureSpaceNode node1 = space.GetFreeNode(4);
            node1.Occupied = true;
            Assert.AreEqual(new Rectangle(0, 0, 4, 4), node1.Rectangle);
            Assert.AreEqual(4, node1.MaxSize);

            //Get another large node, verify it gives correct rectangle
            TextureSpaceNode node2 = space.GetFreeNode(4);
            node2.Occupied = true;
            Assert.AreEqual(new Rectangle(4, 0, 4, 4), node2.Rectangle);
            Assert.AreEqual(8, node1.MaxSize);
            Assert.AreEqual(8, node2.MaxSize);

            //Get a few small nodes, verify in correct rectangle
            TextureSpaceNode node = space.GetFreeNode(2);
            node.Occupied = true;
            Assert.AreEqual(new Rectangle(0, 4, 2, 2), node.Rectangle);
            Assert.AreEqual(8, node1.MaxSize);
            Assert.AreEqual(8, node2.MaxSize);
            Assert.AreEqual(8, node.MaxSize);

            node = space.GetFreeNode(2);
            node.Occupied = true;
            Assert.AreEqual(new Rectangle(2, 4, 2, 2), node.Rectangle);

            node = space.GetFreeNode(2);
            node.Occupied = true;
            Assert.AreEqual(new Rectangle(0, 6, 2, 2), node.Rectangle);

            //Check out another large node
            node = space.GetFreeNode(4);
            node.Occupied = true;
            Assert.AreEqual(new Rectangle(4, 4, 4, 4), node.Rectangle);
            Assert.AreEqual(8, node1.MaxSize);
            Assert.AreEqual(8, node2.MaxSize);
            Assert.AreEqual(8, node.MaxSize);

            //Check out one more large node, to force another subdivision
            node = space.GetFreeNode(4);
            node.Occupied = true;
            Assert.AreEqual(new Rectangle(8, 0, 4, 4), node.Rectangle);
            Assert.AreEqual(16, node1.MaxSize);
            Assert.AreEqual(16, node2.MaxSize);
            Assert.AreEqual(16, node.MaxSize);
        }
        
        /// <summary>
        /// Verify that the texture space doesn't give up extra nodes!
        /// </summary>
        [SxeTest]
        public void TestScenario2()
        {
            TextureSpace space = new TextureSpace(16);

            int counter = 0;
            TextureSpaceNode node;
            do
            {
                node = space.GetFreeNode(8);
                if (node != null)
                {
                    counter++;
                    node.Occupied = true;
                }
            } while (node != null);

            Assert.AreEqual(4, counter);
        }
    }
}
