using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace Sxe.Content.MultiModel
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// 
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentImporter(".smm", DisplayName = "MultiModel Importer", DefaultProcessor = "MultiModel Processor")]
    public class MultiModelImporter : ContentImporter<MultiModelCollection>
    {
        public override MultiModelCollection Import(string filename, ContentImporterContext context)
        {

            MultiModelCollection content = new MultiModelCollection();
            content.FileName = filename;

            content.ModelInfo = new MultiModelInfo[1];
            content.ModelInfo[0] = new MultiModelInfo();
            content.ModelInfo[0].Name = "test_trigger_multiple.bsp";
            content.ModelInfo[0].Transform = Matrix.Identity;
            content.ModelInfo[0].Importer = "BspModelImporter";
            content.ModelInfo[0].Processor = "BspModelProcessor";

            //content.ModelInfo[1] = new MultiModelInfo();
            //content.ModelInfo[1].Name = "health_small.fbx";
            //content.ModelInfo[1].Transform = Matrix.Identity;
            //content.ModelInfo[1].Importer = "FbxImporter";

            return content;
        }
    }
}
