//using System;
//using System.Collections.Generic;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//using Sxe.Engine.Input;

//namespace Sxe.Engine.UI
//{
//    public class TreeViewEventArgs : EventArgs
//    {
//        TreeNode node;

//        public TreeNode Node
//        {
//            get { return node; }
//            set { node = value; }
//        }
//    }

   


//    /// <summary>
//    /// A tree view class
//    /// </summary>
//    public class TreeView : ScrollablePanel
//    {
//        enum NodeDisplayMode
//        {
//            Top,
//            Middle,
//            Bottom
//        }

//        class TreeViewInfo
//        {
//            public TreeNode node;
//            public int indention;
//            public NodeDisplayMode mode;
//        }


//        MultiLineLabel labels;
//        Panel[] brackets;
//        Panel[] icons;
//        Panel[] expandIcons;
//        TreeNode selectedNode;
//        TreeViewEventArgs treeArgs = new TreeViewEventArgs();
//        EventList<TreeNode> nodes;
//        TreeViewInfo[] info;

//        //Scheme data
//        Point treeViewBracketSize = new Point(16, 16);
//        UIImage middleBracket;
//        UIImage topBracket;
//        UIImage bottomBracket;
//        UIImage minus;
//        UIImage plus;
//        Point expandIconOffset;
//        Point expandIconSize;
        
//        public event EventHandler<TreeViewEventArgs> SelectionChanged;

//        public EventList<TreeNode> Nodes
//        {
//            get { return nodes; }
//        }

//        public TreeNode Selected
//        {
//            get { return selectedNode; }
//            set
//            {
//                selectedNode = value;

//                if (SelectionChanged != null)
//                {
//                    treeArgs.Node = selectedNode;
//                    SelectionChanged(this, treeArgs);
//                }

//            }
//        }




//        //public TreeView(IPanelContainer parent, Point position, Point size, IScheme scheme)
//        //    : base(parent, position, size, scheme.White, scheme)
//        public TreeView()
//        {
//            this.BackColor = Color.Ivory;
//            nodes = new EventList<TreeNode>();

//            RecreateControl();

//            UpdateNodes();

//            this.MouseClick += OnClick;
//            this.Scroll += OnScroll;
//            this.SizeChanged += OnSizeChanged;
//            this.nodes.ListModified += OnListChanged;
//        }

//        void RecreateControl()
//        {

//            if (labels != null)
//            {
//                labels.Parent = null;
//            }
//            if (brackets != null)
//            {
//                for (int i = 0; i < brackets.Length; i++)
//                    brackets[i].Parent = null;
//            }
//            if (icons != null)
//            {
//                for (int i = 0; i < icons.Length; i++)
//                {
//                    icons[i].Parent = null;
//                    expandIcons[i].Parent = null;
//                }
//            }

//            //labels = new MultiLineLabel(this, new Point(0, 0), size, scheme, scheme.TreeViewBracketSize.Y);
//            labels = new MultiLineLabel(this, new Point(0, 0), Size);

//            //labels = new MultiLineLabel(


//            info = new TreeViewInfo[labels.Count];
//            for (int i = 0; i < info.Length; i++)
//                info[i] = new TreeViewInfo();


//            //int yOffset = (labels[0].Size.Y - scheme.TreeViewBracketSize.Y) / 2;
//            int yOffset = (labels[0].Size.Y - treeViewBracketSize.Y) / 2;

//            brackets = new Panel[labels.Count];
//            for (int i = 0; i < brackets.Length; i++)
//            {

//                //brackets[i] = new Panel(this, new Point(0, labels[i].Location.Y), scheme.TreeViewBracketSize, scheme.TreeViewMiddleBracket);
//                brackets[i] = new Panel();
//                brackets[i].Parent = this;
//                brackets[i].Location = new Point(0, labels[i].Location.Y);
//                brackets[i].Size = treeViewBracketSize;
//            }

//            icons = new Panel[labels.Count];
//            expandIcons = new Panel[labels.Count];
//            for (int i = 0; i < icons.Length; i++)
//            {
//                //icons[i] = new Panel(this, new Point(0, labels[i].Location.Y + yOffset), scheme.TreeViewBracketSize, null, scheme);
//                icons[i] = new Panel();
//                icons[i].Parent = this;
//                icons[i].Location = new Point(0, labels[i].Location.Y + yOffset);
//                icons[i].Size = treeViewBracketSize;

//                //expandIcons[i] = new Panel(this, new Point(0, labels[i].Location.Y + yOffset), scheme.TreeViewExpandIconSize, null, scheme);
//                expandIcons[i] = new Panel();
//                expandIcons[i].Parent = this;
//                expandIcons[i].Location = new Point(0, labels[i].Location.Y + yOffset);
//                expandIcons[i].Size = treeViewBracketSize;
//            }


//            this.DisplaySize = new Point(1, labels.Count);
//            this.FullSize = new Point(1, 0);



//            UpdateNodes();
//        }

//        void OnSizeChanged(object sender, EventArgs args)
//        {
//            RecreateControl();
//        }

//        public override void ApplyScheme(IScheme scheme)
//        {
//            base.ApplyScheme(scheme);
        
//            //Grab the UI images and everything
//             treeViewBracketSize = new Point(16, 16);
//             middleBracket = scheme.GetImage("treeview_middle_bracket");
//             topBracket = scheme.GetImage("treeview_top_bracket");
//             bottomBracket = scheme.GetImage("treeview_bottom_bracket");
//             minus = scheme.GetImage("treeview_minus");
//             plus = scheme.GetImage("treeview_plus");
//             expandIconOffset = scheme.GetPoint("treeview_expand_icon_offset");
//             expandIconSize = scheme.GetPoint("treeview_expand_icon_size");

//             this.Image = new UIImage(scheme.GetTexture("blank"));

//             //RecreateControl();
        
//        }

//        void OnListChanged(object value, EventArgs args)
//        {
//            UpdateNodes();
//        }

//        void OnScroll(object value, EventArgs args)
//        {
//            UpdateNodes();
//        }

//        void OnClick(object value, MouseEventArgs args)
//        {
//            //Figure out which label this corresponds to
//            int yLabel = ((args.Position.Y - this.AbsoluteLocation.Y) / (this.labels.Size.Y / this.labels.Count));
//            if (yLabel >= labels.Count)
//                yLabel = labels.Count - 1;

//            if (info[yLabel].node != null)
//            {
//                if (!labels[yLabel].IsPointInside(args.Position))
//                {
//                    //Only allowing expanding / unexpanding if the node has children
//                    if(info[yLabel].node.Nodes.Count > 0)
//                    info[yLabel].node.Expanded = !info[yLabel].node.Expanded;
//                }
//                else
//                {
//                    Select(yLabel);
//                }
//            }
//            else
//            {
//                UnSelect();
//            }
                

//            UpdateNodes();



//        }

//        void Select(int index)
//        {
//            if (index >= 0)
//                Selected = info[index].node;
//            else
//                Selected = null;
//        }

//        void UnSelect()
//        {
//            Select(-1);   
//        }




//        /// <summary>
//        /// Takes care of the book keeping involved with updating the nodes
//        /// </summary>
//        public void UpdateNodes()
//        {
//            labels.Clear();
//            ClearInfo();

//            int start = 0;
//            foreach (TreeNode tn in nodes)
//            {
//                NodeDisplayMode mode = NodeDisplayMode.Middle;
//                if (start == 0)
//                    mode = NodeDisplayMode.Top;
//                else if (tn == nodes[nodes.Count - 1] || tn.Expanded)
//                    mode = NodeDisplayMode.Bottom;

//                RecursiveUpdate(tn, ref start, 0, mode);
//            }
//            FullSize = new Point(FullSize.X, start);

//            UpdateLabels();
//            FireDisplayChanged();

//            Invalidate();
//        }

//        /// <summary>
//        /// Recursively update the tree nodes. Tracks the indention level, and the index,
//        /// so we know which tree nodes to draw
//        /// </summary>
//        void RecursiveUpdate(TreeNode tn, ref int index, int indention, NodeDisplayMode nodeMode)
//        {
//            //If this tree node is viewable, add it to the viewable list
//            if (index >= this.DisplayCoordinates.Y && index < this.DisplayCoordinates.Y + DisplaySize.Y)
//            {
//                info[index - DisplayCoordinates.Y].node = tn;
//                info[index - DisplayCoordinates.Y].indention = indention;
//                info[index - DisplayCoordinates.Y].mode = nodeMode;
//            }

//            index++;

//            if (tn.Expanded)
//            {
//                foreach (TreeNode child in tn.Nodes)
//                {
//                    NodeDisplayMode mode = NodeDisplayMode.Middle;
//                    if (child == tn.Nodes[tn.Nodes.Count - 1])
//                        mode = NodeDisplayMode.Bottom;

//                    RecursiveUpdate(child, ref index, indention + 1, mode);
//                }
//            }

//        }

//        void ClearInfo()
//        {
//            for (int i = 0; i < info.Length; i++)
//                info[i].node = null;
//        }

//        void UpdateLabels()
//        {
//            for (int i = 0; i < info.Length; i++)
//            {
//                if (info[i].node != null)
//                {
//                    int start = info[i].indention * this.treeViewBracketSize.X;

//                    //Display proper bracket icon & indent as necessary
//                    brackets[i].Image = middleBracket;
//                    if (info[i].mode == NodeDisplayMode.Bottom)
//                        brackets[i].Image = bottomBracket;
//                    else if (info[i].mode == NodeDisplayMode.Top)
//                        brackets[i].Image = topBracket;

//                    brackets[i].Location = new Point(start, brackets[i].Location.Y);

//                    //Display an expand/collapse icon if the node has children
//                    if (info[i].node.Nodes.Count > 0)
//                    {
//                        if (info[i].node.Expanded)
//                            expandIcons[i].Image = minus;
//                        else
//                            expandIcons[i].Image = plus;
//                        expandIcons[i].Location = new Point(
//                            brackets[i].Location.X + expandIconOffset.X,
//                            brackets[i].Location.Y + expandIconOffset.Y);
//                    }
//                    else
//                        expandIcons[i].Image = null;

//                    if (info[i].node.Image != null)
//                    {
//                        start += this.treeViewBracketSize.X;
//                        icons[i].Image = info[i].node.Image;
//                        icons[i].Location = new Point(start, icons[i].Location.Y);
//                    }
//                    else
//                        icons[i].Image = null;

//                    start += this.treeViewBracketSize.X;

                    
//                    labels[i].Caption = info[i].node.Text;
//                    labels[i].Location = new Point(start, labels[i].Location.Y);
//                    labels[i].Size = new Point(this.Size.X - start, labels[i].Size.Y);

//                    if (info[i].node == selectedNode)
//                        labels[i].SelectAll();
//                    else
//                        labels[i].Unselect();
//                }
//                else
//                {
//                    brackets[i].Image = null;
//                    icons[i].Image = null;
//                    expandIcons[i].Image = null;
//                }
//            }
//        }


//    }
//}
