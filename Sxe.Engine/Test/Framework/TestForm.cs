//using System;
//using System.Collections.Generic;
//using System.Text;

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
//    public class TestForm : Form
//    {
//        TreeView testTreeView;
//        Label outputLabel;
//        Label errorsLabel;
//        Button runButton;
//        TextBox outputText;
//        TextBox errorsText;

//        UnitTestSuite testCases;

//        UIImage defaultImage;
//        UIImage passImage;
//        UIImage failImage;
//        UIImage namespaceImage;

//        const string defaultImagePath = "Test/test_default_icon";
//        const string passImagePath = "Test/test_pass_icon";
//        const string failImagePath = "Test/test_fail_icon";
//        const string namespaceImagePath = "Test/test_namespace_icon";

//        public TestForm(ISchemeManager schemes, ContentManager content)
//            : base(schemes)
//        {
//            this.Location = new Point(100, 100);
//            this.Size = new Point(600, 400);

//            this.Text = "SXEUnitTest";
//            this.Name = "UTF";

//            //testTreeView = new TreeView(this.ClientArea, Point.Zero, new Point(250, 320), Scheme);
//            testTreeView = new TreeView();
//            testTreeView.Parent = this.ClientArea;
//            testTreeView.Location = Point.Zero;
//            testTreeView.Size = new Point(250, 350);
            
//            //outputLabel = new Label(this.ClientArea, new Point(270, 40), new Point(32, 20), Scheme.DefaultFont, Color.Black, Scheme);
//            outputLabel = new Label();
//            outputLabel.Parent = this.ClientArea;
//            outputLabel.Location = new Point(270, 40);
//            outputLabel.Size = new Point(32, 20);
//            outputLabel.Caption = "Output:";

//            //outputText = new TextBox(this.ClientArea, new Point(270, 60), new Point(275, 120), Scheme, 100);
//            outputText = new TextBox(100);
//            outputText.Parent = this.ClientArea;
//            outputText.Location = new Point(270, 60);
//            outputText.Size = new Point(275, 120);
//            outputText.ReadOnly = true;

//            //errorsLabel = new Label(this.ClientArea, new Point(270, 180), new Point(32, 20), Scheme.DefaultFont, Color.Black, Scheme);

//            errorsLabel = new Label();
//            errorsLabel.Parent = this.ClientArea;
//            errorsLabel.Location = new Point(270, 180);
//            errorsLabel.Size = new Point(32, 20);
//            errorsLabel.Caption = "Errors & Failures:";

//            //errorsText = new TextBox(this.ClientArea, new Point(270, 200), new Point(275, 120), Scheme, 100);
//            errorsText = new TextBox(100);
//            errorsText.Parent = this.ClientArea;
//            errorsText.Location = new Point(270, 200);
//            errorsText.Size = new Point(275, 120);
//            errorsText.ReadOnly = true;

//            //runButton = new Button(this.ClientArea, new Point(270, 0), new Point(80, 40), Scheme.DefaultFont,
//            //    Scheme.CommandButtonImage, Scheme.CommandButtonOverImage, Scheme.CommandButtonClickImage, Color.Black,
//            //    BorderPanelMode.Resize, Scheme);
//            runButton = new Button();
//            runButton.Parent = this.ClientArea;
//            runButton.Location = new Point(270, 0);
//            runButton.Size = new Point(80, 40);
//            runButton.Text = "Run";

//            //TODO: Get these back...
//            defaultImage = new UIImage(content.Load<Texture2D>(defaultImagePath));
//            passImage = new UIImage(content.Load<Texture2D>(passImagePath));
//            failImage = new UIImage(content.Load<Texture2D>(failImagePath));
//            namespaceImage = new UIImage(content.Load<Texture2D>(namespaceImagePath));


//            testCases = new UnitTestSuite();

//            TreeNode testCase = new TreeNode();
//            testCase.Text = "Test Cases";
//            testCase.Expanded = true;

//            foreach (UnitTestFixtureInfo tfi in testCases.Tests)
//            {

//                TreeNode fixtureNode = new TestFixtureNode(tfi);
//                fixtureNode.Image = defaultImage;
//                fixtureNode.Text = tfi.TestType.Name;
//                fixtureNode.Expanded = true;
                
//                //Loop through and add all the test classes
//                foreach (UnitTestCaseInfo tci in tfi.TestCases)
//                {
//                    TreeNode testNode = new TestCaseNode(tci);
//                    testNode.Image = defaultImage;
//                    testNode.Text = tci.TestMethod.Name;
//                    fixtureNode.Nodes.Add(testNode);
//                }


//                TreeNode parent = GetNameSpaceNode(tfi.TestType.Namespace, testCase);
//                parent.Nodes.Add(fixtureNode);
//            }

//            testTreeView.Nodes.Add(testCase);

//            runButton.MouseClick += OnClick;
//            testTreeView.SelectionChanged += OnSelectionChanged;

//            //this.ApplyScheme(schemes.DefaultScheme);

//            //Update();
//        }

//        void OnClick(object value, EventArgs args)
//        {
//            testCases.RunAll();
//            Update();
//        }

//        void OnSelectionChanged(object value, TreeViewEventArgs args)
//        {
//            //Set the output and errors to the value of the current node
//            TreeNode node = args.Node;
//            if (node == null)
//                return;

//            if (node is TestCaseNode)
//            {
//                TestCaseNode caseNode = node as TestCaseNode;
//                outputText.Text = caseNode.TestCase.ConsoleOutput;
//                errorsText.Text = caseNode.TestCase.Errors;
                
//            }
//        }

//        TreeNode GetNameSpaceNode(string sz, TreeNode startNode)
//        {
//            string name = sz;
//            if (sz.Contains("."))
//            {
//                int index = sz.IndexOf(".");
//                name = sz.Substring(0, index);
//                sz = sz.Substring(index+1, sz.Length - index-1);
//            }

//            TreeNode nextNode = null;
//            //Loop through namespace nodes of the start node, see if one exists
//            foreach (TreeNode node in startNode.Nodes)
//            {
//                if (node.Text == name)
//                    nextNode = node;
//            }

//            //If not, create it
//            if (nextNode == null)
//            {
//                nextNode = new NamespaceNode();
//                nextNode.Image = defaultImage;
//                nextNode.Text = name;
//                nextNode.Expanded = true;
//                startNode.Nodes.Add(nextNode);
//            }

//            //If the name = sz, then there were no periods left, so return the node
//            if (name == sz)
//                return nextNode;
//            else
//                return GetNameSpaceNode(sz, nextNode);

//        }

//        /// <summary>
//        /// Recursively update the tree nodes based on test case results
//        /// Responsible for updating icons to reflect pass/failings
//        /// </summary>
//        void Update()
//        {
//            foreach (TreeNode node in testTreeView.Nodes)
//                RecursiveUpdate(node);

//            testTreeView.UpdateNodes();
//        }

//        bool RecursiveUpdate(TreeNode tn)
//        {
//            bool passed = true;
//            if (tn is TestCaseNode)
//            {
//                TestCaseNode tcn = tn as TestCaseNode;

//                //If this test failed, then obviously it didn't pass
//                if (tcn.TestCase.Result != TestResult.Pass)
//                    passed = false;
//            }
//            else
//            {
//                foreach (TreeNode node in tn.Nodes)
//                {
//                    //If any of the children test cases failed... return false
//                    if (!RecursiveUpdate(node))
//                        passed = false;
//                }

//            }

//            if (passed)
//            {
//                tn.Image = passImage;
//                return true;
//            }
//            else
//            {
//                tn.Image = failImage;
//                return false;
//            }

//        }
//    }

    

//}
