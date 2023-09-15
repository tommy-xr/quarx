using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

// TODO: replace these with the processor input and output types.
using TInput = System.String;
using TOutput = System.String;

using Sxe.Library.Materials;
using Sxe.Library.Utilities;

namespace Sxe.Content
{

    [ContentProcessor(DisplayName="Material Library Processor")]
    public class MaterialLibraryProcessor : ContentProcessor<MaterialCollection, MaterialManagerContent>
    {
        public override MaterialManagerContent Process(MaterialCollection input, ContentProcessorContext context)
        {
            MaterialManagerContent materialManager = new MaterialManagerContent(input, context);

            return materialManager;
        }
    }
}