using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine;
using Sxe.Engine.Input;

namespace Quarx
{
    public class PlayerGameModel : BaseGameModel
    {

        double nextLateralMoveTime;
        const double lateralMoveDelay = 0.170;

        public override IGameController Controller
        {
            get
            {
                if (Gamer != null)
                    return Gamer.Controller;
                else
                    return base.Controller;
            }
        }

        public PlayerGameModel(int width, int height, IAnarchyGamer inGamer)
            : base(width, height)
        {
            Gamer = inGamer;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            float vibration = GetVibration();

            if (this.Controller != null)
                this.Controller.SetRumble(vibration, vibration);

        }

        float GetVibration()
        {
            if (this.State == QuarxGameState.Playing)
            {
                int height = this.GetMaxHeight();

                int cutoff = 8;
                if (height > cutoff)
                {
                    return (float)(height - cutoff) / (float)(Board.Height - cutoff) * 0.15f;
                }
            }

            return 0f;
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime)
        {


            if (CurrentAtomCluster != null)
            {
                if (Controller != null)
                {

                    double time = gameTime.TotalGameTime.TotalSeconds;
                    if (Controller.IsKeyJustPressed("right"))
                    {
                        CurrentAtomCluster.RotateRight();
                    }

                    if (Controller.IsKeyJustPressed("left"))
                    {
                        CurrentAtomCluster.RotateLeft();

                    }

                    if (Controller.IsKeyJustPressed("move_left") ||
                        (Controller.IsKeyDown("move_left") && nextLateralMoveTime < time))
                    {
                        CurrentAtomCluster.MoveLeft(time);
                        nextLateralMoveTime = time + lateralMoveDelay;
                    }

                    if (Controller.IsKeyJustPressed("move_right") ||
                        (Controller.IsKeyDown("move_right") && nextLateralMoveTime < time))
                    {
                        CurrentAtomCluster.MoveRight(time);
                        nextLateralMoveTime = time + lateralMoveDelay;
                    }

                    if (Controller.IsKeyJustPressed("y_button"))
                        this.Punish();

                    if (Controller.IsKeyJustPressed("x_button"))
                        this.IncrementPunishTarget();


                    if (Controller.IsKeyDown("move_down"))
                        CurrentAtomCluster.Drop();
                }
            }


            base.HandleInput(gameTime);
        }

        //public override void UpdateCurrent(Microsoft.Xna.Framework.GameTime gameTime)
        //{
        //    if (CurrentAtomCluster != null)
        //    {
        //        CurrentAtomCluster.Update(gameTime, gamer.Controller);
        //    }

        //    base.UpdateCurrent(gameTime);
        //}
    }
}
