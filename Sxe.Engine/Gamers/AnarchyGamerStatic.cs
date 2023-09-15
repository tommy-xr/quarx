using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Sxe.Engine.UI;
using Sxe.Engine.Gamers;

namespace Sxe.Engine
{
    public partial class AnarchyGamer
    {
        #region Static Members

        private static AnarchyGamerCollection removeGamers = new AnarchyGamerCollection();
        private static AnarchyGamerCollection anarchyGamers = new AnarchyGamerCollection();
        public static AnarchyGamerCollection Gamers
        {
            get { return anarchyGamers; }
        }

        public static event EventHandler<AnarchySignedInEventArgs> SignedIn;
        public static event EventHandler<AnarchySignedOutEventArgs> SignedOut;

        public static BaseControllerDisconnectScreen DisconnectScreen
        {
            get { return disconnectScreen; }
            set { disconnectScreen = value; }
        }

        public static BaseSignInScreen SignInScreen
        {
            get { return signInScreen; }
            set { signInScreen = value; }
        }


        static List<string> names = new List<string>();
        static List<Texture2D> icons = new List<Texture2D>();
        static Random random = new Random();
        static BaseControllerDisconnectScreen disconnectScreen = new DefaultControllerDisconnectScreen();
        static BaseSignInScreen signInScreen = null;


        public static Texture2D ComputerIcon
        {
            get;
            set;
        }

        public static void PreCache(ContentManager content)
        {
            //TODO: Refactor this so that you can specify your own
            //We shouldn't have to go to engine code to do this
            //Maybe don't use precache, maybe just take in a list of loaded textures & names

            //Or we could have them derive from AnarchyTemporaryGamer, and maybe use a generic function to create that

            icons.Add(content.Load<Texture2D>("icons\\icon0"));
            icons.Add(content.Load<Texture2D>("icons\\icon1"));
            icons.Add(content.Load<Texture2D>("icons\\icon2"));
            icons.Add(content.Load<Texture2D>("icons\\icon3"));


            names.Add("Oscar");
            names.Add("Angela");
            names.Add("Pam");
            names.Add("Creed");
            names.Add("Michael");
            names.Add("Dwight");
            names.Add("Jim");
            names.Add("Kevin");
            names.Add("Jan");
            names.Add("Stanley");
            names.Add("Andy");
            names.Add("Marcus");
            names.Add("Cole");
            names.Add("Baird");
            names.Add("Dom");
            names.Add("Butters");
            names.Add("Kyle");
            names.Add("Chef");
            names.Add("Stan");
            names.Add("Kenny");
            names.Add("Ike");
        }

        public static Texture2D GetRandomTexture()
        {
            int index = random.Next(0, icons.Count);
            return icons[index];
        }


        public static string GetRandomName(bool remove)
        {
            int index = random.Next(0, names.Count);
            string name = names[index];

            if(remove)
            names.RemoveAt(index);

            return name;
        }

        public static void ReturnName(string name)
        {
            names.Add(name);
        }

        public static void ClearAIGamers()
        {
            removeGamers.Clear();
            for (int i = 0; i < Gamers.Count; i++)
            {
                if (!Gamers[i].IsPlayer)
                {
                    removeGamers.Add(Gamers[i]);
                }
            }

            for (int i = 0; i < removeGamers.Count; i++)
            {
                Gamers.Remove(removeGamers[i]);
            }
        }

        public static void StartTemporaryProfile(PlayerIndex pi)
        {
            if (AnarchyGamerComponent.GamerComponent != null)
                AnarchyGamerComponent.GamerComponent.StartTemporaryProfile(pi);
            else
                throw new NullReferenceException("The anarchy gamer component must be created before calling this method.");
        }

 

        #endregion

        public class AnarchyGamerCollection : Collection<IAnarchyGamer>
        {

            protected override void InsertItem(int index, IAnarchyGamer item)
            {
                base.InsertItem(index, item);

                if (item != null)
                {
                    AnarchySignedInEventArgs inEvent = new AnarchySignedInEventArgs(item);
                    if (AnarchyGamer.SignedIn != null)
                        AnarchyGamer.SignedIn(this, inEvent);

                    item.OnSignIn();
                }
            }

            protected override void RemoveItem(int index)
            {
                IAnarchyGamer gamer = this[index];

                //Moved this before firing the event
                base.RemoveItem(index);

                if (gamer != null)
                {
                    AnarchySignedOutEventArgs outEvent = new AnarchySignedOutEventArgs(gamer);
                    if (AnarchyGamer.SignedOut != null)
                        AnarchyGamer.SignedOut(this, outEvent);

                    gamer.OnSignOut();
                }
            }


            public bool IsKeyJustPressed(string keyName)
            {
                bool value = false;
                for (int i = 0; i < Count; i++)
                {
                    if (this[i].Controller != null)
                        value = value || this[i].Controller.IsKeyJustPressed(keyName);
                }

                return value;
            }

            public void MoveToEnd(IAnarchyGamer gamer)
            {
                int index = this.IndexOf(gamer);
                
                //We call base to avoid firing an event
                //This kind of lame, but i guess it will mabe work
                base.RemoveItem(index);

                //Again, calling base, so we avoid firing the OnSignedIn event
                base.InsertItem(this.Count, gamer);
            }

            public IAnarchyGamer GetGamerByPlayerIndex(int index)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (index == this[i].Index)
                        return this[i];
                }
                return null;
            }
        }
    }
}
