using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WinFormsContentLoading
{
    public class SelectionService : ISelectionService
    {
        ArrayList selectedObjects = new ArrayList();

        object[] ISelectionService.GetSelectedObjects()
        {
            return selectedObjects.ToArray();
        }

        void ISelectionService.Add(object addObject)
        {
            selectedObjects.Add(addObject);
            FireSelectionChangedEvent();
        }

        void ISelectionService.Remove(object removeObject)
        {
            selectedObjects.Remove(removeObject);
            FireSelectionChangedEvent();
        }

        void ISelectionService.Clear()
        {
            selectedObjects.Clear();
            FireSelectionChangedEvent();
        }

        void FireSelectionChangedEvent()
        {
            if (SelectionChanged != null)
                SelectionChanged(null, EventArgs.Empty);
        }

        public event EventHandler SelectionChanged;


    }
}
