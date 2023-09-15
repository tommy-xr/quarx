using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Sxe.Library.Materials;

namespace SXEMaterialManager
{
    public partial class MainForm : Form, ILogService
    {

        static ServiceContainer services = new ServiceContainer();
        public static ServiceContainer Services
        {
            get { return services; }
        }

        MaterialCollectionEditor materials;
        

        public MainForm()
        {
            services.AddService<ILogService>(this);
            InitializeComponent();
            
        }

        public void Print(string sz)
        {
            this.textBox1.Text = this.textBox1.Text + Environment.NewLine + sz;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Add("test1");
            treeView1.Nodes.Add("test2");

            TreeNode n = new TreeNode("hey");
            n.Nodes.Add("hey2");
            treeView1.Nodes.Add(n);
            DragEventHandler di;

            treeView1.DragEnter += OnDragEnter;
            treeView1.DragDrop += OnDragDrop;
            treeView1.ItemDrag += OnItemDrag;
            propertyGrid1.SelectedObject = null;
        }

        void OnItemDrag(object o, ItemDragEventArgs arg)
        {
            DoDragDrop(arg.Item, DragDropEffects.Move);
        }
        void OnDragDrop(object o, DragEventArgs dea)
        {
            if(materials == null)
                return;

            string[] s = (string[])dea.Data.GetData(DataFormats.FileDrop, false);

            //Loop through all directories
            for (int i = 0; i < s.Length; i++)
                materials.AddDirectory(s[i], -1, true);

        }
        void OnDragEnter(object o, DragEventArgs dea)
        {
            dea.Effect = DragDropEffects.Move;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (materials == null)
                return;

            List<MaterialBrowserInfo> mbi = new List<MaterialBrowserInfo>();
   
            //Loop through all selected nodes, and add every single node + path
            AddNodes(e.Node, mbi);

 
            textureBrowserControl1.SetMaterials(mbi.ToArray(), materials.CurrentDirectory);

            //If the selected node is a material node, let us change its properties:
            if (e.Node is MaterialTreeNode)
            {
                MaterialTreeNode matNode = e.Node as MaterialTreeNode;
                propertyGrid1.SelectedObject = materials.EditorMaterials[matNode.MaterialIndex];
            }
            
        }

        void AddNodes(TreeNode tn, List<MaterialBrowserInfo> mbi)
        {
            //Loop through and add all children
            foreach (TreeNode t in tn.Nodes)
            {
                AddNodes(t, mbi);
            }

            if (tn is MaterialTreeNode)
            {
                MaterialTreeNode matNode = tn as MaterialTreeNode;
                if (matNode.MaterialIndex >= 0)
                {
                    mbi.Add(new MaterialBrowserInfo(materials.EditorMaterials[matNode.MaterialIndex].DiffuseName, matNode.MaterialIndex));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();


        }

        private void textureBrowserControl1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Click "New"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Have the user choose a directory
            folderBrowserDialog1.ShowDialog();

            string path = folderBrowserDialog1.SelectedPath;

            materials = MaterialCollectionEditor.CreateNew(path, OnMaterialsUpdated, services);
            materials.OnUpdate += OnMaterialsUpdated;

            //And create a new material instance there


        }

        /// <summary>
        /// Click "Save"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (materials != null)
                materials.Save("test_xml.mat");
        }

        /// <summary>
        /// Click "open"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;

            materials = MaterialCollectionEditor.LoadFromFile(path, OnMaterialsUpdated, services);
           

        }

        /// <summary>
        /// Refreshes the tree view control
        /// </summary>
        void RefreshTreeView(MaterialCollectionEditor inMaterials)
        {
          
            //Clear tree view
            treeView1.Nodes.Clear();

            //Create a temporary dictionary to index from category to node
            Dictionary<MaterialCategory, TreeNode> dictCatToNode = new Dictionary<MaterialCategory, TreeNode>();

            //Loop through and add all categories to the dictionary, and create tree nodes for each
            for (int i = 0; i < inMaterials.Categories.Count; i++)
            {
                MaterialCategory cat = inMaterials.Categories[i];
                TreeNode tn = new TreeNode(cat.Name);
                dictCatToNode.Add(cat, tn);
            }

            //Do a second pass to actually add the tree nodes, and make them children of their parents
            for(int i = 0; i < inMaterials.Categories.Count; i++)
            {
                MaterialCategory cat = inMaterials.Categories[i];
                TreeNode tn = dictCatToNode[cat];

                //Do we have a parent?
                if (cat.Parent != -1)
                {
                    MaterialCategory catParent = inMaterials.Categories[cat.Parent];
                    //Find the treenode and become a child of that
                    TreeNode parent = dictCatToNode[catParent];
                    parent.Nodes.Add(tn);
                }
                else
                {
                    treeView1.Nodes.Add(tn);
                }
            }
            
            //Finally, loop through all materials and add them
            for (int i = 0; i < inMaterials.EditorMaterials.Count; i++)
            {
                Material m = inMaterials.EditorMaterials[i];
                MaterialTreeNode mtn = new MaterialTreeNode(m.MaterialName, i);

                //See if this has a parent
                if (m.categoryIndex != -1)
                {
                    MaterialCategory catParent = inMaterials.Categories[m.categoryIndex];
                    TreeNode parent = dictCatToNode[catParent];
                    parent.Nodes.Add(mtn);
                }
                else
                {
                    treeView1.Nodes.Add(mtn);
                }
            }


        }

        /// <summary>
        /// Called when we need to update the tree view
        /// </summary>
        /// <param name="o"></param>
        void OnMaterialsUpdated(object o)
        {
            RefreshTreeView(o as MaterialCollectionEditor);
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void setAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
  
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }


    }
}