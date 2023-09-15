using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace SXE.Engine
{
    public interface ICamera
    {
        Vector3 Position { get; set;}
        Vector3 Forward { get; set; }
        Vector3 Up { get; set; }
        Vector3 Right { get; }
        Vector3 ViewOffset { get; set; }

        Matrix View { get; }
        Matrix Projection { get; }

        BoundingFrustum Frustrum { get; }
        BoundingFrustum ShortFrustrum { get; }

        float FOV { get; set; }
        float AspectRatio { get; set; }
        float NearPlane { get; set; }
        float FarPlane { get; set; }
        float ShortPlane { get; set; }

        void RotateHorizontally(float amt);
        void RotateVertically(float amt);


        void Update(GameTime time);

    }

    /// <summary>
    /// Encapsulates camera functionality
    /// Takes in various parameters, like position, forward vector, up,and view offset
    /// Outputs a view matrix, projection matrix, and some frustrums
    /// Make sure that position, forward, up, viewoffset, and aspectRatio are set before
    /// calling SetupMatrices()
    /// </summary>
    public class Camera : ICamera
    {
        Vector3 position;
        Vector3 forward;
        Vector3 up;
        Vector3 viewOffset;

        Matrix view;
        Matrix projection;
        Matrix shortProjection;

        BoundingFrustum frustrum;
        BoundingFrustum shortFrustrum;

        float fov = (float)MathHelper.PiOver4;
        float aspectRatio = 1.0f;

        float nearPlane = 1.0f;
        float shortPlane = 250.0f;
        float farPlane = 5000.0f;

        bool hasChanged = true; //flag to mark if camera data has changed 



        public Camera(Vector3 position, Vector3 forward)
        {
            this.position = position;
            this.forward = forward;
        }

        public Camera()
            : this(Vector3.Zero, Vector3.Forward)
        {
            this.up = Vector3.Up;
        }

        #region Public Get/Set Functions
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                hasChanged = true;
                position = value;
            }
        }

        public Vector3 Forward
        {
            get
            {
                return forward;
            }
            set
            {
                hasChanged = true;
                forward = value;
                forward.Normalize();
            }
        }

        public Vector3 Up
        {
            get
            {
                return up;
            }
            set
            {
                hasChanged = true;
                up = value;
            }
        }

        public Vector3 Right
        {
            get
            {
                return Vector3.Normalize(Vector3.Cross(Forward, Up));
            }
        }

        public Vector3 ViewOffset
        {
            get
            {
                return viewOffset;
            }
            set
            {
                hasChanged = true;
                viewOffset = value;
            }
        }

        public float FOV
        {
            get
            {
                return fov;
            }
            set
            {
                hasChanged = true;
                fov = value;
            }
        }
        public float AspectRatio
        {
            get
            {
                return aspectRatio;
            }
            set
            {
                hasChanged = true;
                aspectRatio = value;
            }
        }
        public float NearPlane
        {
            get
            {
                return nearPlane;
            }
            set
            {
                hasChanged = true;
                nearPlane = value;
            }
        }
        public float FarPlane
        {
            get
            {
                return farPlane;
            }
            set
            {
                hasChanged = true;
                farPlane = value;
            }
        }
        public float ShortPlane
        {
            get
            {
                return shortPlane;
            }
            set
            {
                hasChanged = true;
                shortPlane = value;
            }
        }
        #endregion

        public void RotateHorizontally(float amount)
        {
            //First, rotate the forward vector
            Forward = Vector3.Transform(forward, Matrix.CreateFromAxisAngle(Vector3.Up, amount));
            Forward.Normalize();

            //TODO: Fix to make more robust
            //This is not very robust, because if you are looking exactly straight up, it will create problems..
        }


        public void RotateVertically(float amount)
        {
            Vector3 cross = Vector3.Cross(forward, Vector3.Up);
            cross.Normalize();

            Matrix rotMatrix = Matrix.CreateFromAxisAngle(cross, amount);

            //Rotate the forward vector
            Forward = Vector3.Transform(forward, rotMatrix);
            Forward.Normalize();


            //TODO:
            //Fix this function, make it way better
        }

        public void Update(GameTime gameTime)
        {
            SetupMatrices();
        }


        /// <summary>
        /// Private function that creates the camera matrices based on our data
        /// </summary>
        void SetupMatrices()
        {
            view = Matrix.CreateLookAt(this.position + viewOffset, this.position + viewOffset + forward, this.up);
            projection = Matrix.CreatePerspectiveFieldOfView(fov, aspectRatio, nearPlane, farPlane);
            shortProjection = Matrix.CreatePerspectiveFieldOfView(fov, aspectRatio, nearPlane, shortPlane);

            if (hasChanged)
            {
                frustrum = new BoundingFrustum(view * projection);
                shortFrustrum = new BoundingFrustum(view * shortProjection);
                hasChanged = false;
            }
        }

        public Matrix View
        {
            get
            {
                return view;
            }
        }

        public Matrix Projection
        {
            get
            {
                return projection;
            }
        }

        public BoundingFrustum Frustrum
        {
            get
            {
                return frustrum;
            }
        }

        public BoundingFrustum ShortFrustrum
        {
            get
            {
                return shortFrustrum;
            }
        }



    }
}
