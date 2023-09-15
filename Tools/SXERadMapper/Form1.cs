using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXERadMapper
{
    public partial class Form1 : Form
    {
        IGraphicsDeviceService GraphicDeviceService;


        RadiosityBSP bsp;

        public Form1()
        {
            InitializeComponent();
            GraphicDeviceService = GraphicsDeviceService.AddRef(this.Handle, this.Width, this.Height);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bsp = new RadiosityBSP();
            bsp.Load("radtest1", GraphicDeviceService.GraphicsDevice, 15);


            MessageBox.Show("Load complete!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //bsp.SaveOriginalLightmaps("original");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //bsp.CreateRadiosityLightmaps(1000000);
            //bsp.SaveNewTextures("new");
            MessageBox.Show("Radiosity calculations complete!");
        }
    }
}