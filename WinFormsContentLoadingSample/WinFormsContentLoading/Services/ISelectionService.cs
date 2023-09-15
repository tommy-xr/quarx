using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsContentLoading
{
    public interface ISelectionService
    {
        object[] GetSelectedObjects();
        void Clear();
        void Add(object addObject);
        void Remove(object removeObject);

        event EventHandler SelectionChanged;

    }
}
