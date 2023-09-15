using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using Sxe.Engine.Input;

namespace Quarx
{
    /// <summary>
    /// Interface for all game pieces
    /// </summary>
    public interface IBlockModel
    {
        void OnRemove();

        void OnAdd(BaseGameModel model);

        IBlockModel LinkedBlock { get; set; }


        //void Update(GameTime gameTime, IGameController controller, bool firstPass);

        bool UpdateTransition(GameTime gameTime);



        bool IsRemoving { get; }


        bool CanDrop();

        bool Drop(GameTime gameTime);

    }

    /// <summary>
    /// Interface for the view of a block
    /// </summary>
    public interface IBlockView
    {
        Point Position { get; }

        BlockType Type { get; }

        BlockColor Color { get; }

        byte Alpha { get; }

        float FadeAmount { get; }

        int NearbyBlocks { get; }

        float Rotation { get; set; }
    }
}
