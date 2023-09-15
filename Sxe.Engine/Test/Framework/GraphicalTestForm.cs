//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Reflection;

//using Sxe.Engine.UI;
//using Sxe.Engine.Input;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;

//namespace Sxe.Engine.Test.Framework
//{
//    /// <summary>
//    /// Form for handling all the test cases
//    /// </summary>
//    public class GraphicalTestForm : Form
//    {
//        TreeView testTreeView;
//        Button runButton;

//        IGameScreenService services;

//        GraphicalTestSuite testCases;

//        //TODO: Is this really where the content manager should be?
//        ContentManager content;

//        public GraphicalTestForm(IServiceProvider inServices, IGameScreenService gameServices)
//            : base(gameServices.Schemes)
 
//        {
//            content = new ContentManager(inServices);
//            this.Location = new Point(100, 100);
//            this.Size = new Point(600, 400);

//            services = gameServices;

//            testCases = new GraphicalTestSuite();

//            this.Text = "GraphicalUnitTests";
//            this.Name = "GTF";
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

//            runButton.MouseClick += OnClick;
//            testTreeView.SelectionChanged += OnSelectionChanged;

//            PopulateTestCases();

//            //this.ApplyScheme(gameServices.Schemes.DefaultScheme);

//            //Update();
//        }

//        void PopulateTestCases()
//        {
//            //Add a top level test case node
//            TreeNode baseNode = new TreeNode();
//            baseNode.Text = "Graphical Tests";

//            //Create a dictionary to hold category names
//            Dictionary<string, TreeNode> categoryToNode = new Dictionary<string, TreeNode>();

//            //Loop through all test cases in suite, and create nodes for them
//            foreach (GraphicalTestInfo info in testCases.Tests)
//            {
//                GraphicalTestNode node = new GraphicalTestNode(info);
//                node.Text = info.TestType.Name;
//                //Find the parent of this node
//                TreeNode parent = GetCategoryNode(info.Category, categoryToNode);
//                parent.Nodes.Add(node);
//            }

//            //Now, loop through and add all the categorys to the parent node
//            foreach (TreeNode node in categoryToNode.Values)
//            {
//                baseNode.Nodes.Add(node);
//            }

//            //Finally, add the top level node to the tree view
//            testTreeView.Nodes.Add(baseNode);


//        }

//        TreeNode GetCategoryNode(string name, Dictionary<string, TreeNode> nodeDictionary)
//        {
//            if (!nodeDictionary.ContainsKey(name))
//            {
//                GraphicalCategoryNode node = new GraphicalCategoryNode();
//                node.Text = name;
//                nodeDictionary.Add(name, node);
//            }

//            return nodeDictionary[name];
//        }



//        void OnClick(object value, EventArgs args)
//        {
//            if (testTreeView.Selected != null)
//            {
//                if (testTreeView.Selected is GraphicalTestNode)
//                {
//                    GraphicalTestNode testNode = testTreeView.Selected as GraphicalTestNode;
//                    testNode.TestInfo.Run(services, content );
//                }
//            }
//        }

//        void OnSelectionChanged(object value, TreeViewEventArgs args)
//        {

//        }

   

//    }

    

//}
