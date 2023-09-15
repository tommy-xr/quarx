using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

#if !XBOX

using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using EnvDTE;
using Sxe.Design.ImagePicker;

namespace Sxe.Engine.Design
{
    public class UISpriteFontTypeEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
            //return base.GetEditStyle(context);
        }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
                if (editorService != null)
                {
                    //SpriteFontPickerDropDown dropDown = (SpriteFontPickerDropDown)context.Instance;
                    SpriteFontPickerDropDown fontPicker = new SpriteFontPickerDropDown();
                    editorService.DropDownControl(fontPicker);
                }
            }

            return value;
            //return base.EditValue(context, provider, value);
        }
    }
}
#endif