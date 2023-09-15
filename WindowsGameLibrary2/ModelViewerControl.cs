#region File Description
//-----------------------------------------------------------------------------
// ModelviewersControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

using Sxe.Design;
using Sxe.Engine.UI;

using Microsoft.Xna.Framework.Input;

namespace AnarchyEditor
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, and displays
    /// a spinning 3D model. The main form class is responsible for loading
    /// the model: this control just displays it.
    /// </summary>
    class ModelViewerControl : GraphicsDeviceControl
    {

        //Vector3 position;
        //Vector3 upVector;
        bool isLooking = false;
        Vector2 lastLook;

        AnarchyLevelViewer [] viewers;

        AnarchyLevelViewer viewerFocus;

 


        // Cache information about the model size and position.
        //Matrix[] boneTransforms;
        //Vector3 modelCenter;
        //float modelRadius;


        // Timer controls the rotation speed.
        Stopwatch timer;

        SpriteBatch spriteBatch;

        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            // Start the animation timer.
            timer = Stopwatch.StartNew();           

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };

        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {

            base.OnInvalidated(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            viewers = new AnarchyLevelViewer[4];

            viewers[0] = new AnarchyLevelViewer3D();
         

            viewers[1] = new AnarchyLevelViewerOrthographic(OrthographicAxis.XY);
            viewers[2] = new AnarchyLevelViewerOrthographic(OrthographicAxis.XZ);
            viewers[3] = new AnarchyLevelViewerOrthographic(OrthographicAxis.YZ);


            Point size = new Point(this.Size.Width / 2, this.Size.Height / 2);   
            viewers[0].Position = new Point(this.Size.Width / 2, 0);
            viewers[1].Position = new Point(0, 0);
            viewers[2].Position = new Point(0, this.Size.Height / 2);
            viewers[3].Position = new Point(this.Size.Width / 2, this.Size.Height / 2);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentManager content = new ContentManager(this.Services);

            for (int i = 0; i < viewers.Length; i++)
            {
                viewers[i].Size = size;
                viewers[i].LoadContent(GraphicsDevice, content);
            }

            
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            //If we have a focused control...


            base.OnMouseMove(e);
        }
        //protected override 

        //protected override void PaintUsingSystemDrawing(System.Drawing.Graphics graphics, string text)
        //{
        //    //graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.White, 1.0f),
        //    //    new System.Drawing.Point(1, 1), new System.Drawing.Point(100, 100));

        //    base.PaintUsingSystemDrawing(graphics, text);
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
           

            base.OnPaint(e);

            //Vector3 result = Vector3.Up;


            //e.Graphics.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.White,
            //   1.0f), position.X-2, position.Y-2, 4, 4);

            // e.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.White, 5.0f),
            //    new System.Drawing.Point((int)position.X, (int)position.Y), 
            //    new System.Drawing.Point((int)(upVector.X), (int)(upVector.Y)));
        }

        protected void Update()
        {
            for (int i = 0; i < viewers.Length; i++)
                viewers[i].Update();

            //Check for input shizz
            if (viewerFocus != null)
            {
                EditorInputEventArgs inputEvent = new EditorInputEventArgs();

                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
                {
                    inputEvent.EditorInputType = EditorInputType.SlideUp;
                    viewerFocus.HandleInput(inputEvent);
                }

                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.E))
                {
                    inputEvent.EditorInputType = EditorInputType.SlideDown;
                    viewerFocus.HandleInput(inputEvent);
                }

                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
                {
                    inputEvent.EditorInputType = EditorInputType.ZoomIn;
                    viewerFocus.HandleInput(inputEvent);
                }

                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
                {
                    inputEvent.EditorInputType = EditorInputType.ZoomOut;
                    viewerFocus.HandleInput(inputEvent);
                }

                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
                {
                    inputEvent.EditorInputType = EditorInputType.SlideLeft;
                    viewerFocus.HandleInput(inputEvent);
                }

                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
                {
                    inputEvent.EditorInputType = EditorInputType.SlideRight;
                    viewerFocus.HandleInput(inputEvent);
                }

            }

        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            Editor.Update();

            // Clear to the default control background color.
            Color backColor = new Color(BackColor.R, BackColor.G, BackColor.B);

            backColor = Color.CornflowerBlue;

            GraphicsDevice.Clear(backColor);

            Update();

            if (viewers != null)
            {
                for (int i = 0; i < viewers.Length; i++)
                {
                    //viewers[i].Update();
                    viewers[i].PreDraw();
                }

                spriteBatch.Begin();
                for (int i = 0; i < viewers.Length; i++)
                {
                    bool focused = false;
                    if (viewerFocus == viewers[i])
                        focused = true;

                    viewers[i].Draw(spriteBatch, focused);
                }

                //viewers[0].Draw(spriteBatch, false);
                spriteBatch.End();
                
            }

            //spriteBatch.Begin();
            //spriteBatch.Draw(viewers[0].Texture, new Rectangle(0, 0, this.Size.Width, this.Size.Height), Color.White);
            //spriteBatch.End();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Dude check which viewer is underneath
                for (int i = 0; i < viewers.Length; i++)
                {
                    if(IsPointInside(viewers[i], new Point(e.X, e.Y)))
                        viewerFocus = viewers[i];
                }
                
            }

            for (int i = 0; i < viewers.Length; i++)
            {
                if (IsPointInside(viewers[i], new Point(e.X, e.Y)))
                {
                    Sxe.Engine.Input.MouseEventArgs mouseEvent = new Sxe.Engine.Input.MouseEventArgs();
                    mouseEvent.MouseEventType = Sxe.Engine.Input.MouseEventType.Move;
                    mouseEvent.Position = new Point(e.X - viewers[i].Position.X, e.Y - viewers[i].Position.Y);
                    viewers[i].HandleInput(mouseEvent);


                }
            }

            //if (e.Button == MouseButtons.Left && isLooking)
            //{
            //    isLooking = false;
            //}
            //else if (e.Button == MouseButtons.Left && !isLooking)
            //{
            //    isLooking = true;
            //    lastLook = new Vector2(e.X, e.Y);
            //}
            base.OnMouseClick(e);
        }

        bool IsPointInside(AnarchyLevelViewer viewer, Point p)
        {
            if (p.X >= viewer.Position.X &&
                p.Y >= viewer.Position.Y &&
                p.X <= viewer.Position.X + viewer.Size.X &&
                p.Y <= viewer.Position.Y + viewer.Size.Y)
            {
                return true;
            }

            return false;
        }
    }
}
