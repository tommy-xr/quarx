using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.Input;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Engine 
{
    public class AnarchyTemporaryGamer : BaseAnarchyGamer
    {
        public AnarchyTemporaryGamer(ContentManager inContent, int playerIndex, IGameController controller )
            : base(inContent, playerIndex, controller)
        {
            this.GamerTag = AnarchyGamer.GetRandomName(true);
            this.GamerIcon.Value = AnarchyGamer.GetRandomTexture();
        }

        public override void OnSignOut()
        {
            AnarchyGamer.ReturnName(this.GamerTag);
            base.OnSignOut();
        }
    }
}
