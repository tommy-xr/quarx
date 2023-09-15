using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

using Sxe.Library.Bsp;

namespace Sxe.Content.Bsp
{

    [ContentProcessor(DisplayName = "BSP Model Processor")]
    public class BspModelProcessor : ModelProcessor
    {

        Dictionary<string, int> lightmaps;

        public override ModelContent Process(NodeContent input, ContentProcessorContext context)
        {
            lightmaps = new Dictionary<string, int>();

            BspNodeContent bspNodeContent = input as BspNodeContent;

            ModelContent model = base.Process(input, context);

            AddLightmapData(input);

            model.Tag = new BspTagData(bspNodeContent.Bsp.LightMaps.ToArray(), lightmaps);

            return model;

        }

        void AddLightmapData(NodeContent node)
        {
            MeshContent mesh = node as MeshContent;
            if (mesh != null)
            {
                //Add all the lightmaps from the geometry contents
                //foreach (GeometryContent geometry in mesh.Geometry)
                //{
                //    lightmaps.Add(geometry.Name, (int)geometry.OpaqueData["lightmap"]);
                //}
                lightmaps.Add(mesh.Name, (int)mesh.OpaqueData["lightmap"]);
            }

            foreach (NodeContent child in node.Children)
                AddLightmapData(child);
        }


        protected override MaterialContent ConvertMaterial(MaterialContent material, ContentProcessorContext context)
        {
            //BitmapContent b

            //Convert to use LevelEffect
            BasicMaterialContent basic = material as BasicMaterialContent;

            EffectMaterialContent effect = new EffectMaterialContent();

            string effectPath = "LevelEffect.fx";

            effect.Effect = new ExternalReference<EffectContent>(effectPath);

            if (basic.Texture != null)
                effect.Textures.Add("g_baseTexture", basic.Texture);
            else
                effectPath = "";

            return base.ConvertMaterial(effect, context);
        }
    }
}