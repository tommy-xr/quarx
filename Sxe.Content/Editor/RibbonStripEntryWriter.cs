using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using Sxe.Library.Editor;

namespace Sxe.Content.Editor
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentTypeWriter]
    public class RibbonStripEntryWriter : SxeContentWriter<RibbonStripEntry>
    {
        protected override void Write(ContentWriter output, RibbonStripEntry value)
        {
            output.Write(value.Command);
            output.Write(value.DefaultImagePath);
            output.Write(value.OverImagePath);
            output.WriteRawObject<Point>(value.Location);
            output.WriteRawObject<Point>(value.Size);
            output.Write(value.ToolTip);
        }
    }
}
