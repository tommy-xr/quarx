using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;


using Sxe.Library.Utilities;

using Microsoft.Build.BuildEngine;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WinFormsContentLoading
{
    /// <summary>
    /// Provides functions and properties to manage the editor
    /// </summary>
    public static class Editor
    {

        #region Constants
        public const string SettingsFileName = "Settings.xml";
        #endregion

        static string editorPath;
        public static string EditorPath
        {
            get { return editorPath; }
        }

        static EditorSettings settings;
        public static EditorSettings Settings
        {
            get { return settings; }
            set 
            { 
                settings = value;
                ReloadContentProject();
            }
        }

        static Sxe.Design.ServiceContainer services;
        public static Sxe.Design.ServiceContainer Services
        {
            get { return services; }
            set 
            { 
                services = value;

                //Initialize service stuff
                content = new ContentManager(services);
                content.RootDirectory = settings.ContentDirectoryPath;

                //Add services
                services.AddService<ISelectionService>(selectionService);

               
            }
        }

        static ISelectionService selectionService = new SelectionService();
        static ISelectionService SelectionService
        {
            get { return selectionService; }
        }

        static GraphicsDevice device;
        public static GraphicsDevice GraphicsDevice
        {
            get { return device; }
            set { device = value; }
        }
        

        static ContentManager content;
        public static ContentManager Content
        {
            get { return content; }
        }

        static AnarchyLevelData level;
        public static AnarchyLevelData Level
        {
            get { return level; }
            //set 
            //{ 
            //    level = value;
            //    if (LevelChanged != null)
            //        LevelChanged(null, EventArgs.Empty);
            //}
        }

        //Dictionary to store all the models used in the level scene
        static Dictionary<string, Model> modelDictionary = new Dictionary<string, Model>();
        public static Model GetModel(string modelName)
        {
            return modelDictionary[modelName];
        }

        public static event EventHandler LevelChanged;

        static Microsoft.Build.BuildEngine.Engine engine;
        static Microsoft.Build.BuildEngine.Project contentProject;
        public static Microsoft.Build.BuildEngine.Project ContentProject
        {
            get { return contentProject; }
           //set { contentProject = value; }
        }

        public static void CreateNewLevel()
        {
            //TODO: Add save logic here, if the level is dirtied

            LoadLevel(new AnarchyLevelData());
        }

        public static void LoadLevel(AnarchyLevelData loadLevel)
        {
            if (LevelChanged != null)
                LevelChanged(null, EventArgs.Empty);

            modelDictionary.Clear();
            level = loadLevel;

            ReloadModels();

        }

        public static void Reload()
        {
            ReloadModels();
        }

        static void ReloadModels()
        {
            foreach (ModelInfo mi in level.Models)
            {
                if (!modelDictionary.ContainsKey(mi.ContentPath))
                {
                    modelDictionary.Add(mi.ContentPath, content.Load<Model>(mi.ContentPath));
                }
            }
        }

        public static void Update()
        {
            KeyboardState state = Keyboard.GetState();
        }



        public static void Initialize()
        {
           //TODO: Set editor path

            //Try and load Settings.xml, if we can't, show the Settings box immediately
            try
            {
                Settings = XmlIO.Load<EditorSettings>(SettingsFileName, true);

            }
            catch (FileNotFoundException ex)
            {
                SettingsForm form = new SettingsForm();
                form.Show();
            }

            //Level = new AnarchyLevelData();

            LoadLevel(new AnarchyLevelData());
            


            //ReloadShaders();
        }

        static void ReloadContentProject()
        {
            if (engine == null)
                engine = new Microsoft.Build.BuildEngine.Engine(RuntimeEnvironment.GetRuntimeDirectory());

            try
            {
                contentProject = new Microsoft.Build.BuildEngine.Project(engine);
                ContentProject.Load(Settings.ContentProjectPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not load content project.");
            }
        }

        public static List<string> GetModels()
        {
            return GetContentItems(".x", ".fbx");
        }

        public static List<string> GetShaders()
        {
            return GetContentItems(".fx");
        }

        static List<String> GetContentItems(params string[] extensions)
        {
            List<string> shaders = new List<string>();
            if (contentProject != null)
            {
                foreach (BuildItemGroup group in contentProject.ItemGroups)
                {
                    foreach (BuildItem item in group)
                    {
                        if (item.Name == "Compile")
                        {
                            bool passFilter = false;
                            foreach (string sz in extensions)
                            {
                                if (item.Include.Contains(sz))
                                {
                                    passFilter = true;
                                    break;
                                }
                            }

                            //Sweet, we have a content item. Let's change if the item is a shader
                            if (passFilter)
                            {
                                //Do we have a compiled version??
                                string fileName = Path.GetFileName(item.Include);
                                string directory = item.Include.Remove(item.Include.Length - fileName.Length);
                                string name = item.GetMetadata("Name");

                                string compiledPath = directory;

                                compiledPath += name;// +".xnb";

                                shaders.Add(compiledPath);

                                //Effect effect = new Effect();
                                //EffectParameter parameter;
                            }

                        }
                    }
                }
            }
            return shaders;
        }



    }
}
