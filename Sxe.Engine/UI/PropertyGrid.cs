//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Reflection;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace Sxe.Engine.UI
//{
//    public class TestClass
//    {
//        public int Int1 { get { return 1; } }
//        public int Int2 { get { return 2; } }
//        public float Float1 { get { return 3.0f; } }
//        public Vector3 Vector3 { get { return Vector3.Up; } }
//    }

//    public class PropertyGrid : ScrollablePanel
//    {
//        Label[] titleLabels;
//        MultiLineLabel[] labels;
//        List<string> propertyNames;
//        List<MethodInfo> propertyValues;
//        float split = 0.5f;


//        object selectedObject;

//        public object SelectedObject
//        {
//            get { return selectedObject; }
//            set { selectedObject = value; LoadObject(); }
//        }


//        public PropertyGrid(IPanelContainer parent, Point location, Point size)
//        {
//            propertyNames = new List<string>();
//            propertyValues = new List<MethodInfo>();

//            titleLabels = new Label[2];
//            labels = new MultiLineLabel[2];

//            //titleLabels[0] = new Label(this, new Point(0, 0), new Point((int)(split * size.X), 20),
//            //    Scheme.DefaultFont, Scheme.DefaultFontColor, scheme);
//            titleLabels[0] = new Label();
//            titleLabels[0].Parent = this;
//            titleLabels[0].Location = new Point(0, 0);
//            titleLabels[0].Size = new Point((int)(split * size.X), 20);

//            //titleLabels[1] = new Label(this, new Point((int)(split * size.X), 0), 
//            //    new Point(size.X - (int)(split * size.X), 20),
//            //    Scheme.DefaultFont, Scheme.DefaultFontColor, scheme);
//            titleLabels[1] = new Label();
//            titleLabels[1].Parent = this;
//            titleLabels[1].Location = new Point((int)(split * size.X), 0);
//            titleLabels[1].Size = new Point(size.X - (int)(split * size.X), 20);

//            titleLabels[0].Caption = "Name";
//            titleLabels[1].Caption = "Value";

//            labels[0] = new MultiLineLabel(this, new Point(0, 20), new Point((int)(split * size.X), size.Y - 20));
//            labels[1] = new MultiLineLabel(this, new Point((int)(split * size.X), 20), 
//                new Point(size.X - (int)(split * size.X), size.Y - 20));

//            for (int i = 0; i < labels[0].Count; i++)
//            {
//                labels[0][i].Caption = i.ToString();
//                labels[1][i].Caption = i.ToString();
//            }

//            DisplaySize = new Point(1, labels[0].Count);
//            FullSize = new Point(1, 0);

//            ResetSize();

//            SelectedObject = new TestClass();

            
            


//        }

//        public override void Update(GameTime time)
//        {
//            RefreshLabels();
//            base.Update(time);
//        }

//        /// <summary>
//        /// Called when split is changed or other stuff
//        /// </summary>
//        void ResetSize()
//        {
//            int height = labels[0].FontHeight;
//            titleLabels[0].Size = new Point(titleLabels[0].Size.X, height);
//            titleLabels[1].Size = new Point(titleLabels[1].Size.X, height);

//            labels[0].Size = new Point(labels[0].Size.X, Size.Y - height);
//            labels[1].Size = new Point(labels[1].Size.X, Size.Y - height);
//        }

//        /// <summary>
//        /// Loads a new object, gets properties through reflection, ya buddy
//        /// </summary>
//        void LoadObject()
//        {
//            Type t = selectedObject.GetType();

//            propertyNames.Clear();
//            propertyValues.Clear();

//            foreach (PropertyInfo pi in t.GetProperties())
//            {
//                propertyNames.Add(pi.Name);
//                propertyValues.Add(pi.GetGetMethod());
//            }

//            FullSize = new Point(1, propertyNames.Count);
//            RefreshLabels();
//        }

//        /// <summary>
//        /// Update the contents of the labels
//        /// </summary>
//        void RefreshLabels()
//        {
//            for (int i = DisplayCoordinates.Y; i < DisplayCoordinates.Y + DisplaySize.Y; i++)
//            {
//                int normalizedI = i - DisplayCoordinates.Y;

//                if (i < propertyNames.Count)
//                {
//                    labels[0][normalizedI].Caption = propertyNames[i];

//                    object value = propertyValues[i].Invoke(selectedObject, null);
//                    labels[1][normalizedI].Caption = value.ToString();

//                }
//                else
//                {
//                    labels[0][normalizedI].Caption = "";
//                    labels[1][normalizedI].Caption = "";
//                }
//            }
//        }
//    }
//}
