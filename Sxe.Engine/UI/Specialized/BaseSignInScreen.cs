using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework.GamerServices;

namespace Sxe.Engine.UI
{
    public class BaseSignInScreen : BaseScreen
    {
        int controllerIndex;
        SignInModule signInModule; 


        public int ControllerIndex
        {
            get { return controllerIndex; }
            set 
            { 
                controllerIndex = value;
                this.AllowedPlayers.Clear();
                this.AllowedPlayers.Add(controllerIndex);
            }
        }

        public SignInModule SignInModule
        {
            set { signInModule = value; }
        }

        protected IAnarchyGamerService AnarchyGamerService
        {
            get
            {

                return this.GameScreenService.Services.GetService(typeof(IAnarchyGamerService)) as IAnarchyGamerService;
            }

        }

        public BaseSignInScreen()
        {
            InitializeComponent();

            this.IsPopup = true;
            this.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;



        }

        protected void DoTemporaryProfile()
        {
            if(this.AnarchyGamerService != null)
            this.AnarchyGamerService.StartTemporaryProfile((Microsoft.Xna.Framework.PlayerIndex)controllerIndex);

            this.ExitScreen();
        }

        protected void DoSignIn()
        {
            Guide.ShowSignIn(4, false);
            this.ExitScreen();
        }


        

        void InitializeComponent()
        {
        }
    }
}
