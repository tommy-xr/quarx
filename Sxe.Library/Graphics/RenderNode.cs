using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;


using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using Sxe.Library.Graphics.Materials;

namespace Sxe.Library.Graphics
{
    /// <summary>
    /// Manages a specific node of the rendering pipeline
    /// Handles rendering groups of materials
    /// </summary>
    public class RenderNode
    {
        /// <summary>
        /// Store some info for materials,
        /// beyond just the list of Materials
        /// </summary>
        class MaterialInfo
        {
            Effect materialEffect;
            MaterialCollection materialList = new MaterialCollection();
            Type materialType;

            public Effect MaterialEffect
            {
                get { return materialEffect; }
            }

            public MaterialCollection Materials { get { return materialList; } }

            public MaterialInfo(Type inMaterialType)
            {
                //TODO: Figure this out
                //dummyMaterial = new T();
                materialType = inMaterialType;
            }

            public void Load(ContentManager content)
            {
                Type t = materialType;
                
                //Check all the attributes on this type, and find an effect
                foreach (Attribute attr in t.GetCustomAttributes(typeof(Materials.MaterialEffectAttribute), true))
                {
                    Materials.MaterialEffectAttribute effectAttribute = attr as Materials.MaterialEffectAttribute;
                    string defaultEffectName = effectAttribute.DefaultEffectName;

                    if (defaultEffectName == "BasicEffect")
                    {
                        IGraphicsDeviceService service = content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
                        materialEffect = new BasicEffect(service.GraphicsDevice, null);
                    }
                    else
                    {
                        materialEffect = content.Load<Effect>(defaultEffectName);
                    }

                    return;
                }

                //If we got here, then we found the type ok, but it didn't have a material effect attribute
                //So its an error, because we don't know what effect to use for it
                throw new Exception("Error: No MaterialEffectAttribute provided for type " + materialType.ToString() + ", could not load effect.");
            }

            public void Draw(GlobalParameters parameters)
            {
                Draw(this.materialEffect, parameters, false);
            }

            public void DrawGeometryOnly(Effect effect, GlobalParameters parameters)
            {
                Draw(effect, parameters, true);
            }

            protected void Draw(Effect effect, GlobalParameters parameters, bool useConservativeParams)
            {
                if (Materials.Count == 0)
                    return;

                Materials[0].SetGlobalParameters(parameters);

                effect.Begin();

                for (int i = 0; i < effect.CurrentTechnique.Passes.Count; i++)
                {
                    effect.CurrentTechnique.Passes[i].Begin();
                    for (int m = 0; m < Materials.Count; m++)
                    {
                        Materials[m].DrawGeometry(effect, useConservativeParams);
                    }
                    effect.CurrentTechnique.Passes[i].End();

                }
                effect.End();
            }

        }

        ContentManager content;
        Dictionary<Type, MaterialInfo> typeToInfo = new Dictionary<Type, MaterialInfo>();
        List<MaterialInfo> infoList = new List<MaterialInfo>();

        public RenderNode(ContentManager inContent)
        {
            content = inContent;
        }

        public void Add(BaseMaterial material)
        {
            //Check to see if this type already exists
            Type type = material.GetType();
            MaterialInfo info = null;
            if (!typeToInfo.ContainsKey(type))
            {
                //Need to create a new info stuff
                info = new MaterialInfo(type);
                info.Load(content);
                typeToInfo.Add(type, info);
                infoList.Add(info);
            }
            else
            {

                info = typeToInfo[type];
            }

            material.CacheEffectParameters(info.MaterialEffect);

            info.Materials.Add(material);
        }

        public bool Remove(BaseMaterial material)
        {
            Type type = material.GetType();
            if (typeToInfo.ContainsKey(type))
            {
                MaterialInfo info = typeToInfo[type];
                return info.Materials.Remove(material);
            }
            else
            {
                return false;
            }
            

        }

        public void DrawGeometryOnly(Effect effect, GlobalParameters parameters)
        {
            for (int i = 0; i < this.infoList.Count; i++)
            {
                MaterialInfo info = this.infoList[i];
                info.DrawGeometryOnly(effect, parameters);
            }
        }

        public void Draw(GlobalParameters parameters)
        {
            for (int i = 0; i < this.infoList.Count; i++)
            {
                MaterialInfo info = this.infoList[i];
                info.Draw(parameters);
            }
        }

    }
}
