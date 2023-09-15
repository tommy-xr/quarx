using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Quarx.AI
{
    public struct Action
    {
        Point startPoint;
        int startFormation;
        ActionType actionType;
        Point endPoint;
        int endFormation;
        int level;

        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        public int StartFormation
        {
            get { return startFormation; }
            set { startFormation = value; }
        }

        public ActionType ActionType
        {
            get { return actionType; }
            set { actionType = value; }
        }

        public Point EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        public int EndFormation
        {
            get { return endFormation; }
            set { endFormation = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }
    }
}
