using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.UI
{
    public class BaseSliderPanel : CompositePanel
    {
        int min=0;
        int max=1;

        int value;
        int valueSize=1;

        public virtual int Minimum
        {
            get { return min; }
            set 
            { 
                min = value;

                if (this.value < min)
                    this.value = min;
            }
        }

        public virtual int Maximum
        {
            get { return max; }
            set 
            { 
                max = value;
                if (this.value > max)
                    this.value = max;
            }
        }

        public virtual int Value
        {
            get { return value; }
        }

        public virtual int ValueSize
        {
            get { return valueSize; }
            set { valueSize = value; }
        }

        public virtual bool Increment()
        {
            value += ValueSize;
            if (value > Maximum)
            {
                value = Maximum;
                return false;
            }

            return true;
        }

        public virtual bool Decrement()
        {
            value -= ValueSize;
            if (value < Minimum)
            {
                value = Minimum;
                return false;
            }

            return true;
        }


        public BaseSliderPanel()
        {
        }


    }
}
