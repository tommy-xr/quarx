using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Library.Materials;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Sxe.Content
{
    /// <summary>
    /// Content-time class for holding data needed by the material manager
    /// </summary>
    public class MaterialManagerContent
    {
        /// <summary>
        /// Class for holding individual data for each material
        /// </summary>
        class MaterialContent
        {
            public Material info;
            public ExternalReference<Texture2DContent> diffuse;
            public ExternalReference<Texture2DContent> normal;
            public ExternalReference<Texture2DContent> specular;
            public ExternalReference<CompiledEffect> effect;
        }
       
        ExternalReference<Texture2DContent> defaultDiffuse;
        ExternalReference<Texture2DContent> defaultNormal;
        ExternalReference<Texture2DContent> defaultSpecular;
        ExternalReference<CompiledEffect> defaultEffect;

        MaterialContent[] materials;

        /// <summary>
        /// Creates a material manager content from a material collection
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        public MaterialManagerContent(MaterialCollection input, ContentProcessorContext context)
        {
        
            //Load all the defalt objects
            defaultDiffuse = context.BuildAsset<Texture2DContent, Texture2DContent>(
                new ExternalReference<Texture2DContent>(input.DefaultDiffuseName), null);
            defaultSpecular = context.BuildAsset<Texture2DContent, Texture2DContent>(
                new ExternalReference<Texture2DContent>(input.DefaultGlossName), null);
            defaultNormal = context.BuildAsset<Texture2DContent, Texture2DContent>(
                new ExternalReference<Texture2DContent>(input.DefaultNormalName), null);
            defaultEffect = context.BuildAsset<EffectContent, CompiledEffect>(
                new ExternalReference<EffectContent>(input.DefaultShaderName), "EffectProcessor");


            //TODO: Read rest of materials!
            materials = new MaterialContent[input.Materials.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = new MaterialContent();
                materials[i].info = input.Materials[i];
                materials[i].diffuse = context.BuildAsset<Texture2DContent, Texture2DContent>(
                new ExternalReference<Texture2DContent>(input.Materials[i].DiffuseName), null);
                materials[i].normal = context.BuildAsset<Texture2DContent, Texture2DContent>(
                new ExternalReference<Texture2DContent>(input.Materials[i].NormalName), null);
                materials[i].specular = context.BuildAsset<Texture2DContent, Texture2DContent>(
                new ExternalReference<Texture2DContent>(input.Materials[i].GlossName), null);
                materials[i].effect = context.BuildAsset<EffectContent, CompiledEffect>(
                new ExternalReference<EffectContent>(input.DefaultShaderName), "EffectProcessor");
            }


        }

        /// <summary>
        /// Writes the MaterialManagerContent to a .xnb
        /// </summary>
        /// <param name="writer"></param>
        public void Write(ContentWriter output)
        {
            output.WriteExternalReference<Texture2DContent>(defaultDiffuse);
            output.WriteExternalReference<Texture2DContent>(defaultNormal);
            output.WriteExternalReference<Texture2DContent>(defaultSpecular);
            output.WriteExternalReference<CompiledEffect>(defaultEffect);

            //Write all the materials to a file
            output.Write(materials.Length);
            for (int i = 0; i < materials.Length; i++)
            {
                output.WriteExternalReference<Texture2DContent>(materials[i].diffuse);
                output.WriteExternalReference<Texture2DContent>(materials[i].normal);
                output.WriteExternalReference<Texture2DContent>(materials[i].specular);
                output.WriteExternalReference<CompiledEffect>(materials[i].effect);
            }
        }

    }


}
