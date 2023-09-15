using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using Sxe.Engine.UI;

namespace Sxe.Engine.Utilities
{
    class PerformanceNode : TreeNode
    {
        string name;
        bool active = true;
        object targetObject;
        MethodInfo targetProperty;

        double min = double.MaxValue;
        double total = 0.0;
        double count = 0.0;
        double max = double.MinValue;

        public double Minimum { get { return min; } }
        public double Maximum { get { return max; } }
        public double Average { get { return total / count; } }

        public PerformanceNode(string inName, object inTargetObject, MethodInfo inTargetProperty)
        {
            name = inName;
            targetObject = inTargetObject;
            targetProperty = inTargetProperty;
        }

        public void Update()
        {
            if (active)
            {
                //PERF WARNING: Unboxing!!
                object value = targetProperty.Invoke(targetObject, null);
                this.Text = name + " |" + value.ToString() + "|";

                double doubleValue = double.Parse(value.ToString());
                total += doubleValue;
                count++;
                if (doubleValue < min)
                    min = doubleValue;

                if (doubleValue > max)
                    max = doubleValue;
            }

        }
    }
}
