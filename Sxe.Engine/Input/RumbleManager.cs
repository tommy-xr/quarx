//using System;
//using System.Collections.Generic;
//using System.Text;

//using Microsoft.Xna.Framework;

//namespace Sxe.Engine.Input
//{
//    public enum RumbleMode
//    {
//        Constant = 0,
//        Linear,
//        Quadratic
//    }

//    public class RumbleManager
//    {

//        struct RumbleInfo
//        {
//            public double LeftIntensity;
//            public double RightIntensity;
//            public double Duration;
//            public double CurrentDuration;
//            public RumbleMode mode;
//        }

//        float leftMotor = 0.0f;
//        float rightMotor = 0.0f;

//        List<RumbleInfo> removeRumbles = new List<RumbleInfo>();
//        List<RumbleInfo> currentRumbles = new List<RumbleInfo>();

//        public void AddRumble(double duration, double leftIntensity, double rightIntensity, RumbleMode mode)
//        {
//            RumbleInfo info = new RumbleInfo();
//            info.LeftIntensity = leftIntensity;
//            info.RightIntensity = rightIntensity;
//            info.Duration = duration;
//            info.CurrentDuration = 0.0;
//            info.mode = mode;
//            currentRumbles.Add(info);
//        }

//        public float LeftMotor
//        {
//            get { return 0.0f; }
//        }

//        public float RightMotor
//        {
//            get { return 0.0f; }
//        }

//        public void Update(GameTime gameTime)
//        {
//            leftMotor = 0.0f;
//            rightMotor = 0.0f;

//            removeRumbles.Clear();

//            for (int i = 0; i < currentRumbles.Count; i++)
//            {
//                if (currentRumbles[i].CurrentDuration >= currentRumbles[i].Duration)
//                {
//                    removeRumbles.Add(currentRumbles[i]);
//                }
//                else
//                {
//                    float transition = currentRumbles[i].CurrentDuration / currentRumbles[i].Duration;

//                    if (currentRumbles[i].mode == RumbleMode.Constant)
//                    {
//                        leftMotor += currentRumbles[i].LeftIntensity;
//                        rightMotor += currentRumbles[i].RightIntensity;
//                    }
//                    else if (currentRumbles[i].mode == RumbleMode.Linear || currentRumbles[i].mode == RumbleMode.Quadratic)
//                    {
//                        float leftAddAmount = 0.0f;
//                        float rightAddAmount = 0.0f;
//                        if (transition > 0.5f)
//                            transition = 2f * (1.0f - transition);
//                        else
//                            transition *= 2f;

//                        if (currentRumbles[i].mode == RumbleMode.Quadratic)
//                            transition = (float)Math.Pow((double)transition, 2.0);

//                        leftAddAmount = transition * 2f * leftMotor;
//                        rightAddAmount = transition * 2f * rightMotor;

//                        leftMotor += leftAddAmount;
//                        rightMotor += rightAddAmount;
//                    }


//                }
//            }

//            leftMotor = MathHelper.Clamp(leftMotor, 0.0f, 1.0f);
//            rightMotor = MathHelper.Clamp(rightMotor, 0.0f, 1.0f);

//            for (int i = 0; i < removeRumbles.Count; i++)
//            {
//                currentRumbles.Remove(removeRumbles[i]);
//            }
        
//        }

//    }
//}
