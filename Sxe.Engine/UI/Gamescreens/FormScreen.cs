using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{
    /// <summary>
    /// A form screen is a game screen that can display UI forms
    /// </summary>
    public class FormScreen : BaseScreen
    {
        FormCollection forms;
        public FormCollection Forms
        {
            get { return forms; }
        }

        public FormScreen()
        {
            forms = new FormCollection();
        }


        //public FormScreen(IGameScreenService service, ContentManager content)
        //    : base(service, content)
        //{ }

        //public override void Initialize(IGameScreenService service, Microsoft.Xna.Framework.Content.ContentManager content)
        //{
        //    base.Initialize(service,  content);
        //    forms = new FormCollection();
        //    //this.AddEventHandler(forms);
        //}

        //public override void Activate()
        //{
        //    forms.Activate();
        //    base.Activate();
        //}

        //public override void Deactivate()
        //{
        //    forms.Deactivate();
        //    base.Deactivate();
        //}

        public override void PreDraw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            //Split into a pre draw 
            forms.DrawForms(spriteBatch);
            base.PreDraw(spriteBatch, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            base.Draw(spriteBatch,gameTime);

            //And a post draw
            forms.Draw(spriteBatch);
        }

        public override void  Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            forms.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            //TODO:
            //Fix this - for a FormScreen, we normalized the event twice... so that is not good!!!
            //NormalizeEvent(inputEvent);
           //First, check if the forms manager handles the event
            if (!forms.HandleEvent(inputEvent))
            {
                return base.HandleEvent(inputEvent);
            }
            return true;
        }
    }
}
