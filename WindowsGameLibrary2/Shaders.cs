using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnarchyEditor
{
    public partial class PickerForm : Form
    {
        string result;
        public string Result
        {
            get { return listBox1.SelectedItem.ToString(); }
        }


        public PickerForm()
        {
            InitializeComponent();


            //List<string> shaders = Editor.GetShaders();

            //foreach (string shaderName in shaders)
            //    listBox1.Items.Add(shaderName);
        }

        public void SetupList(List<string> options)
        {
            foreach (string sz in options)
                listBox1.Items.Add(sz);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}