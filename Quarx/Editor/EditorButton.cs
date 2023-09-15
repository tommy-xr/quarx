using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine.UI.Buttons;

using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public class EditorButton : ColorButton
    {
        UIImage tileOutline;

        PuzzleEditor Editor
        {
            get { return this.Parent as PuzzleEditor; }
        }

        BlockType type = BlockType.Blob;
        BlockColor color = BlockColor.Null;

        public BlockType BlockType
        {
            get { return type; }
            set { type = value; }
        }

        public BlockColor BlockColor
        {
            get { return color; }
            set { color = value; }
        }

        public EditorButton()
        {
            this.MouseClick += OnClick;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            tileOutline = this.LoadImage(content, "Editor/tileOutline");
            base.LoadContent(content);

            this.OverColor = Color.Green;
            this.ClickColor = Color.Silver;
            this.DefaultColor = Color.White;

            this.ColorTransitionTime = TimeSpan.FromSeconds(1.0);
        }

      

        public override void PaintAll(SpriteBatch sb, Microsoft.Xna.Framework.Rectangle rectangle)
        {
            base.PaintAll(sb, rectangle);

            if(Editor != null && Editor.SelectedButton == this)
            tileOutline.Draw(sb, rectangle, this.BackColor);
        }

        void OnClick(object sender, EventArgs args)
        {
            if(Editor != null)
            Editor.SelectedButton = this;
        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            return base.HandleEvent(inputEvent);
        }

        

    }
}
