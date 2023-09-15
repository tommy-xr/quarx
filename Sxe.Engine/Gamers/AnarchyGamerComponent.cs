using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

using Sxe.Engine.Input;
using Sxe.Engine.Storage;


namespace Sxe.Engine
{


    /// <summary>
    /// This does legwork for the AnarchyGamer class, including setting up the AnarchyGamer collection,
    /// and hooking the appropriate SignedInGamer events
    /// </summary>
    public class AnarchyGamerComponent : Microsoft.Xna.Framework.GameComponent, IAnarchyGamerService
    {
        static AnarchyGamerComponent gamerComponent;
        public static AnarchyGamerComponent GamerComponent
        {
            get { return gamerComponent; }
        }


        ContentManager content;
        IInputService input;
        IStorageDeviceService storageDevice;

        public AnarchyGamerComponent(Game game)
            : base(game)
        {
            gamerComponent = this;
            game.Services.AddService(typeof(IAnarchyGamerService), this);

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            content = new ContentManager(Game.Services);
            //Find the input service
            input = this.Game.Services.GetService(typeof(IInputService)) as IInputService;
            storageDevice = this.Game.Services.GetService(typeof(IStorageDeviceService)) as IStorageDeviceService;


            if (input == null)
                throw new Exception("Could not find input service.");


            SignedInGamer.SignedIn += OnGamerSignedIn;
            SignedInGamer.SignedOut += OnGamerSignedOut;

           

            base.Initialize();
        }

        public void AddAI(IAnarchyGamer gamer)
        {
            if(!AnarchyGamer.Gamers.Contains(gamer))
            AnarchyGamer.Gamers.Add(gamer);

        }

        public void StartTemporaryProfile(PlayerIndex pi)
        {
            IAnarchyGamer gamer = AnarchyGamer.Gamers.GetGamerByPlayerIndex((int)pi);
            if (gamer == null)
            {
                gamer = new AnarchyTemporaryGamer(content, (int)pi, input.Controllers[(int)pi]);
                AnarchyGamer.Gamers.Add(gamer);
            }
            //    gamer = new AnarchyGamer(content, (int)pi);
            //    AnarchyGamer.Gamers.Add(gamer);

            //    gamer.Controller = input.Controllers[(int)pi];
            //    gamer.Controller.Gamer = gamer;
            //}

            //gamer.StartTemporary();
        }

        void OnGamerSignedIn(object sender, SignedInEventArgs args)
        {
            //Check the player index - do we have an anarchy gamer already here?
            IAnarchyGamer gamer = AnarchyGamer.Gamers.GetGamerByPlayerIndex((int)args.Gamer.PlayerIndex) as IAnarchyGamer;

            if (gamer != null)
            {
                if (gamer.IsTemporary)
                {
                    AnarchyGamer.Gamers.Remove(gamer);
                    gamer = null;
                }
            }

            if (gamer == null)
            {
                gamer = new AnarchyGamer(content, (int)args.Gamer.PlayerIndex, input.Controllers[(int)args.Gamer.PlayerIndex],
                    args.Gamer);
                gamer.StorageDevice = this.storageDevice;
                //gamer = new AnarchyGamer(this.content, (int)args.Gamer.PlayerIndex);
                //gamer.Controller = input.Controllers[(int)args.Gamer.PlayerIndex];
                //gamer.Controller.Gamer = gamer;
                AnarchyGamer.Gamers.Add(gamer);

            }
        }

        void OnGamerSignedOut(object sender, SignedOutEventArgs args)
        {
            IAnarchyGamer gamer = AnarchyGamer.Gamers.GetGamerByPlayerIndex((int)args.Gamer.PlayerIndex) as IAnarchyGamer;

            if (gamer != null)
            {
                //gamer.SignOut();
                AnarchyGamer.Gamers.Remove(gamer);
            }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            for (int i = 0; i < AnarchyGamer.Gamers.Count; i++)
                AnarchyGamer.Gamers[i].Update(gameTime);

            base.Update(gameTime);
        }
    }
}