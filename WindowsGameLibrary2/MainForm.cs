#region File Description
//-----------------------------------------------------------------------------
// MainForm.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace AnarchyEditor
{
    /// <summary>
    /// Custom form provides the main user interface for the program.
    /// In this sample we used the designer to fill the entire form with a
    /// ModelViewerControl, except for the menu bar which provides the
    /// "File / Open..." option.
    /// </summary>
    public partial class MainForm : Form
    {
        ContentBuilder contentBuilder;
        ContentManager contentManager;


        /// <summary>
        /// Constructs the main form.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            contentBuilder = new ContentBuilder();

            contentManager = new ContentManager(modelViewerControl.Services,
                                                contentBuilder.OutputDirectory);

            Editor.Services = modelViewerControl.Services;



            /// Automatically bring up the "Load Model" dialog when we are first shown.
            this.Shown += OpenMenuClicked;
        }


        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Editor.GraphicsDevice = modelViewerControl.GraphicsDevice;
            Editor.Initialize();
        }

        /// <summary>
        /// Event handler for the Exit menu option.
        /// </summary>
        void ExitMenuClicked(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Event handler for the Open menu option.
        /// </summary>
        void OpenMenuClicked(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Content");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;

            fileDialog.Title = "Load Model";

            fileDialog.Filter = "Model Files (*.fbx;*.x)|*.fbx;*.x|" +
                                "FBX Files (*.fbx)|*.fbx|" +
                                "X Files (*.x)|*.x|" +
                                "All Files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadModel(fileDialog.FileName);
            }
        }


        /// <summary>
        /// Loads a new 3D model file into the ModelViewerControl.
        /// </summary>
        void LoadModel(string fileName)
        {
            Cursor = Cursors.WaitCursor;

            // Unload any existing model.
            //modelViewerControl.Model = null;
            contentManager.Unload();

            // Tell the ContentBuilder what to build.
            contentBuilder.Clear();
            contentBuilder.Add(fileName, "Model", null, "ModelProcessor");

            // Build this new model data.
            string buildError = contentBuilder.Build();

            if (string.IsNullOrEmpty(buildError))
            {
                // If the build succeeded, use the ContentManager to
                // load the temporary .xnb file that we just created.
                //modelViewerControl.Model = contentManager.Load<Model>("Model");
            }
            else
            {
                // If the build failed, display an error message.
                MessageBox.Show(buildError, "Error");
            }

            Cursor = Cursors.Arrow;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form shaders = new PickerForm();
            //if (shaders.ShowDialog() == DialogResult.OK)
            //{

            //}

            PickerForm modelPicker = new PickerForm();
            modelPicker.SetupList(Editor.GetModels());

            if (modelPicker.ShowDialog() == DialogResult.OK)
            {

                ModelInfo info = new ModelInfo();
                info.ContentPath = modelPicker.Result;
                Editor.Level.Models.Add(info);
                Editor.Reload();
                RefreshTreeNodes();
                

            }


        }

        void RefreshTreeNodes()
        {
            treeView1.Nodes.Clear();
            if (Editor.Level != null)
            {
                for (int i = 0; i < Editor.Level.Models.Count; i++)
                {
                    ModelRootTreeNode tn = new ModelRootTreeNode(Editor.Level.Models[i].ContentPath, Editor.Level.Models[i]);

                    Model m = Editor.GetModel(Editor.Level.Models[i].ContentPath);
                    foreach (ModelMesh mesh in m.Meshes)
                    {
                        TreeNode node = new TreeNode(mesh.Name);
                        tn.Nodes.Add(node);
                    }
                    

                    treeView1.Nodes.Add(tn);
                }
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.CreateNewLevel();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ModelRootTreeNode rootNode = e.Node as ModelRootTreeNode;
            if (rootNode != null)
            {
                propertyGrid1.SelectedObject = rootNode.ModelInfo;
            }
        }
    }
}
