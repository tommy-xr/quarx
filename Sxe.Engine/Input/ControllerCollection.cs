using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.Input
{
    /// <summary>
    /// A collection of BaseGameControllers
    /// </summary>
    public class ControllerCollection : Collection<BaseGameController>
    {

        public bool IsKeyDown(string actionName)
        {
            bool value = false;
            for (int i = 0; i < Count; i++)
                value = value || this[i].IsKeyDown(actionName);

            return value;
        }

        public bool IsKeyDown(int actionIndex)
        {
            bool value = false;
            for (int i = 0; i < Count; i++)
                value = value || this[i].IsKeyDown(actionIndex);

            return value;
        }

        public bool IsKeyJustPressed(int actionIndex)
        {
            bool value = false;
            for (int i = 0; i < Count; i++)
                value = value || this[i].IsKeyJustPressed(actionIndex);
            return value;
        }

        public bool IsKeyJustPressed(string actionName)
        {
            bool value = false;
            for (int i = 0; i < Count; i++)
                value = value || this[i].IsKeyJustPressed(actionName);
            return value;
        }

        public int AddAction(string actionName)
        {
            //TODO: Verify that all controllers give same action
            int action = -1;
            for (int i = 0; i < Count; i++)
                action = this[i].AddAction(actionName);

            return action;
        }

    }
}
