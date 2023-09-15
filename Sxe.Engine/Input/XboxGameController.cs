using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Sxe.Engine.Input
{
    public class XboxGameController : BaseGameController
    {
        Point xboxCursor;
        bool connected = true;

        bool rumbleSet = false;

        float leftRumble = 0.0f;
        float rightRumble = 0.0f;

        public override bool Connected
        {
            get
            {
                return connected; 
            }
        }


        public XboxGameController(IServiceProvider services, ContentManager content, int playerIndex)
            : base(services, content, playerIndex)
        {
            //Add xbox buttons
            AddKey("a_button");
            AddKey("b_button");
            AddKey("x_button");
            AddKey("y_button");
            AddKey("left_stick_button");
            AddKey("right_stick_button");
            AddKey("back_button");
            AddKey("start_button");
            AddKey("left_shoulder_button");
            AddKey("right_shoulder_button");

            //Add dpad
            AddKey("dpad_up");
            AddKey("dpad_down");
            AddKey("dpad_left");
            AddKey("dpad_right");

            //Add thumbstick
            AddKey("left_stick_left");
            AddKey("left_stick_right");
            AddKey("left_stick_up");
            AddKey("left_stick_down");
            AddKey("right_stick_left");
            AddKey("right_stick_right");
            AddKey("right_stick_up");
            AddKey("right_stick_down");

            AddKey("xbox_cursor_x");
            AddKey("xbox_cursor_y");

            //Trigger
            AddKey("left_trigger");
            AddKey("right_trigger");


            Bind("xbox_cursor_x", "cursor_x");
            Bind("xbox_cursor_y", "cursor_y");
            Bind("a_button", "cursor_leftclick");
            Bind("b_button", "cursor_rightclick");

            Bind("left_stick_down", "menu_down");
            Bind("left_stick_up", "menu_up");
            Bind("left_stick_left", "menu_left");
            Bind("left_stick_right", "menu_right");
            Bind("dpad_left", "menu_left");
            Bind("dpad_down", "menu_down");
            Bind("dpad_up", "menu_up");
            Bind("dpad_right", "menu_right");

            Bind("a_button", "menu_select");
            Bind("b_button", "menu_back");



        }

        public override void SetRumble(float leftMotor, float rightMotor)
        {
            leftRumble = leftMotor;
            rightRumble = rightMotor;

            rumbleSet = true;


            base.SetRumble(leftMotor, rightMotor);
        }

        public override void ControllerUpdate(GameTime gameTime)
        {
            GamePadState currentState = GamePad.GetState( (PlayerIndex)PlayerIndex);


                GamePad.SetVibration((PlayerIndex)PlayerIndex, leftRumble, rightRumble);

            if (rumbleSet)
                    rumbleSet = false;
            else if(!rumbleSet)
            {
                leftRumble = 0f;
                rightRumble = 0f;
                rumbleSet = false;
            }
            

            connected = currentState.IsConnected;
      

            SetValue("a_button", ButtonToValue(currentState.Buttons.A));
            SetValue("b_button", ButtonToValue(currentState.Buttons.B));
            SetValue("x_button", ButtonToValue(currentState.Buttons.X));
            SetValue("y_button", ButtonToValue(currentState.Buttons.Y));
            SetValue("left_stick_button", ButtonToValue(currentState.Buttons.LeftStick));
            SetValue("right_stick_button", ButtonToValue(currentState.Buttons.RightStick));
            SetValue("back_button", ButtonToValue(currentState.Buttons.Back));
            SetValue("start_button", ButtonToValue(currentState.Buttons.Start));
            SetValue("left_shoulder_button", ButtonToValue(currentState.Buttons.LeftShoulder));
            SetValue("right_shoulder_button", ButtonToValue(currentState.Buttons.RightShoulder));
            SetValue("dpad_up", ButtonToValue(currentState.DPad.Up));
            SetValue("dpad_down", ButtonToValue(currentState.DPad.Down));
            SetValue("dpad_left", ButtonToValue(currentState.DPad.Left));
            SetValue("dpad_right", ButtonToValue(currentState.DPad.Right));

            float leftStickX = currentState.ThumbSticks.Left.X;
            SetValue("left_stick_left", GetValueFromStickValue(leftStickX, -1f));
            SetValue("left_stick_right", GetValueFromStickValue(leftStickX, 1f));

            float leftStickY = currentState.ThumbSticks.Left.Y;
            SetValue("left_stick_up", GetValueFromStickValue(leftStickY, 1f));
            SetValue("left_stick_down", GetValueFromStickValue(leftStickY, -1f));

            float rightStickX = currentState.ThumbSticks.Right.X;
            SetValue("right_stick_left", GetValueFromStickValue(rightStickX, -1f));
            SetValue("right_stick_right", GetValueFromStickValue(rightStickX, 1f));

            float rightStickY = currentState.ThumbSticks.Right.Y;
            SetValue("right_stick_up", GetValueFromStickValue(rightStickY, 1f));
            SetValue("right_stick_down", GetValueFromStickValue(rightStickY, -1f));


            xboxCursor.X += (int)(currentState.ThumbSticks.Left.X * 2.5f);
            xboxCursor.Y -= (int)(currentState.ThumbSticks.Left.Y * 2.5f);

            SetValue("xbox_cursor_x", xboxCursor.X);
            SetValue("xbox_cursor_y", xboxCursor.Y);




        }

        private float GetValueFromStickValue(float value, float bias)
        {
            value *= bias;
            return MathHelper.Clamp(value, 0f, 1f);
            
        }
    }
}
