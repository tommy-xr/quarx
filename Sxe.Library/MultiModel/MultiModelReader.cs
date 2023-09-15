using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Library.MultiModel
{
    public class MultiModelReader : ContentTypeReader<MultiModel>
    {
        protected override MultiModel Read(ContentReader input, MultiModel existingInstance)
        {
            MultiModel model = new MultiModel(input);
            return model;
            

            //throw new Exception("The method or operation is not implemented.");
        }
    }
}
