
#if !XBOX

namespace Sxe.Design.ImagePicker
{
    partial class ImagePickerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.texturePickerControl1 = new Sxe.Design.ImagePicker.TexturePickerControl();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(311, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(121, 354);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(563, 73);
            this.vScrollBar1.Maximum = 20;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(18, 379);
            this.vScrollBar1.TabIndex = 3;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // texturePickerControl1
            // 
            this.texturePickerControl1.Location = new System.Drawing.Point(149, 73);
            this.texturePickerControl1.Name = "texturePickerControl1";
            this.texturePickerControl1.Size = new System.Drawing.Size(411, 379);
            this.texturePickerControl1.TabIndex = 1;
            this.texturePickerControl1.Text = "texturePickerControl1";
            // 
            // ImagePickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 464);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.texturePickerControl1);
            this.Controls.Add(this.button1);
            this.Name = "ImagePickerForm";
            this.Text = "AnarchyX Texture Browser";
            this.Load += new System.EventHandler(this.ImagePickerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private TexturePickerControl texturePickerControl1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
    }
}

#endif