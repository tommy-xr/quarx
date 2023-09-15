using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.SceneGraph
{
    public class SceneGraphNodeCollection : Collection<SceneGraphNode>
    {
        SceneGraphNode owner;

        public SceneGraphNodeCollection(SceneGraphNode inOwner)
        {
            owner = inOwner;
        }

        protected override void InsertItem(int index, SceneGraphNode item)
        {
            if (item.Parent != null)
            {
                if (item.Parent.Children.Contains(item))
                    item.Parent.Children.Remove(item);
            }

            item.Parent = owner;

            base.InsertItem(index, item);
        }
    }
}
