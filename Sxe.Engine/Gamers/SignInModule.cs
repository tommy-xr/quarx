using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine.Input;

using Microsoft.Xna.Framework;

namespace Sxe.Engine
{
    public class SignInModule : Panel
    {
        BaseScreen currentScreen = null;
        bool temporaryOnly = false;

        /// <summary>
        /// If this is true, the player will automatically be signed in as a temporary gamer,
        /// instead of showing the full screen with options to sign in as a normal gamer
        /// </summary>
        public bool TemporaryOnly
        {
            get { return temporaryOnly; }
            set { temporaryOnly = value; }
        }


        public override bool HandleEvent(InputEventArgs inputEvent)
        {
            BaseScreen parent = this.Parent as BaseScreen;
            if (parent == null)
                throw new Exception("SignInModule must have a BaseScreen as a parent.");


            if (currentScreen != null)
            {
                if (!parent.GameScreenService.ContainsScreen(currentScreen))
                    currentScreen = null;
            }
            else
            {

                ControllerChangedEventArgs changedEvent = inputEvent as ControllerChangedEventArgs;
                if (changedEvent != null)
                {
                    IAnarchyGamer gamer = AnarchyGamer.Gamers.GetGamerByPlayerIndex(changedEvent.PlayerIndex) as IAnarchyGamer;
                    if (gamer == null && Input.Controllers[changedEvent.PlayerIndex].IsKeyJustReleased("menu_select"))
                    {

                        if (TemporaryOnly)
                        {
                            AnarchyGamer.StartTemporaryProfile((Microsoft.Xna.Framework.PlayerIndex)changedEvent.PlayerIndex);
                        }
                        else
                        {

                            BaseScreen screen = this.Parent as BaseScreen;
                            if (screen.IsActive)
                            {
                                //if (AnarchyGamer.SignInScreen.TransitionPosition == 0.0f)
                                //{

                                if (AnarchyGamer.SignInScreen != null)
                                {
                                    AnarchyGamer.SignInScreen.ControllerIndex = changedEvent.PlayerIndex;
                                    screen.GameScreenService.AddScreen(AnarchyGamer.SignInScreen);
                                    currentScreen = AnarchyGamer.SignInScreen;
                                }
                                else
                                {
     
                                                  BaseScreen pc = this.Parent as BaseScreen;
                                                  if (pc != null)
                                                  {
                                                      XboxMessageInfo info = new XboxMessageInfo(new List<string>() { "Temporary profile", "Xbox profile", "Cancel" });
                                                      info.Message = "Would you like to sign in with a temporary profile or an Xbox profile?";
                                                      info.Title = "Quarx Sign-In";
                                                      info.PlayerIndex = (PlayerIndex)changedEvent.PlayerIndex;
                                                      info.MessageBoxIcon = Microsoft.Xna.Framework.GamerServices.MessageBoxIcon.Alert;
                                                      info.Completed += this.OnSignIn;
                                                      pc.GameScreenService.XboxMessage.Add(info);
                                                  }
                                    
                                }
                                //}
                            }
                        }

                    }

                }
            }

            return base.HandleEvent(inputEvent);
        }

        public void OnSignIn(object sender, XboxMessageEventArgs args)
        {
            XboxMessageInfo info = sender as XboxMessageInfo;



            if (args.Result.HasValue && info != null)
            {
                if (args.Result == 0)
                {
                    AnarchyGamer.StartTemporaryProfile(info.PlayerIndex);
                }
                else if(args.Result == 1)
                {
                    //Hacky way to get the GameScreenService
                    BaseScreen pc = this.Parent as BaseScreen;
                    if (pc != null)
                    {
                        pc.GameScreenService.XboxMessage.ShowSignIn(4, false);
                    }
                }
            }
        }



    }
}
