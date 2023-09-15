using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.Entity
{
    public class EntityModule : BaseEntity
    {
        Entity parent;
        public Entity Parent
        {
            get { return parent; }
        }

        public EntityModule(Entity inParent, IServiceProvider services)
            : base(services)
        {
            parent = inParent;
        }
    }
}
