using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Library.Materials
{
    /// <summary>
    /// This class
    /// </summary>
    public class MaterialCollection
    {
        public string DefaultDiffuseName;
        public string DefaultNormalName;
        public string DefaultGlossName;
        public string DefaultShaderName;

        public Material[] Materials;

        //Better design: Make these private and accessible only through read properties
        //And make an interior XML Save function
    }

    /// <summary>
    /// This class defines what is stored in a material
    /// </summary>
    public class Material
    {
        //Editor fields
        public int categoryIndex;

        //Other fields
        string materialName;
        string diffuseName;
        string normalName;
        string glossName;
        string shaderName;

        bool useSkyMap;
        bool transparent;

        string footstepSound;
        string ricochetSound;

        float elasticity;
        float staticFriction;
        float kineticFriction;

       

        //Bindable properties
        public string DiffuseName
        {
            get { return diffuseName; }
            set { diffuseName = value; }
        }
        public string NormalName
        {
            get { return normalName; }
            set { normalName = value; }
        }
        public string GlossName
        {
            get { return glossName; }
            set { glossName = value; }
        }
        public string ShaderName
        {
            get { return shaderName; }
            set { shaderName = value; }
        }
        
        public bool UseSkyMap
        {
            get { return useSkyMap; }
            set { useSkyMap = value; }
        }

        public bool Transparent
        {
            get { return transparent; }
            set { transparent = value; }
        }

        public string MaterialName
        {
            get { return materialName; }
            set { materialName = value; }
        }

        public float Elasticity
        {
            get { return elasticity; }
            set { elasticity = value; }
        }

        public float StaticFriction
        {
            get { return staticFriction; }
            set { staticFriction = value; }
        }

        public float KineticFriction
        {
            get { return kineticFriction; }
            set { kineticFriction = value; }
        }

        public string FootstepSound
        {
            get { return footstepSound; }
            set { footstepSound = value; }
        }

        public string RicochetSound
        {
            get { return ricochetSound; }
            set { ricochetSound = value; }


        }
    }


}
