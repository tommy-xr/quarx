using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace Sxe.Engine.Input
{
    /// <summary>
    /// Interface for a general purpose input to interact with the game
    /// Meant as a further level of abstraction that can encapsulate
    /// both mouse and keyboard inputs
    /// Also, meant for interacting in the game, and not necessarily with UI
    /// </summary>
    public interface IGameController : IInputEventGenerator
    {
        int PlayerIndex { get; set; }

        bool Connected { get; }

        float Sensitivity { get; set; }

        void SetRumble(float leftMotor, float rightMotor); //function to set rumble for controller

        float GetValue(string actionName); //gets a float value for keys that aren't just up or down

        bool IsKeyDown(int actionIndex);
        bool IsKeyDown(string actionName); //returns true if key is currently down

        bool IsKeyJustPressed(int actionIndex);
        bool IsKeyJustPressed(string actionName); //returns true if the key is down now but wasn't last frame

        void ClearPressed(int actionIndex);
        void ClearPressed(string actionName);

        bool IsKeyJustReleased(int actionIndex);
        bool IsKeyJustReleased(string actionName); //returns true if the key isn't down now but was down last frame

        int AddAction(string actionName); //add types of actions that the controller can recognize

        //Bind is the function that connects a given keyName with an input from keyboard, mouse,etc
        //For example Bind("forward", "mouse1") would bind the "forward" key with mouse button1
        void Bind(string key, string actionName);

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);

        IAnarchyGamer Gamer { get; set; }
    }
}
