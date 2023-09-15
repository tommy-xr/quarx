using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Sxe.Engine;

using Sxe.Library.Editor;

using Sxe.Library.Utilities;

namespace Sxe.Editor
{
    /// <summary>
    /// This is a base editor class with handy items just for editing
    /// </summary>
    public class SxeEditor : SxeGame
    {
        EditorScreen editorScreen;
        protected EditorScreen EditorScreen
        {
            get { return editorScreen; }
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.InputComponent.FreezeMouse = false;

            //editorScreen = new EditorScreen(this.GameScreenComponent, this.Content);
            editorScreen = new EditorScreen();
            //editorScreen.Initialize(this.GameScreenComponent, this.Content);

            GameScreenComponent.AddScreen(editorScreen);

        }
    }
}
