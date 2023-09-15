using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;


namespace WinFormsContentLoading
{
    public class ModelInfo
    {
        string contentPath;
        public string ContentPath
        {
            get { return contentPath; }
            set { contentPath = value; }
        }

        Vector3 translation;
        public Vector3 Translation
        {
            get { return translation; }
            set { translation = value; }
        }

        Vector3 rotation;
        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        Vector3 scale = Vector3.One;
        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        //Matrix transform;
        //public Matrix Transform
        //{
        //    get { return transform; }
        //    set { transform = value; }
        //}
    }
}
