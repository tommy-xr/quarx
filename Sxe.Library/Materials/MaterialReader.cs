using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Library.Materials
{
    public class MaterialReader : ContentTypeReader<MaterialManager>
    {
        protected override MaterialManager Read(ContentReader input, MaterialManager existingInstance)
        {
            MaterialManager mm = new MaterialManager();

            mm.DefaultDiffuse = input.ReadExternalReference<Texture2D>();
            mm.DefaultNormal = input.ReadExternalReference<Texture2D>();
            mm.DefaultSpecular = input.ReadExternalReference < Texture2D>();
            mm.DefaultEffect = input.ReadExternalReference<Effect>();

            return mm;
            

            //throw new Exception("The method or operation is not implemented.");
        }
    }
}
