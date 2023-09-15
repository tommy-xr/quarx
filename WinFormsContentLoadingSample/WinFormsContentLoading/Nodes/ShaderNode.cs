using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace WinFormsContentLoading.Nodes
{
    public class ShaderNode : TreeNode
    {
        AnarchyShader shader;
        public AnarchyShader Shader
        {
            get { return shader; }
        }

        public ShaderNode(AnarchyShader inShader)
        {
            shader = inShader;
        }
    }
}
