using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.Input;

namespace WinFormsContentLoading
{
    public enum EditorInputType
    {
        ZoomIn = 0,
        ZoomOut,
        SlideLeft,
        SlideRight,
        SlideUp,
        SlideDown
    }
    public class EditorInputEventArgs : InputEventArgs
    {
        EditorInputType inputType;
        public EditorInputType EditorInputType
        {
            get { return inputType; }
            set { inputType = value; }
        }
    }
}
