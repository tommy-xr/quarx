using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

using Sxe.Library.Materials;
using Sxe.Library.Utilities;

namespace Sxe.Content.Materials
{
    /// <summary>
    /// This is an importer that imports material files
    /// </summary>
    [ContentImporter(".mat", DefaultProcessor = "MaterialLibraryProcessor", DisplayName = "Material Library Importer")]
    public class MaterialLibraryImporter : ContentImporter<MaterialCollection>
    {
        public override MaterialCollection Import(string filename, ContentImporterContext context)
        {
            return XmlIO.Load<MaterialCollection>(filename, false);
        }
    }
}
