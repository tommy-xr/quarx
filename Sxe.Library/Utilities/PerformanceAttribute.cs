using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Library.Utilities
{

    [AttributeUsage(AttributeTargets.Property)]
    public class SxePerformanceAttribute : Attribute
    {
        string counterName;
        public string Name
        {
            get { return counterName; }
        }

        public SxePerformanceAttribute(string name)
        {
            counterName = name;
        }
    }
}
