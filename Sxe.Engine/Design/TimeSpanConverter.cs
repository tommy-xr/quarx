using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;

namespace Sxe.Design
{
    class TimeSpanConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            TimeSpan span = (TimeSpan)value;
            if (span != null)
                return span.TotalSeconds.ToString();

            return null;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
           


            if (value.GetType() == typeof(string))
            {
                float floatVal = float.Parse((string)value);

                return TimeSpan.FromSeconds(floatVal);
            }



            return base.ConvertFrom(context, culture, value);

            //return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {


            return base.CanConvertTo(context, destinationType);
        }
    }
}
