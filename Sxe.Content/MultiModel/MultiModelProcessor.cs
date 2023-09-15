using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

using Sxe.Library.MultiModel;

namespace Sxe.Content.MultiModel
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "MultiModelProcessor")]
    public class MultiModelProcessor : ContentProcessor<MultiModelCollection, MultiModelContent>
    {
        public override MultiModelContent Process(MultiModelCollection input, ContentProcessorContext context)
        {
            Form1 dummyForm = new Form1();

            PresentationParameters pp = new PresentationParameters();           
            GraphicsDevice device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.Hardware,
                dummyForm.Handle, pp);


            
            return new MultiModelContent(input, context, device);
        }
    }
}