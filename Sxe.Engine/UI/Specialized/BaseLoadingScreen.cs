//using System;
//using System.Collections.Generic;
//using System.Text;

//using Sxe.Engine.UI;

//namespace Sxe.Engine.UI
//{
//    public class BaseLoadingScreen : BaseScreen
//    {
//        ILoadableCollection loadableCollection = new ILoadableCollection();
//        public ILoadableCollection LoadableCollection
//        {
//            get 
//            {
//                if (!IsLoading)
//                    return loadableCollection;
//                else
//                    //TODO: What to do if we are loading already?
//                    return null;
//            }
//        }

//        private BaseScreen nextScreen;
//        public BaseScreen NextScreen
//        {
//            get { return nextScreen; }
//            set { nextScreen = value; }
//        }


//        private bool IsLoading
//        {
//            get { return currentLoad >= 0; }
//        }

//        //protected float PercentComplete
//        //{
//        //    get
//        //    {
//        //        float delta = 1.0f / (float)(loadableCollection.Count);

//        //        float percentComplete = delta * currentLoad;

//        //        if (currentLoad < loadableCollection.Count && currentLoad >= 0)
//        //            percentComplete += delta * loadableCollection[currentLoad].PercentComplete;

//        //        return percentComplete;
//        //    }
//        //}

//        int currentLoad = -1;
        

//        public BaseLoadingScreen()
//        {
//            InitializeComponent();
//        }

//        void InitializeComponent()
//        {
//        }

//        public override void Activate()
//        {
//            currentLoad = 0;
//            if(loadableCollection.Count > 0)
//            loadableCollection[currentLoad].Load();

//            base.Activate();
//        }
//        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
//        {
//            base.Draw(spriteBatch, gameTime);
//        }

//        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
//        {
//            if (currentLoad < loadableCollection.Count)
//            {
//                if (loadableCollection[currentLoad].IsComplete)
//                {
//                    currentLoad++;
//                    if (currentLoad < loadableCollection.Count)
//                        loadableCollection[currentLoad].Load();
//                }
//            }
//            else
//            {
//                this.ExitScreen();
//                if (this.nextScreen != null)
//                    this.GameScreenService.AddScreen(this.nextScreen);
//            }


//            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
//        }
//    }
//}
