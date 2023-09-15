using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Xml.Serialization;

using Sxe.Library.Materials;
using Sxe.Library.Utilities;

namespace SXEMaterialManager
{

    public class MaterialCategory
    {
        public string Name;
        public int Parent;
        public MaterialCategory(string inName, int inParent)
        {
            Name = inName;
            Parent = inParent;
        }

        public MaterialCategory()
        {
        }
    }

    


    public delegate void MCEEventHandler(object o);
 

    /// <summary>
    /// Derived class from material manager. This will be our editor class. It will have additional
    /// information, including the categories and the files contained in the material project,
    /// which have no importance from the MaterialCollection's perspective
    /// </summary>
    [XmlRoot("MaterialCollection")] 
    //HACK We have to "trick" the XML Serializer into thinking we are a true MaterialCollection
    public class MaterialCollectionEditor : MaterialCollection
    {
        

        public event MCEEventHandler OnUpdate;

        string currentDirectory;
        bool dirty = true;

        List<MaterialCategory> listCategory; //list of all the available categories in the project
        List<string> listFiles; //list of all the available files in the project
        List<Material> listMaterials; //list of all materials defined

        ServiceContainer services;
        ILogService log;

        //HACK We need to have the serializer ignore stuff that is public but instead in a MaterialCollection
        [XmlIgnore]
        public string CurrentDirectory
        {
            get { return currentDirectory; }
        }

        [XmlIgnore]
        public List<MaterialCategory> Categories
        {
            get { return listCategory; }
            set { listCategory = value; }
        }

        [XmlIgnore]
        public List<string> Files
        {
            get { return listFiles; }
        }

        [XmlIgnore]
        public List<Material> EditorMaterials
        {
            get { return this.listMaterials; }
        }

        [XmlIgnore]
        public bool Dirty
        {
            get { return dirty; }
        }

        public static MaterialCollectionEditor CreateNew(string directory, MCEEventHandler handler, ServiceContainer inServices)
        {
            MaterialCollectionEditor matEditor =new MaterialCollectionEditor(directory, inServices);
            matEditor.OnUpdate += handler;
            matEditor.AddDirectory(Constants.DefaultMaterialDirectory, -1, false);
           
            //SEMI-HACK: We are ASSUMING the default directory is 0. This is reasonable though, as there should be
            //no other directories when creating new
            matEditor.AddMaterial("Default", Constants.DefaultTexture, Constants.DefaultNormal, Constants.DefaultShader, 0);



            return matEditor;
        }

        public static MaterialCollectionEditor LoadFromFile(string path, MCEEventHandler handler, ServiceContainer inServices)
        {
            FileInfo fi = new FileInfo(path);

            MaterialCollectionEditor mat = XmlIO.Load<MaterialCollectionEditor>(path);
            mat.services = inServices;
            mat.OnUpdate += handler;
            mat.FinishLoad(path);

            return mat;
        }

        private MaterialCollectionEditor()
        {
            listCategory = new List<MaterialCategory>();
            listFiles = new List<string>();
            listMaterials = new List<Material>();
        }

        private MaterialCollectionEditor(string inDirectory, ServiceContainer inService)
            : this()
        {
            this.services = inService;
            currentDirectory = inDirectory;
            InitializeServices();

            log.Print("**Created new material file.");

        }

        void InitializeServices()
        {
            log = (ILogService)services.GetService(typeof(ILogService));
        }

        /// <summary>
        /// The load function loads a material file.
        /// It is also responsible for traversing the child directory structure and adding all
        /// files to the file list.
        /// </summary>
        /// <param name="materialPath"></param>
        private void FinishLoad(string path)
        {
            currentDirectory = Path.GetDirectoryName(path);
            InitializeServices();

            //At the end, we need to make sure we copy the Material[] over to the list
            if(this.Materials != null)
            listMaterials = new List<Material>(this.Materials);

        log.Print("**Load successful.");

        if (OnUpdate != null)
            OnUpdate(this);
        }

        /// <summary>
        /// The save function saves a material file
        /// </summary>
        /// <param name="materialPath"></param>
        public void Save(string name)
        {
            
            //At the end, make sure we copy over our list to the MaterialManager's array
            this.Materials = listMaterials.ToArray();

            XmlIO.Save<MaterialCollectionEditor>(this, currentDirectory + "//" + name);
            dirty = false;
            
        }


        /// <summary>
        /// Adds a category to the root. Returns the index
        /// </summary>
        /// <param name="categoryName"></param>
        public int AddCategory(string categoryName)
        {
            return AddCategory(categoryName, -1);
        }

        /// <summary>
        /// Adds a category as a child to specified parent. If parent < 0, the category is considered a root
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="parent"></param>
        public int AddCategory(string categoryName, int parent)
        {
            MaterialCategory matCat = new MaterialCategory(categoryName, parent);
            listCategory.Add(matCat);
            
            //Create the subdirectory
            string dir = currentDirectory + GetFullCategoryName(listCategory.Count - 1);
            Directory.CreateDirectory(dir);

            return listCategory.Count - 1;

        }

        /// <summary>
        /// Recursively deletes a category and all child categories
        /// </summary>
        /// <param name="categoryName"></param>
        public void DeleteCategory(int categoryIndex)
        {
        }

        public void DeleteMaterial(int materialIndex)
        {
        }


        /// <summary>
        /// Adds a directory from the file system
        /// </summary>
        /// <param name="path"></param>
        public void AddDirectory(string path, int category, bool addMaterials)
        {

            //First, check if this is actually a directory, if not, we'll add as a file
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists)
            {
                //Create a new category
                //We update the category variable because 
                int newCategory = AddCategory(di.Name, category);
                
                //Now, add files to this category
                FileInfo[] fileInfo = di.GetFiles();
                foreach (FileInfo fi in fileInfo)
                {
                    AddFile(fi.FullName, newCategory, addMaterials);
                    //AddMaterial(fi.Name, 
                }

                //Loop through all subdirectories in here and add them
                DirectoryInfo[] dirInfo = di.GetDirectories();
                foreach (DirectoryInfo dInfo in dirInfo)
                {
                    AddDirectory(dInfo.FullName, newCategory, addMaterials);
                }

            }
            else
            {
               
                AddFile(path, category, addMaterials);
            }

            //Signal anything hooked to the even thandler
            if(OnUpdate != null)
            OnUpdate(this);
        }

        /// <summary>
        /// Adds a file from the filesystem. Copies the file over, if necessary,
        /// and adds to the list of active files. DOES NOT add the material!
        /// </summary>
        /// <param name="path"></param>
        public void AddFile(string path, int category, bool addMaterial)
        {
            //Copy the file over

            string name = Path.GetFileName(path);

            //Only copy if file is not already in project
            if (!IsFileAlreadyInProject(path))
            {

                string copyPath = currentDirectory + GetFullCategoryName(category) + name;
                File.Copy(path, copyPath);
                Files.Add(GetRelativePath(copyPath));
                path = copyPath;
            }

            

            //TODO: add material
            if (addMaterial)
            {
                string materialName = GetRelativePath(path);
                AddMaterial(materialName, category);
            }

        }


        public void AddMaterial(string materialName, int category)
        {
            AddMaterial(materialName, materialName, Constants.DefaultNormalPath, Constants.DefaultShaderPath, category);
        }

        /// <summary>
        /// Responsible for adding the materials
        /// </summary>
        public void AddMaterial(string materialName, string diffuseTex, string normalTex, string shaderName, int category)
        {
            Material m = new Material();
            m.categoryIndex = category;
            m.MaterialName = materialName;
            m.DiffuseName = diffuseTex;
            m.NormalName = normalTex;
            m.ShaderName = shaderName;

            EditorMaterials.Add(m);
            if (OnUpdate != null)
                OnUpdate(this);
        }

        /// <summary>
        /// Returns true if the directory is a subdirectory of the working directory, false otherwise
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsSubdirectory(string path)
        {
            string fullPath = Path.GetFullPath(path);

            if (fullPath.Contains(this.currentDirectory))
            {
                return true;
            }

            return false;
        }


        public string GetFullCategoryName(int catIndex)
        {
            return GetFullCategoryName(catIndex, "");
        }

        public string GetFullCategoryName(int catIndex, string start)
        {
            if (catIndex == -1)
                return start + Path.DirectorySeparatorChar.ToString();

            return GetFullCategoryName(Categories[catIndex].Parent, Path.DirectorySeparatorChar +Categories[catIndex].Name + start );

        }

        public string GetRelativePath(string path)
        {
            if (path.Contains(currentDirectory))
            {
                string sz = path.Replace(currentDirectory, "");
                return sz.Substring(1, sz.Length - 1);
            }
            else
            {
                throw new Exception("Path is not inside project path!");
            }
        }

        /// <summary>
        /// Function to check if a file is already in the project or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsFileAlreadyInProject(string path)
        {
            string fullPath = Path.GetFullPath(path);

            foreach (string sz in listFiles)
            {
                if (sz == fullPath)
                    return true;
            }
            return false;
        }


    }
}
