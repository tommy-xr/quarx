using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Library.Graphics.Materials
{
   
    public class BaseMaterial
    {

        GeometryChunkCollection geometryCollection = new GeometryChunkCollection();
        public GeometryChunkCollection GeometryCollection
        {
            get { return geometryCollection; }
        }

        public virtual void Initialize()
        {
        }

        public virtual void CacheEffectParameters(Effect effect)
        {
        }

        public void DrawGeometry(Effect effect, bool useConservativeParameters)
        {
            SetGeometryParameters(useConservativeParameters);

            effect.CommitChanges();

            for (int i = 0; i < geometryCollection.Count; i++)
            {
                geometryCollection[i].Draw(effect.GraphicsDevice);
            }
        }

        public virtual void SetGlobalParameters(GlobalParameters parameters)
        {
        }

        protected virtual void SetGeometryParameters(bool useConservativeParameters)
        {

        }


        
    }
}
