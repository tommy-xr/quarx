using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.UI;
using Sxe.Library.Editor;

namespace Sxe.Editor.UI
{
    public class JewelSubPanel : Panel
    {
        List<MenuStripEntry> entries;
        SpriteFont font;
        Label[] labels;

        public JewelSubPanel(List<MenuStripEntry> menuEntries)
        {
            entries = menuEntries;
        }

        public override void ApplyScheme(IScheme scheme)
        {
            base.ApplyScheme(scheme);

            font = scheme.GetFont("default_font");

            Texture2D tex = scheme.GetTexture("blank");
            this.Image = new UIImage(tex);
            this.BackColor = Color.Gray;

            ResetLabels();
        }

        void ResetLabels()
        {
            if (labels != null)
            {
                for (int i = 0; i < labels.Length; i++)
                    labels[i].Parent = null;
            }

            int delta = (int)(font.MeasureString("W").Y);
            this.Size = new Point(this.Size.X, entries.Count * delta);

            labels = new Label[entries.Count];
            for (int i = 0; i < entries.Count; i++)
            {
                labels[i] = new Label();
                labels[i].Parent = this;
                labels[i].Location = new Point(0, delta * i);
                labels[i].Size = new Point(this.Size.X, delta);
                labels[i].Caption = entries[i].Caption;
                labels[i].Font = font;
            }
        }
    }
}
