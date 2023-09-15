//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Reflection;

//using Sxe.Engine.UI;
//using Sxe.Engine.Input;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;

//using Sxe.Library.Utilities;

//namespace Sxe.Engine.Utilities
//{
//    /// <summary>
//    /// Form for handling all the test cases
//    /// </summary>
//    public class PerformanceForm : Form
//    {
//        TreeView testTreeView;
//        Button runButton;

//        Label minLabel;
//        Label maxLabel;
//        Label averageLabel;

//        UIImage defaultImage;
//        UIImage passImage;
//        UIImage failImage;
//        UIImage namespaceImage;

//        const string defaultImagePath = "Test/test_default_icon";
//        const string passImagePath = "Test/test_pass_icon";
//        const string failImagePath = "Test/test_fail_icon";
//        const string namespaceImagePath = "Test/test_namespace_icon";

//        public PerformanceForm(ISchemeManager schemes, ContentManager content)
//            : base(schemes)
//        {
//            this.Location = new Point(100, 100);
//            this.Size = new Point(600, 400);

//            this.Text = "Performance Monitor";
//            this.Name = "PerformanceMonitor";

//            //testTreeView = new TreeView(this.ClientArea, Point.Zero, new Point(250, 320), Scheme);
//            testTreeView = new TreeView();
//            testTreeView.Parent = this.ClientArea;
//            testTreeView.Location = Point.Zero;
//            testTreeView.Size = new Point(250, 350);
            
//            //runButton = new Button(this.ClientArea, new Point(270, 0), new Point(80, 40), Scheme.DefaultFont,
//            //    Scheme.CommandButtonImage, Scheme.CommandButtonOverImage, Scheme.CommandButtonClickImage, Color.Black,
//            //    BorderPanelMode.Resize, Scheme);
//            runButton = new Button();
//            runButton.Parent = this.ClientArea;
//            runButton.Location = new Point(270, 0);
//            runButton.Size = new Point(80, 40);
//            runButton.Text = "Run";


//            minLabel = new Label();
//            minLabel.Parent = this.ClientArea;
//            minLabel.Location = new Point(270, 50);
//            minLabel.Size = new Point(100, 25);
//            minLabel.FontColor = Color.Black;

//            maxLabel = new Label();
//            maxLabel.Parent = this.ClientArea;
//            maxLabel.Location = new Point(270, 100);
//            maxLabel.Size = new Point(100, 25);
//            maxLabel.FontColor = Color.Black;

//            averageLabel = new Label();
//            averageLabel.Parent = this.ClientArea;
//            averageLabel.Location = new Point(270, 150);
//            averageLabel.Size = new Point(100, 25);
//            averageLabel.FontColor = Color.Black;
            
//            //TODO: Get these back...
//            defaultImage = new UIImage(content.Load<Texture2D>(defaultImagePath));
//            passImage = new UIImage(content.Load<Texture2D>(passImagePath));
//            failImage = new UIImage(content.Load<Texture2D>(failImagePath));
//            namespaceImage = new UIImage(content.Load<Texture2D>(namespaceImagePath));



//            TreeNode testCase = new TreeNode();
//            testCase.Text = "Performance Counter";
//            testCase.Expanded = true;

//            //foreach (UnitTestFixtureInfo tfi in testCases.Tests)
//            //{

//            //    TreeNode fixtureNode = new TestFixtureNode(tfi);
//            //    fixtureNode.Image = defaultImage;
//            //    fixtureNode.Text = tfi.TestType.Name;
//            //    fixtureNode.Expanded = true;
                
//            //    //Loop through and add all the test classes
//            //    foreach (UnitTestCaseInfo tci in tfi.TestCases)
//            //    {
//            //        TreeNode testNode = new TestCaseNode(tci);
//            //        testNode.Image = defaultImage;
//            //        testNode.Text = tci.TestMethod.Name;
//            //        fixtureNode.Nodes.Add(testNode);
//            //    }


//            //    TreeNode parent = GetNameSpaceNode(tfi.TestType.Namespace, testCase);
//            //    parent.Nodes.Add(fixtureNode);
//            //}

//            testTreeView.Nodes.Add(testCase);
//            testTreeView.MouseClick += OnClick;

//            //this.ApplyScheme(schemes.DefaultScheme);

//            //Update();
//        }

//        /// <summary>
//        /// Adds an object to be tracked using the performance monitor
//        /// </summary>
//        /// <param name="performanceObject"></param>
//        public void AddPerformanceCounter(object performanceObject)
//        {
//            TreeNode objectCounter = new TreeNode();
//            objectCounter.Text = performanceObject.ToString();

//            //Scan the object for PerformanceAttributes
//            Type t = performanceObject.GetType();
        
//            //Loop through all the properties
//            foreach (PropertyInfo property in t.GetProperties())
//            {
//                //See if the property has our attribute
//                foreach (Attribute attr in property.GetCustomAttributes(true))
//                {
//                    SxePerformanceAttribute perfAttr = attr as SxePerformanceAttribute;
//                    if (perfAttr != null)
//                    {
//                        PerformanceNode performanceNode = new PerformanceNode(perfAttr.Name, performanceObject, property.GetGetMethod());
//                        objectCounter.Nodes.Add(performanceNode);
//                    }
//                }
//            }

//            testTreeView.Nodes.Add(objectCounter);

//        }

//        public override void Update(GameTime time)
//        {
//            foreach (TreeNode tn in testTreeView.Nodes)
//                RecursiveUpdate(tn);

 

//            base.Update(time);
//        }

//        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
//        {
//            base.Paint(sb, positionOffset, scale);
//        }

//        void RecursiveUpdate(TreeNode tn)
//        {
//            foreach (TreeNode child in tn.Nodes)
//                RecursiveUpdate(child);

//            PerformanceNode perf = tn as PerformanceNode;
//            if (perf != null)
//                perf.Update();
//        }

//        void OnClick(object sender, EventArgs args)
//        {
//            if (testTreeView.Selected != null)
//            {
//                PerformanceNode node = testTreeView.Selected as PerformanceNode;
//                UpdateLabels(node);
//            }
//        }
//        void UpdateLabels(PerformanceNode node)
//        {
//            if (node == null)
//            {
//                maxLabel.Caption = "Min: -";
//                minLabel.Caption = "Max: -";
//                averageLabel.Caption = "Average: -";
//            }
//            else
//            {
//                minLabel.Caption = "Min: " + node.Minimum.ToString();
//                maxLabel.Caption = "Max: " + node.Maximum.ToString();
//                averageLabel.Caption = "Average: " + node.Average.ToString();
//            }
//        }

//    }

    

//}
