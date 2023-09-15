//using System;
//using System.Collections.Generic;
//using System.Text;

////using Scurvy.Media;

//using Sxe.Engine.UI;
//using Microsoft.Xna.Framework;

//namespace Quarx.VideoImage
//{
//    public class VideoImage : UIImage
//    {

//        VideoContentManager contentManager;
//        Video video;

//        public void Play()
//        {
//            video.Play();
//        }

//        public void Stop()
//        {
//            video.Stop();
//        }

//        public double FrameRate
//        {
//            get { return video.FrameRate; }
//        }

//        public bool Loop
//        {
//            get { return video.Loop; }
//            set { video.Loop = value; }
//        }

//        public event EventHandler End;

//        public VideoImage(string fileName, IServiceProvider services)
//        {
//            contentManager = new VideoContentManager(services, "Content");
//            video = contentManager.Load<Video>(fileName);
//            video.End += OnEnd;
            
//        }

//        void OnEnd(object sender, EventArgs args)
//        {
//            if (End != null)
//                End(this, EventArgs.Empty);
//        }


//        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch, Rectangle rectangle, Microsoft.Xna.Framework.Rectangle destination, Microsoft.Xna.Framework.Graphics.Color inColor)
//        {
//            batch.Draw(video.CurrentTexture, destination, this.Color);
//            base.Draw(batch, rectangle, destination, inColor);
//        }

//        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
//        {
//            video.Update();
//            base.Update(gameTime);
//        }

//        public override void UnloadContent()
//        {
//            if(contentManager != null)
//            contentManager.Dispose();
//            base.UnloadContent();
//        }
     
//    }
//}
