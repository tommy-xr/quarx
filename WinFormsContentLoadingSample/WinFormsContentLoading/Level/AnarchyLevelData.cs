using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WinFormsContentLoading
{
    public class AnarchyLevelData
    {
        public class ModelInfoCollection : Collection<ModelInfo>
        {

        }

        ModelInfoCollection models = new ModelInfoCollection();
        public ModelInfoCollection Models
        {
            get { return models; }
        }


    }


}
