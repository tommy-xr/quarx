using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Engine
{
    /// <summary>
    /// Base class for SXE game components.
    /// </summary>
    public class SxeGameComponent : IGameComponent
    {
        SxeGame game;
        bool enabled = true;
        TimeSpan lastUpdateTime;

        /// <summary>
        /// Indicates whether update should be called
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        /// <summary>
        /// If debug mode is enabled, keeps track of the update time for this component
        /// </summary>
        public TimeSpan LastUpdateTime
        {
            get { return lastUpdateTime; }
            set { lastUpdateTime = value; }
        }

        /// <summary>
        /// Gets the game associated with this game component
        /// </summary>
        public SxeGame Game
        {
            get { return game; }
            set { game = value; }
        }

        /// <summary>
        /// Initializes a new instance of the class
        /// </summary>
        /// <param name="Game">An SXE game</param>
        public SxeGameComponent(SxeGame Game)
        {
            game = Game;
        }

        /// <summary>
        /// Called when the SXE game component needs to be initialized. Override this
        /// to query for services and load any non-graphics resources.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Called when the game component needs to be updated
        /// </summary>
        /// <param name="gameTime"></param>
        public void BaseUpdate(GameTime gameTime)
        {
            DateTime start = new DateTime();
            DateTime end = new DateTime();
            if (Globals.DebugMode)
            {
                start = DateTime.Now;
            }

            Update(gameTime);

            if (Globals.DebugMode)
            {
                end = DateTime.Now;
                lastUpdateTime = end - start;
            }

        }

        protected virtual void Update(GameTime gameTime)
        {
        }

    }
}
