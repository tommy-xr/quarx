using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using Sxe.Library.Bsp;

namespace Sxe.Content.Bsp
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentTypeWriter]
    public class BspTagTypeWriter : ContentTypeWriter<BspTagData>
    {
        protected override void Write(ContentWriter output, BspTagData value)
        {
            output.WriteObject(value.LightDictionary);

            output.Write(value.LightMaps.Length);
            for (int i = 0; i < value.LightMaps.Length; i++)
            {
                output.WriteObject(value.LightMaps[i].map);
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(BspTagReader).AssemblyQualifiedName;
        }
    }
}
