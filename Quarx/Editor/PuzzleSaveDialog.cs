using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Quarx.Editor
{
    public partial class PuzzleSaveDialog : Form
    {


        public string PuzzleName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public string IconName
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }

        public string Path
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }

        public event EventHandler<CancelEventArgs> FileOk;

        public PuzzleSaveDialog()
        {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.FileOk += OnFileOk;
            fileDialog.ShowDialog();
        }

        private void OnFileOk(object sender, CancelEventArgs cancel)
        {
            SaveFileDialog dialog = sender as SaveFileDialog;
            if (dialog != null)
            {
                Path = dialog.FileName; 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InvokeFileOk(false);
            this.Close();
        }

        private void InvokeFileOk(bool cancelled)
        {
            if(FileOk != null)
                FileOk(this, new CancelEventArgs(cancelled));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InvokeFileOk(true);
            this.Close();
        }

    }
}