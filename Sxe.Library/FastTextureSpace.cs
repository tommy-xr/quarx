using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace Sxe.Library
{


    public class FastTextureSpace
    {
        public class FastTextureSpaceNode
        {
            FastTextureSpace parent;

            Rectangle rectangle;
            Vector2 position;
            Vector2 size;
            int parentSize;
            int index;

            List<FastTextureSpaceNode> children = new List<FastTextureSpaceNode>();

            public List<FastTextureSpaceNode> Children
            {
                get { return children; }
            }


            public Vector2 TexturePosition
            {
                get { return position; }
            }
            public Vector2 TextureSize
            {
                get { return size; }
            }
            public Rectangle Rectangle
            {
                get { return rectangle; }
            }
            public int Size
            {
                get { return rectangle.Width; }
            }

            public bool Occupied
            {
                get { return IsOccupied(); }
            }

            public FastTextureSpaceNode(FastTextureSpace inParent, int inIndex, Vector2 inPosition, Vector2 inSize)
            {
                position = inPosition;
                size = inSize;
                parent = inParent;
                parentSize = parent.size;
                rectangle = new Rectangle((int)(inPosition.X * parentSize),
                    (int)(inPosition.Y * parentSize),
                    (int)(inSize.X * parentSize),
                    (int)(inSize.Y * parentSize));
                index = inIndex;
            }

            public bool IsOccupied()
            {
                for (int x = rectangle.X; x < rectangle.X + rectangle.Width; x++)
                {
                    for (int y = rectangle.Y; y < rectangle.Y + rectangle.Height; y++)
                    {
                        if (parent.occupiedData[x, y] != -1)
                            return true;
                    }
                }
                return false;
            }

            public void Occupy()
            {
                if (IsOccupied())
                    throw new Exception("Occupied..");

                for (int x = rectangle.X; x < rectangle.X + rectangle.Width; x++)
                {
                    for (int y = rectangle.Y; y < rectangle.Y + rectangle.Height; y++)
                    {
                        parent.occupiedData[x, y] = index;
                    }
                }

            }

            public void UnOccopy()
            {
                for (int x = rectangle.X; x < rectangle.X + rectangle.Width; x++)
                {
                    for (int y = rectangle.Y; y < rectangle.Y + rectangle.Height; y++)
                    {
                        if (parent.occupiedData[x, y] != index && parent.occupiedData[x,y] != -1)
                            throw new Exception("Error - index doesn't match... this shouldn't happen");
                        else
                            parent.occupiedData[x, y] = -1;

                    }
                }

                AddSelfToList();
                //if (!list.Contains(this))
                //{
                //    list.Enqueue(this);
                //}
            }

            void AddSelfToList()
            {
                Queue<FastTextureSpaceNode> list = parent.scaleToNodes[this.Size];
                list.Enqueue(this);

                foreach (FastTextureSpaceNode child in children)
                    child.AddSelfToList();
            }

        }

        int size;
        int nodes = 0;
        int[,] occupiedData;
        Dictionary<int, Queue<FastTextureSpaceNode>> scaleToNodes;

        public int Size { get { return size; } }

        public FastTextureSpace(int textureSize, int maxNodeSize, int minNodeSize)
        {
            //TODO:
            //Verify each is a power of two
            size = textureSize;
            occupiedData = new int[size, size];

            //Initialize occupied data
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    occupiedData[x, y] = -1;

            scaleToNodes = new Dictionary<int,Queue<FastTextureSpaceNode>>();
            

            //Create initial nodes
            int tempScale = textureSize / maxNodeSize;
            float relativeSize = (float)maxNodeSize / (float)size;
            for (int x = 0; x < tempScale; x++)
            {
                for (int y = 0; y < tempScale; y++)
                {
                    CreateNode(new Vector2(relativeSize * x, relativeSize * y), relativeSize, minNodeSize);   
                }
            }

        }

        public FastTextureSpaceNode CreateNode(Vector2 position, float relativeSize, int minSize)
        {
            int actualSize = (int)(size * relativeSize);
            if (actualSize < minSize)
                return null;

            //Do we have a list for this size yet?
            if (!scaleToNodes.ContainsKey(actualSize))
                scaleToNodes.Add(actualSize, new Queue<FastTextureSpaceNode>());

            FastTextureSpaceNode fastNode = new FastTextureSpaceNode(this, nodes, position, new Vector2(relativeSize));
            nodes++;
            scaleToNodes[actualSize].Enqueue(fastNode);

            //Create 4 sub nodes with half the size
            float newSize = relativeSize / 2;
            Vector2 newSizeVec = new Vector2(newSize);
            FastTextureSpaceNode node1 = CreateNode(position, newSize, minSize);
            FastTextureSpaceNode node2 = CreateNode(position + new Vector2(newSize, 0f), newSize, minSize);
            FastTextureSpaceNode node3 = CreateNode(position + new Vector2(0f, newSize), newSize, minSize);
            FastTextureSpaceNode node4 = CreateNode(position + newSizeVec, newSize, minSize);

            if (node1 != null)
                fastNode.Children.Add(node1);
            if (node2 != null)
                fastNode.Children.Add(node2);
            if(node3 != null)
                fastNode.Children.Add(node3);
            if (node4 != null)
                fastNode.Children.Add(node4);

            return fastNode;

        }

        public FastTextureSpaceNode GetFreeNode(int size)
        {
            if (!scaleToNodes.ContainsKey(size))
                return null;

            Queue<FastTextureSpaceNode> nodes = scaleToNodes[size];

            int count = nodes.Count;
            for (int i = 0; i < count; i++)
            {
                //if (!nodes[i].IsOccupied())
                //    return nodes[i];
                FastTextureSpaceNode node = nodes.Dequeue();
                if (!node.IsOccupied())
                {
                    

                    return node;
                }


            }
            return null;
        }

    }
}
