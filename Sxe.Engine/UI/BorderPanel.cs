using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{
    //public enum BorderPanelMode
    //{
    //    Stretch = 0,
    //    Resize
    //}

    ///// <summary>
    ///// Like a Panel, but the image is a border instead of a normal texture
    ///// </summary>
    //public class BorderPanel : Panel
    //{
    //    class BorderSizeInfo
    //    {
    //        public Rectangle TopLeft;
    //        public Rectangle Top;
    //        public Rectangle TopRight;
    //        public Rectangle Left;
    //        public Rectangle Middle;
    //        public Rectangle Right;
    //        public Rectangle BottomLeft;
    //        public Rectangle Bottom;
    //        public Rectangle BottomRight;
    //    }

    //    Panel topLeftCorner;
    //    Panel topRightCorner;
    //    Panel botLeftCorner;
    //    Panel botRightCorner;

    //    Panel top;
    //    Panel left;
    //    Panel bottom;
    //    Panel right;

    //    Panel middle;


    //    public Panel Middle
    //    {
    //        get { return middle; }
    //    }

    //    public BorderPanel(IPanelContainer parent, Point location, Point size, UIImage inBackground, BorderPanelMode mode)
    //        : base(parent, location, size, inBackground, null)
    //    {
    //        if (mode == BorderPanelMode.Resize)
    //        {
    //            Texture2D borderImg = this.Image.Value;

    //            int cornerWidthPixels = (borderImg.Width - 2)/2;
    //            int cornerHeightPixels = (borderImg.Height - 2)/2;

    //            topLeftCorner = new Panel(this, Point.Zero, Point.Zero, null);
    //            topLeftCorner.Image = new UIImage(borderImg, new Rectangle(0, 0, cornerWidthPixels, cornerHeightPixels));

    //            topRightCorner = new Panel(this, Point.Zero, Point.Zero, null);
    //            topRightCorner.Image = new UIImage(borderImg, new Rectangle(cornerWidthPixels + 2, 0, cornerWidthPixels, cornerHeightPixels));

    //            botLeftCorner = new Panel(this, Point.Zero, Point.Zero, null);
    //            botLeftCorner.Image = new UIImage(borderImg, new Rectangle(0, cornerHeightPixels + 2, cornerWidthPixels, cornerHeightPixels));

    //            botRightCorner = new Panel(this, Point.Zero, Point.Zero, null);
    //            botRightCorner.Image = new UIImage(borderImg, new Rectangle(cornerWidthPixels + 2, cornerHeightPixels + 2, cornerWidthPixels, cornerHeightPixels));

    //            top = new Panel(this, Point.Zero, Point.Zero, null);
    //            top.Image = new UIImage(borderImg, new Rectangle(cornerWidthPixels, 0, 2, cornerHeightPixels));

    //            left = new Panel(this, Point.Zero, Point.Zero, null);
    //            left.Image = new UIImage(borderImg, new Rectangle(0, cornerHeightPixels, cornerWidthPixels, 2));

    //            right = new Panel(this, Point.Zero, Point.Zero, null);
    //            right.Image = new UIImage(borderImg, new Rectangle(cornerWidthPixels + 2, cornerHeightPixels, cornerWidthPixels, 2));

    //            bottom = new Panel(this, Point.Zero, Point.Zero, null);
    //            bottom.Image = new UIImage(borderImg, new Rectangle(cornerWidthPixels, cornerHeightPixels + 2, 2, cornerHeightPixels));

    //            middle = new Panel(this, Point.Zero, Point.Zero, null);
    //            middle.Image = new UIImage(borderImg, new Rectangle(cornerWidthPixels, cornerHeightPixels, 2, 2));

    //            ResetSize();
    //        }
            

    //    }

    //    void ResetSize()
    //    {
    //        Texture2D borderImg = this.Image.Value;

    //        int cornerWidthPixels = (borderImg.Width / 2 - 2);
    //        int cornerHeightPixels = (borderImg.Height / 2 - 2);

    //        BorderSizeInfo info = CreateSizeInfo(this.Size, new Point(cornerWidthPixels, cornerHeightPixels));

    //        topLeftCorner.Location = new Point(info.TopLeft.X, info.TopLeft.Y);
    //        topLeftCorner.Size = new Point(info.TopLeft.Width, info.TopLeft.Height);
    //        top.Location = new Point(info.Top.X, info.Top.Y);
    //        top.Size = new Point(info.Top.Width, info.Top.Height);
    //        topRightCorner.Location = new Point(info.TopRight.X, info.TopRight.Y);
    //        topRightCorner.Size = new Point(info.TopRight.Width, info.TopRight.Height);

    //        left.Location = new Point(info.Left.X, info.Left.Y);
    //        left.Size = new Point(info.Left.Width, info.Left.Height);
    //        middle.Location = new Point(info.Middle.X, info.Middle.Y);
    //        middle.Size = new Point(info.Middle.Width, info.Middle.Height);
    //        right.Location = new Point(info.Right.X, info.Right.Y);
    //        right.Size = new Point(info.Right.Width, info.Right.Height);

    //        botLeftCorner.Location = new Point(info.BottomLeft.X, info.BottomLeft.Y);
    //        botLeftCorner.Size = new Point(info.BottomLeft.Width, info.BottomLeft.Height);
    //        bottom.Location = new Point(info.Bottom.X, info.Bottom.Y);
    //        bottom.Size = new Point(info.Bottom.Width, info.Bottom.Height);
    //        botRightCorner.Location = new Point(info.BottomRight.X, info.BottomRight.Y);
    //        botRightCorner.Size = new Point(info.BottomRight.Width, info.BottomRight.Height);
    //    }

    //    static BorderSizeInfo CreateSizeInfo(Point absolutePanelSize, Point cornerSize)
    //    {
    //        BorderSizeInfo info = new BorderSizeInfo();

    //        Point filler = new Point(absolutePanelSize.X - 2 * cornerSize.X, absolutePanelSize.Y - 2 * cornerSize.Y);

    //        int topPosition = 0;
    //        int topSize = cornerSize.Y;
    //        info.TopLeft = new Rectangle(0, topPosition, cornerSize.X,topSize);
    //        info.Top = new Rectangle(cornerSize.X, topPosition, filler.X,topSize);
    //        info.TopRight = new Rectangle(cornerSize.X + filler.X, topPosition, cornerSize.X,topSize);

    //        int middlePosition = cornerSize.Y;
    //        int middleSize = filler.Y;
    //        info.Left = new Rectangle(0, middlePosition, cornerSize.X, middleSize);
    //        info.Middle = new Rectangle(cornerSize.X, middlePosition, filler.X, middleSize);
    //        info.Right = new Rectangle(cornerSize.X + filler.X, middlePosition, cornerSize.X, middleSize);

    //        int bottomPosition = cornerSize.Y + filler.Y;
    //        int bottomSize = cornerSize.Y;
    //        info.BottomLeft = new Rectangle(0, bottomPosition, cornerSize.X, bottomSize);
    //        info.Bottom = new Rectangle(cornerSize.X, bottomPosition, filler.X, bottomSize);
    //        info.BottomRight = new Rectangle(cornerSize.X + filler.X, bottomPosition, cornerSize.X, bottomSize);

    //        return info;
    //    }

    //}
}
