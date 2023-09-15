using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{
    public class EditForm : OldForm
    {
        public event EventHandler<EventArgs> Save;

        public EditForm(ISchemeManager schemes)
            : base(schemes)
        {
            this.Location = new Point(10, 10);
            this.Size = new Point(100, 100);
            //Button saveButton = new Button(this.ClientArea, new Point(0, 0), new Point(75, 50),
            //    scheme.DefaultFont, scheme.CommandButtonImage, scheme.CommandButtonOverImage, scheme.CommandButtonClickImage,
            //    Color.White, BorderPanelMode.Resize, scheme);
            Button saveButton = new Button();
            saveButton.Parent = this.ClientArea;
            saveButton.Location = new Point(0, 0);
            saveButton.Size = new Point(75, 50);

            saveButton.Text = "Save";
            saveButton.MouseClick += OnSave;

        }

        void OnSave(object sender, EventArgs args)
        {
            if (Save != null)
                Save(sender, EventArgs.Empty);
        }
    }
}
