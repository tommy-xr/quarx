using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using Sxe.Library.Materials;

namespace Sxe.Content
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentTypeWriter]
    public class MaterialWriter : ContentTypeWriter<MaterialManagerContent>
    {
        protected override void Write(ContentWriter output, MaterialManagerContent value)
        {
            value.Write(output);

            // TODO: write the specified value to the output ContentWriter.
            //throw new NotImplementedException();
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(MaterialManager).ToString();
            //return base.GetRuntimeType(targetPlatform);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            // TODO: change this to the name of your ContentTypeReader
            // class which will be used to load this data.
            return typeof(MaterialReader).AssemblyQualifiedName;
        }
    }
}
