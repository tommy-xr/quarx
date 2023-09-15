using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.Test.Framework;
using Sxe.Engine.UI;
using Sxe.Engine.Graphics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Quarx.Tests
{
    //public class TestDraw3D : TestScreen
    //{
    //    BasicEffect effect;
    //    Model blockModel;

    //    RenderTarget2D target;

    //    public Vector3 CameraPosition
    //    {
    //        get { return Camera.Position; }
    //    }
    //    public Vector3 CameraForward
    //    {
    //        get { return Camera.Forward; }
    //    }

    //    public override void TestInitialize(IGameScreenService service, ContentManager content)
    //    {
    //        effect = new BasicEffect(service.GraphicsDevice, null);
    //        blockModel = content.Load<Model>("block_model");
    //        base.TestInitialize(service, content);
    //        Camera.Position = new Vector3(0f, 10f, 0f);
    //        Camera.Forward = Vector3.Forward;
    //        Camera.AspectRatio = 0.5f;

    //        target = new RenderTarget2D(service.GraphicsDevice, 256, 512, 1, SurfaceFormat.Color);

    //        PropertyGrid pg = new PropertyGrid(this.Panel, new Point(10, 10), new Point(100, 800));
    //        pg.SelectedObject = this;
    //    }

    //    public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
    //    {
    //        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
    //        if (Controller.IsKeyDown("menu_up"))
    //            Camera.Position = Camera.Position + Camera.Forward * 100.0f * elapsedTime ;
    //        if (Controller.IsKeyDown("menu_down"))
    //            Camera.Position = Camera.Position - Camera.Forward * 100.0f * elapsedTime;
    //        if (Controller.IsKeyDown("menu_left"))
    //            Camera.RotateHorizontally(0.5f * elapsedTime);
    //        if (Controller.IsKeyDown("menu_right"))
    //            Camera.RotateHorizontally(-0.5f * elapsedTime);

    //        base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
    //    }

    //    public override void Draw(GameTime gameTime)
    //    {
    //        Device.Clear(Color.CornflowerBlue);

    //        Device.RenderState.DepthBufferEnable = true;

    //        Render3D.RenderFloorGrid(new Vector2(-10f,-10.0f), new Vector2(10.0f, 10.0f),
    //            1.0f, Color.Green);

    //        Render3D.RenderSphere(Vector3.Zero + Vector3.Up, 5.0f, Color.Blue);

    //        Vector3 scale = new Vector3(0.02f);
    //        float xDelta = 0.5f / 8.0f;
    //        float yDelta = 1.0f / 15.0f;

    //        Device.SetRenderTarget(0, target);
    //        Device.Clear(Color.Green);
    //        for (int x = 0; x < 8; x++)
    //        {
    //            for (int y = 0; y < 15; y++)
    //            {

    //                foreach (ModelMesh m in blockModel.Meshes)
    //                {
    //                    foreach (BasicEffect effect in m.Effects)
    //                    {
    //                        effect.World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(
    //                            new Vector3(-0.45f + 2f * (xDelta * x), -1f + 2f * (yDelta * y), -2.75f));
    //                        effect.View = Matrix.Identity;
    //                        //effect.View = Camera.View;
    //                        effect.Projection = Camera.Projection;
    //                        //effect.Projection = Matrix.Identity;
    //                    }
    //                    m.Draw();
    //                }
    //            }

    //        }

    //        Device.SetRenderTarget(0, null);

    //        Texture2D tex = target.GetTexture();
    //        Render2D.Render2D(tex, new Vector2(0f, 0f), new Vector2(256f, 512f), Color.White, 1.0f);


    //        base.Draw(gameTime);
    //    }
    //}
}
