using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsContentLoading.Level
{
    /// <summary>
    /// Wrapper for a model
    /// Lets us add some additional details, like transform and stuff
    /// </summary>
    public class AnarchyModel
    {
        Model model;
        public Model Moddel
        {
            get { return model; }
        }

        public AnarchyModel(Model inModel)
        {
            model = inModel;
        
            
        }



        public class AnarchyMesh
        {
            ModelMesh mesh;
            public ModelMesh Mesh
            {
                get { return mesh; }
            }



        }

        public class AnarchyMeshCollection : Collection<AnarchyMesh>
        {
        }
    }
}
