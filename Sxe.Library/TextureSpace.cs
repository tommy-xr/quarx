using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Library
{
    /// <summary>
    /// Item that maintains the free space in a texture object
    /// </summary>
    public class TextureSpace
    {
        int currentSize;
        TextureSpaceNode root;
        bool allowResize = false;

        public int Size
        {
            get { return currentSize; } 
        }

        public bool AllowResize
        {
            get { return allowResize; }
            set { allowResize = value; }
        }

        public TextureSpaceNode Root
        {
            get { return root; }
        }

        public TextureSpace(int size)
        {
            currentSize = size;
            root = new TextureSpaceNode(size, null, Vector2.Zero, Vector2.One);
        }

        public TextureSpaceNode GetFreeNode(int size)
        {

            //We can't give a node of size 0... so... screw you basically
            if (size == 0)
                return null;

            TextureSpaceNode node = root.GetFreeNode(size);

            if (node != null)
            {
                return node;
            }
            //If the node is null, then we didn't have enough space. That means we should resize
            //so that we can accomodate bigger texture
            else if(allowResize)
            {
                TextureSpaceNode oldRoot = root;
                currentSize *= 2; //double the size, we only allow textures of powers of two

                root = new TextureSpaceNode(currentSize, null, Vector2.Zero, Vector2.One);
                root.Subdivide();
                oldRoot.TextureSize = new Vector2(0.5f); //resize the root, because its small now
                root[0] = oldRoot; //make the old root a child of the new root
                oldRoot.Parent = root;

                return GetFreeNode(size);
            }

            return null;
 
            
            
        }


    }

   


    public class TextureSpaceNode
    {
        int size;
        TextureSpaceNode parent;
        TextureSpaceNode[] children;
        bool occupied = false;

        Vector2 positionVector;
        Vector2 sizeVector;

        public Vector2 TexturePosition
        {
            get 
            {
                if (parent == null)
                    return positionVector;
                else
                    return parent.TexturePosition + this.positionVector * parent.TextureSize; 
            }
        }

        public bool HasChildren
        {
            get { return children != null; }
        }

        public TextureSpaceNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Vector2 TextureSize
        {
            get 
            {
                if (parent == null)
                    return sizeVector;
                else
                    return parent.TextureSize * this.sizeVector; 
            }
            set
            {
                sizeVector = value;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                Vector2 pixelPosition = TexturePosition * MaxSize;
                Vector2 pixelSize = TextureSize * MaxSize;

                return new Rectangle((int)pixelPosition.X, (int)pixelPosition.Y, (int)pixelSize.X, (int)pixelSize.Y);
            }
        }

        public int MaxSize
        {
            get
            {
                if (parent == null)
                    return this.size;
                else
                    return parent.MaxSize;
            }
        }

        public TextureSpaceNode this[int i]
        {
            get { return children[i]; }
            set { children[i] = value; }
        }

        public int Size
        {
            get { return size; }
        }

        public bool Occupied
        {

            
            get
            {
                return occupied;
            }
            set
            {
                occupied = value; 
            }
        }

        public bool ChildrenOccupied
        {
            get
            {
                if (children == null)
                    return false;
                else
                    return children[0].Occupied || children[1].Occupied || children[2].Occupied || children[3].Occupied
                        || children[0].ChildrenOccupied || children[1].ChildrenOccupied || children[2].ChildrenOccupied || children[3].ChildrenOccupied;
            }
        }

        public TextureSpaceNode(int nodeSize, TextureSpaceNode inParent, Vector2 pos, Vector2 sizeVec)
        {
            positionVector = pos;
            sizeVector = sizeVec;
            size = nodeSize;
            parent = inParent;
        }

        /// <summary>
        /// Subdivides the node by creating child nodes
        /// </summary>
        public void Subdivide()
        {
            if (children != null)
                throw new Exception("Tried to subdivide with children already existing!");

            children = new TextureSpaceNode[4]; //4 children for quad tree like structure
            Vector2 childSize = new Vector2(0.5f);
            children[0] = new TextureSpaceNode(size / 2, this, Vector2.Zero, childSize);
            children[1] = new TextureSpaceNode(size / 2, this, new Vector2(0.5f, 0.0f), childSize);
            children[2] = new TextureSpaceNode(size / 2, this, new Vector2(0.0f, 0.5f), childSize);
            children[3] = new TextureSpaceNode(size / 2, this, new Vector2(0.5f, 0.5f), childSize);
        }

        /// <summary>
        /// Returns a free node of the request size. If there are no free nodes, returns null.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public TextureSpaceNode GetFreeNode(int nodeSize)
        {
            //If the requested size is bigger than we are, there is nothing we can do!
            //Also, if we are occupied, there is nothing we can do
            if (nodeSize > this.Size || Occupied)
            {
                return null;
            }

            //Otherwise, if the size is equal to us, and we are not occupied, return us!
            if (nodeSize == this.Size && !(Occupied || ChildrenOccupied))
            {
                return this;
            }
            else 
            {
                //If we don't have children... subdivide now
                if(children == null)
                Subdivide();

                //Finally, loop through and see if this fits any of our children
                for (int i = 0; i < 4; i++)
                {
                    if (children[i].GetFreeNode(nodeSize) != null)
                        return children[i].GetFreeNode(nodeSize);
                }


            }

            return null;

        }
    }
}
