using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Sxe.Engine.Input;
using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;
using Sxe.Engine.Gamers;

namespace Sxe.Engine
{


    /// <summary>
    /// Class that abstracts gamer information into an anarchy gamer class
    /// This allows for things like temporary profiles and such
    /// </summary>
    public partial class AnarchyGamer : BaseAnarchyGamer
    {

        IAsyncResult profileResult;
        GamerProfile profile;

        public override bool IsTemporary
        {
            get
            {
                return false;
            }
        }

        public override bool IsSignedInToLive
        {
            get
            {
                return this.Gamer.IsSignedInToLive;
            }
        }

        public override GamerPrivileges Privileges
        {
            get
            {
                return this.Gamer.Privileges;
            }
        }

        string GetPath(string fileName)
        {
            return Path.Combine(this.GamerTag, fileName);
        }

        public AnarchyGamer(ContentManager inContent, int inIndex, IGameController controller, SignedInGamer gamer)
            :base(inContent, inIndex, controller)
        {
            this.SetGamer(gamer);
        
            
            //We have to override our controllers sensitivity
            //This is sort of sloppy but its a result of sloppiness in the design of the hierarchy
            //SignedInGamers shouldn't be kept track of at the base class - this whole class tree needs refactoring
            //TODO: Do this refactoring
        }

        void SetGamer(SignedInGamer newGamer)
        {
            //if (state == AnarchyGamerState.NotSignedIn || state == AnarchyGamerState.SignedInTemporary)
            //{
                Gamer = newGamer;
                if (Gamer.IsSignedInToLive)
                    State = AnarchyGamerState.SignedInLive;
                else
                    State = AnarchyGamerState.SignedInLocal;              

                GamerTag = newGamer.Gamertag;

                this.GamerDefaults.SetFromGameDefaults(Gamer.GameDefaults);

                //Set these to null so we know to grab them up
                profileResult = null;
                GamerIcon.Value = null;
                profile = null;




            //}
        }



        public override void Update(GameTime gameTime)
        {


            if (Gamer != null)
            {
                if (profileResult == null && profile == null)
                {
                    profileResult = Gamer.BeginGetProfile(null, null);
                }
                else if (profileResult.IsCompleted && profile == null)
                {
                    profile = Gamer.EndGetProfile(profileResult);
                    GamerIcon.Value = profile.GamerPicture;
                }
            }

            base.Update(gameTime);
        }

    }



}
