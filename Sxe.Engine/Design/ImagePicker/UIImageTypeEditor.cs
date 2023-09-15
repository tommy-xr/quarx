using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

#if !XBOX

using System.Drawing.Design;
using System.Windows.Forms;
using EnvDTE;
using Sxe.Design.ImagePicker;

namespace Sxe.Design
{
    public class UIImageTypeEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
            //return base.GetEditStyle(context);
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            ImagePickerForm form = new ImagePickerForm();

            //Lets get a list of content names
            StringCollection allowedProcessors = new StringCollection();
            allowedProcessors.Add("TextureProcessor");

            form.SetContentDirectory(DesignerUtilities.GetBuiltContentPath(context));

            ArrayList values = DesignerUtilities.GetContentItems(context, allowedProcessors, null);
            form.SetValues(values);


            if (form.ShowDialog() == DialogResult.OK)
            {
                return "Content\\1st";
            }
            else
            {
                return value; 
            }


            //return base.EditValue(context, provider, value);
        }
    }
}

#endif
