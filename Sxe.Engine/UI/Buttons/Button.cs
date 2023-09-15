using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#if !XBOX
using System.ComponentModel.Design;
using Sxe.Design;
#endif

namespace Sxe.Engine.UI
{
    /// <summary>
    /// A basic button
    /// </summary>
#if !XBOX
    [ToolboxItemFilter("AnarchyUI", ToolboxItemFilterType.Require)]
#endif
    public class Button : ButtonBase
    {
        UIImage defaultImage;
        UIImage overImage;
        UIImage clickImage;
        UIImage overlayTextImage;

        SpriteFont font;

        string defaultImagePath;

        [ReloadContent]
#if !XBOX
        [TypeConverter(typeof(TextureContentNameConverter))]
#endif
        public string DefaultImagePath
        {
            get { return defaultImagePath; }
            set { defaultImagePath = value; }
        }

        string overImagePath;
        [ReloadContent]
#if !XBOX
        [TypeConverter(typeof(TextureContentNameConverter))]
#endif
        public string OverImagePath
        {
            get { return overImagePath; }
            set { overImagePath = value; }
        }

        string clickImagePath;
        [ReloadContent]
#if !XBOX
        [TypeConverter(typeof(TextureContentNameConverter))]
#endif
        public string ClickImagePath
        {
            get { return clickImagePath; }
            set { clickImagePath = value; }
        }

        string textImagePath;
        [ReloadContent]
#if !XBOX
        [TypeConverter(typeof(TextureContentNameConverter))]
#endif
        public string TextImagePath
        {
            get { return textImagePath; }
            set { textImagePath = value; }
        }

        string fontName;
        [ReloadContent]
        public string FontName
        {
            get { return fontName; }
            set { fontName = value; }
        }

        string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

#if !XBOX
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public UIImage DefaultImage
        {
            get { return defaultImage; }
            set { defaultImage = value; }
        }

#if !XBOX
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public UIImage ClickImage
        {
            get { return clickImage; }
            set { clickImage = value; }
        }

#if !XBOX
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public UIImage OverImage
        {
            get { return overImage; }
            set { overImage = value; }
        }

        public override void Update(GameTime gameTime)
        {
            if (clickImage != null)
                clickImage.Update(gameTime);

            if (defaultImage != null)
                defaultImage.Update(gameTime);

            if (overImage != null)
                overImage.Update(gameTime);

            base.Update(gameTime);
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);
            if(defaultImagePath != null)
            defaultImage = LoadImage(content, DefaultImagePath);

            if(overImagePath != null)
            overImage = LoadImage(content, OverImagePath);

            if(clickImagePath != null)
            clickImage = LoadImage(content, ClickImagePath);

        if (textImagePath != null)
            overlayTextImage = LoadImage(content, TextImagePath);

            if(fontName != null)
            font = content.Load<SpriteFont>(FontName);

        }

        public override void Over(int index)
        {
            AnimatedImage animation = this.OverImage as AnimatedImage;
            if (animation != null)
                animation.Play(0);

            base.Over(index);
        }

        public override void PerformClick(int index)
        {
            AnimatedImage animation = this.ClickImage as AnimatedImage;
            if (animation != null)
                animation.Play(0);

            base.PerformClick(index);
        }

        public override void PaintNormal(SpriteBatch sb, Rectangle destinationRect)
        {
            base.PaintNormal(sb, destinationRect);
            if (defaultImage != null)
                defaultImage.Draw(sb, destinationRect, Color.White);

            DrawText(sb, destinationRect);
        }

        public override void PaintOver(SpriteBatch sb, Rectangle destinationRect)
        {
            base.PaintOver(sb, destinationRect);
            if (overImage != null)
                overImage.Draw(sb, destinationRect, Color.White);

            DrawText(sb, destinationRect);
        }
        public override void PaintPressed(SpriteBatch sb, Rectangle destinationRect)
        {
            base.PaintPressed(sb, destinationRect);
            if (clickImage != null)
                clickImage.Draw(sb, destinationRect, Color.White);

            DrawText(sb, destinationRect);
        }

        void DrawText(SpriteBatch sb, Rectangle destinationRect)
        {
            if (overlayTextImage != null)
                overlayTextImage.Draw(sb, destinationRect, Color.White);
        }

    }
}
