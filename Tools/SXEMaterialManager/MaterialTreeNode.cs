using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SXEMaterialManager
{

    public enum MaterialInfoType
    {
        Diffuse = 0,
        Normal,
        Specular,
        Shader
    }

    public class CategoryTreeNode : TreeNode
    {
    }


    //
    public class MaterialTreeNode : TreeNode
    {
        public int MaterialIndex = -1;
        TreeNode normal;
        TreeNode diffuse;
        TreeNode specular;
        TreeNode shader;

        //We also need tree nodes for the different items
        public MaterialTreeNode(string name, int index)
            : base(name)
        {
            MaterialIndex = index;

            normal = new MaterialInfoTreeNode("Normal", MaterialInfoType.Normal);
            diffuse = new MaterialInfoTreeNode("Diffuse", MaterialInfoType.Diffuse);
            specular = new MaterialInfoTreeNode("Specular", MaterialInfoType.Specular);
            shader = new MaterialInfoTreeNode("Shader", MaterialInfoType.Shader);

            this.Nodes.Add(normal);
            this.Nodes.Add(diffuse);
            this.Nodes.Add(specular);
            this.Nodes.Add(shader);
        }
    }

    public class MaterialInfoTreeNode : TreeNode
    {
        MaterialInfoType type;
        string name;

        public MaterialInfoTreeNode(string name, MaterialInfoType inType)
            : base(name)
        {
            const int IMAGE_OFFSET = 1;

            type = inType;
            this.ImageIndex = (int)inType + IMAGE_OFFSET;
            this.SelectedImageIndex = (int)inType + IMAGE_OFFSET;
            //this.Show
       
        }
    }
}
