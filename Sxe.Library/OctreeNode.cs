using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Sxe.Library
{
    /// <summary>
    /// A node in an octree
    /// </summary>
    class OctreeNode
    {
        const int DefaultLimit = 5; 

        #region Fields
        OctreeNode parent;
        OctreeNode[] children;
        BoundingBox box;
        int objectLimit;
        int depthLimit;
        #endregion

        #region Properties
        public int Depth
        {
            get
            {
                if (parent == null)
                    return 0;
                else
                    return parent.Depth + 1;
            }
        }
        public Vector3 Center
        {
            get { return (box.Max + box.Min) / 2.0f; }
        }
        #endregion

        #region Constructors
        public OctreeNode(int maxNodes, int maxDepth, Vector3 min, Vector3 max, OctreeNode inParent)
        {
            objectLimit = maxNodes;
            depthLimit = maxDepth;
            box = new BoundingBox(min, max);
            parent = inParent;
        }
        #endregion

        /// <summary>
        /// This is called when nodelimit is exceeded and we need to split
        /// </summary>
        void Subdivide()
        {
            if (children == null)
            {
                Vector3 min = box.Min;
                Vector3 max = box.Max;
                Vector3 center = Center;
                children = new OctreeNode[8];
                children[0] = new OctreeNode(this.objectLimit, this.depthLimit,
                    min, center, this);
                children[1] = new OctreeNode(this.objectLimit, this.depthLimit,
                    new Vector3(min.X, min.Y, center.Z),
                    new Vector3(center.X, center.Y, max.Z), this);
                children[2] = new OctreeNode(this.objectLimit, this.depthLimit,
                    new Vector3(min.X, center.Y, min.Z),
                    new Vector3(center.X, max.Y, center.Z), this);
                children[3] = new OctreeNode(this.objectLimit, this.depthLimit,
                    new Vector3(min.X, center.Y, center.Z),
                    new Vector3(center.X, max.Y, max.Z), this);
                children[4] = new OctreeNode(this.objectLimit, this.depthLimit,
                    new Vector3(center.X, min.Y, min.Z),
                    new Vector3(max.X, center.Y, center.Z), this);
                children[5] = new OctreeNode(this.objectLimit, this.depthLimit,
                    new Vector3(center.X, min.Y, center.Z),
                    new Vector3(max.X, center.Y, max.Z), this);
                children[6] = new OctreeNode(this.objectLimit, this.depthLimit,
                    new Vector3(center.X, center.Y, min.Z),
                    new Vector3(max.X, max.Y, center.Z), this);
                children[7] = new OctreeNode(this.objectLimit, this.depthLimit,
                    new Vector3(center.X, center.Y, center.Z),
                    new Vector3(max.X, max.Y, max.Z), this);
            }
        }

    }
}
