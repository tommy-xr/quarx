using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;

namespace WinFormsContentLoading
{
    public partial class SettingsForm : Form
    {
        EditorSettings Settings;
        //public EditorSettings Settings
        //{
        //    get { return settings; }
        //    set { settings = value; }
        //}

        static Microsoft.Build.BuildEngine.Engine engine = null;
        

        public SettingsForm()
        {
            InitializeComponent();
            Settings = Editor.Settings;
            SetupFields();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void SetupFields()
        {
            if (Settings != null)
            {
                textBox1.Text = Settings.GameExecutablePath;
                textBox2.Text = Settings.ContentProjectPath;
                textBox3.Text = Settings.ContentDirectoryPath;
            }

        }



        void Save()
        {
            if (Settings == null)
                Settings = new EditorSettings();

            Settings.GameExecutablePath = textBox1.Text;
            Settings.ContentProjectPath = textBox2.Text;
            Settings.ContentDirectoryPath = textBox3.Text;

            if (Settings != null)
                Settings.Save(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                    ,Editor.SettingsFileName));

            if (Editor.Settings != Settings)
                Editor.Settings = Settings;

            //if (Settings != null)
            //{

            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        public string GetFileFromDialog(string title, string filter )
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            //string relativePath = Path.Combine(assemblyLocation, "../../../../Content");
            //string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = assemblyLocation;

            fileDialog.Title = title;
            fileDialog.Filter = filter;

            //fileDialog.Title = "Load Model";

            //fileDialog.Filter = "Model Files (*.fbx;*.x)|*.fbx;*.x|" +
            //                    "FBX Files (*.fbx)|*.fbx|" +
            //                    "X Files (*.x)|*.x|" +
            //                    "All Files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                return(fileDialog.FileName);
            }

            return null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = GetFileFromDialog("Game Executable", "XNA Game Executables (*.exe)|*.exe");

            string path = Path.GetDirectoryName(textBox1.Text);

            textBox3.Text = Path.Combine(path, "Content");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = GetFileFromDialog("Content Project Path", "XNA Content Project (*.contentproj)|*.contentproj");
        }



    }
}