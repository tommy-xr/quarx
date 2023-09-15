using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

#if !XBOX

namespace Sxe.Design.ImagePicker
{
    public partial class ImagePickerForm : Form
    {
        private Dictionary<string, PathNode> stringToNode = new Dictionary<string, PathNode>();

        public ImagePickerForm()
        {
            InitializeComponent();
            this.texturePickerControl1.ScrollParamsChanged += this.OnTexturePickerScrollChange;
            //Application.Idle += delegate { this.texturePickerControl1.Invalidate(); };
        }

        public void SetValues(ArrayList values)
        {

            values.Sort();
            
            //First, we'll create a node for "All" values
            PathNode allNode = new PathNode();
            allNode.Text = "<All>";


            //Loop through each content path
            foreach (string sz in values)
            {
                //Split up the directories
                string directory = Path.GetDirectoryName(sz);
                string file = Path.GetFileName(sz);

                if (directory == String.Empty)
                {
                    allNode.ContentNames.Add(file);
                }
                else
                {

                    string[] dirs = directory.Split(Path.DirectorySeparatorChar, '\\', '/');
                    string start = dirs[0];


                    PathNode node = GetNodeFromName(dirs[0]);
                    PathNode startNode = node;

                    for (int i = 1; i < dirs.Length; i++)
                    {
                        start += Path.DirectorySeparatorChar + dirs[i];
                        PathNode oldNode = node;
                        node = GetNodeFromName(start);
                        oldNode.Nodes.Add(node);
                    }

                    node.ContentNames.Add(sz);

                    //node.Text = sz;
                    if (!allNode.Nodes.Contains(startNode))
                        allNode.Nodes.Add(startNode);
                }

            }

            this.treeView1.Nodes.Add(allNode);

            //StringCollection initialValues = new StringCollection();
            //foreach (TreeNode node in treeView1.Nodes)
            //{
            //    PathNode pathNode = node as PathNode;
            //    if (pathNode != null)
            //    {
            //        if (pathNode.IsLeaf)
            //            initialValues.Add(pathNode.FullPath);
            //    }
            //}

            //this.texturePickerControl1.SetImages(initialValues);
        }

        private void UpdatePickerControl(TreeNode tn)
        {
            StringCollection values = new StringCollection();
            RecursiveAdd(tn, values);

            this.texturePickerControl1.SetImages(values);
        }

        private void RecursiveAdd(TreeNode tn, StringCollection values)
        {
            PathNode pathNode = tn as PathNode;
            if (pathNode != null)
            {
                foreach (string sz in pathNode.ContentNames)
                    values.Add(sz);

                foreach (TreeNode node in pathNode.Nodes)
                    this.RecursiveAdd(node, values);
            }
        }




        private PathNode GetNodeFromName(string name)
        {
            if (!stringToNode.ContainsKey(name))
            {
                PathNode node = new PathNode();
                node.FullPath = name;
                node.Text = Path.GetFileName(name);
                stringToNode.Add(name, node);
            }

            return stringToNode[name];
        }

        public void SetContentDirectory(string directory)
        {
            texturePickerControl1.SetContentDirectory(directory);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            //this.Close();
        }

        protected void OnTexturePickerScrollChange(object sender, EventArgs e)
        {
            this.vScrollBar1.LargeChange = this.texturePickerControl1.LargeChange;
            this.vScrollBar1.SmallChange = this.texturePickerControl1.SmallChange;
            this.vScrollBar1.Minimum = this.texturePickerControl1.MinScroll;
            this.vScrollBar1.Maximum = this.texturePickerControl1.MaxScroll;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.UpdatePickerControl(e.Node);
        }

        private void ImagePickerForm_Load(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.texturePickerControl1.Scroll(e.NewValue);
        }
    }
}

#endif