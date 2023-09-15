using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;


using System.ComponentModel;
using EnvDTE;

//using Microsoft.Build.BuildEngine;

namespace Sxe.Design
{
    public class BaseContentNameConverter : TypeConverter
    {
        private ArrayList values;



        public BaseContentNameConverter()
        {
            values = new ArrayList();
        }

        protected virtual StringCollection AllowedProcessors
        {
            get { return null; }
        }

        protected virtual StringCollection AllowedImporters
        {
            get { return null; }
        }

        // Indicates this converter provides a list of standard values.
        public override sealed bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        // Returns a StandardValuesCollection of standard value objects.
        public override sealed System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ArrayList values = DesignerUtilities.GetContentItems(context, AllowedProcessors, AllowedImporters);
            if (values == null)
                return null;

            return new StandardValuesCollection(values);
        }








    }

}
