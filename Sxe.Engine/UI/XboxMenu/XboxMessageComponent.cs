using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;


namespace Sxe.Engine.UI
{
    public class XboxMessageComponent : GameComponent, IXboxMessageService
    {
        //TODO: Refactor this so that the queue can handle other types, instead of just messages

        private Queue<XboxMessageInfo> messages = new Queue<XboxMessageInfo>();
        private XboxMessageInfo currentInfo = null;
        private IAsyncResult currentResult = null;
        private XboxMessageEventArgs args = new XboxMessageEventArgs();

        private bool showMarketPlace = false;
        private PlayerIndex marketIndex = PlayerIndex.One;

        private bool showSignIn = false;
        private int showSignInNumber = 4;
        private bool showSignInOnlineOnly = false;


        public XboxMessageComponent(Game game)
            : base(game)
        {
            Game.Services.AddService(typeof(IXboxMessageService), this);
        }

        public void ShowMarketPlace(PlayerIndex playerIndex)
        {
            IAnarchyGamer gamer = AnarchyGamer.Gamers.GetGamerByPlayerIndex((int)playerIndex);
            if (Guide.IsTrialMode && gamer != null)
            {
                //Make sure we know this gamer can buy and shiz
                bool canBuy = true;

                if (!gamer.IsSignedInToLive)
                    canBuy = false;

                if (gamer.Privileges == null)
                    canBuy = false;
                else
                {
                    if (!gamer.Privileges.AllowPurchaseContent)
                    {
                        canBuy = false;
                    }
                }

                if (canBuy)
                {
                    showMarketPlace = true;
                    marketIndex = (PlayerIndex)gamer.Index;
                }
                else
                {
                    XboxMessageInfo failedBuyInfo = new XboxMessageInfo(new List<string>() { "OK" });
                    failedBuyInfo.Title = "Sorry!";

                    failedBuyInfo.Message = "We couldn't take you to the marketplace.\n\n";
                    failedBuyInfo.Message += "You must:\n";
                    failedBuyInfo.Message += "-Be signed in to XBOX live.\n";
                    failedBuyInfo.Message += "-Have permission to purchase content.\n\n";
                    failedBuyInfo.Message += "Thank you for your interest!";

                    failedBuyInfo.PlayerIndex = (PlayerIndex)gamer.Index;

                    failedBuyInfo.MessageBoxIcon = MessageBoxIcon.Error;
                    this.Add(failedBuyInfo);
                }

    
            }
        }

        public void ShowSignIn(int paneCount, bool onlineOnly)
        {
            showSignIn = true;
            showSignInNumber = paneCount;
            showSignInOnlineOnly = onlineOnly;
        }

        public void Add(XboxMessageInfo messageInfo)
        {
            this.messages.Enqueue(messageInfo);
        }
        
        public override void Update(GameTime gameTime)
        {

           
            if (this.currentInfo == null && this.messages.Count > 0)
            {
                this.currentInfo = this.messages.Dequeue();
            }

            if (this.currentInfo != null && this.currentResult == null)
            {
                if(!Guide.IsVisible)
                this.currentResult = Guide.BeginShowMessageBox((PlayerIndex)this.currentInfo.PlayerIndex, this.currentInfo.Title, this.currentInfo.Message,
                    this.currentInfo.Options, 0, this.currentInfo.MessageBoxIcon, null, null);
            }
            
            if (this.currentInfo != null && this.currentResult != null)
            {
                if (this.currentResult.IsCompleted)
                {
                    //Get result: 
                    int? result = Guide.EndShowMessageBox(this.currentResult);                    
                    this.args.Result = result;
                    this.args.PlayerIndex = this.currentInfo.PlayerIndex;

                    //Fire the event
                    this.currentInfo.FireEvent(this.args);

                    //Clear our info, so we're ready for the next round
                    this.currentResult = null;
                    this.currentInfo = null;
                }
            }

            if(!Guide.IsVisible)
            {
                if (this.messages.Count == 0 && this.currentInfo == null && this.currentResult == null)
                {
                    if (this.showMarketPlace)
                    {
                        this.showMarketPlace = false;

                        Guide.ShowMarketplace(marketIndex);

                        
                            
                        
                    }
                    else if (this.showSignIn)
                    {
                        this.showSignIn = false;
                        Guide.ShowSignIn(showSignInNumber, showSignInOnlineOnly);
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
