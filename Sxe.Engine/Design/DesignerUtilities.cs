#if !XBOX

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.ComponentModel;
using EnvDTE;
using System.IO;
using Microsoft.Build.BuildEngine;
using System.Runtime.InteropServices;

namespace Sxe.Design
{
    public static class DesignerUtilities
    {

        private static Microsoft.Build.BuildEngine.Engine engine = null;
        private static Microsoft.Build.BuildEngine.Project project = null;

        public static ArrayList GetContentItems(ITypeDescriptorContext context, StringCollection allowedProcessors, StringCollection allowedImporters)
        {
            if (context == null)
                return null;

            if (project == null)
                project = GetProject(context);

            return GetContentItems(allowedProcessors, allowedImporters);
        }

        public static ArrayList GetContentItems(string path, StringCollection allowedProcessors, StringCollection allowedImporters)
        {
            if (project == null)
                project = GetProject(path);

            return GetContentItems(allowedProcessors, allowedImporters);
        }


        private static ArrayList GetContentItems(StringCollection allowedProcessors, StringCollection allowedImporters)
        {
            
            ArrayList values = new ArrayList();
            values.Clear();
            //values.Add("HEY");

            //if (project == null)
            //    project = GetProject(context);

            foreach (BuildItemGroup group in project.ItemGroups)
            {
                foreach (BuildItem item in group)
                {
                    if (item.Name == "Compile")
                    {
                        if (allowedProcessors == null || allowedProcessors.Contains(item.GetMetadata("Processor")))
                        {
                            if (allowedImporters == null || allowedImporters.Contains(item.GetMetadata("Importer")))
                            {
                                //string directory = Path.GetDirectoryName(item.Include);
                                //directory = item.Include;
                                //if (item.HasMetadata("Name"))
                                //    directory += item.GetMetadata("Name");

                                string filename = Path.GetFileName(item.Include);
                                string directory = item.Include.Remove(item.Include.Length - filename.Length);
                                string name = item.GetMetadata("Name");

                                values.Add(directory + name);
                            }
                        }
                    }
                }
            }

            //if (engine != null)
            //    engine.UnloadAllProjects();

            return values;
        }

        private static Microsoft.Build.BuildEngine.Project GetProject(ITypeDescriptorContext context)
        {
            return GetProject(GetProjectPath(context));
        }
      

        private static Microsoft.Build.BuildEngine.Project GetProject(string projectPath)
        {
            
            string contentPath = GetContentProjectPath(projectPath);

            if (engine == null)
                engine = new Microsoft.Build.BuildEngine.Engine(RuntimeEnvironment.GetRuntimeDirectory());
                //engine = Microsoft.Build.BuildEngine.Engine.GlobalEngine;

            Microsoft.Build.BuildEngine.Project outProject = new Microsoft.Build.BuildEngine.Project(engine);
            //Microsoft.Build.BuildEngine.Project project = engine.GetLoadedProject(projectPath);
            outProject.Load(contentPath);
            //project.AddNewImport(contentPath, null);

            return outProject;
        }

        public static string GetProjectPath(ITypeDescriptorContext context)
        {
            DTE dte = context.GetService(typeof(DTE)) as DTE;

            string outPath = null;
            if (dte != null)
            {
                object[] projects = (object[])dte.ActiveSolutionProjects;
                EnvDTE.Project project = projects[0] as EnvDTE.Project;


                if (project != null)
                {
                    outPath = project.FullName;
                }
            }

            return outPath;
        }

        public static string GetBuiltContentPath(ITypeDescriptorContext context)
        {
            string projectPath = GetProjectPath(context);

            //TODO: Fix this to work for any config!!!!!!!!!
            return Path.Combine(Path.GetDirectoryName(projectPath), "bin\\x86\\Debug\\Content");
        }

        

        public static string GetContentPath(ITypeDescriptorContext context)
        {
            return Path.GetDirectoryName(GetContentProjectPath(GetProjectPath(context)));
        }

        private static string GetContentProjectPath(string fullProjectPath)
        {
            //Clip the filename off the full project path
            string contentFolder = Path.GetDirectoryName(fullProjectPath);

            contentFolder += "/Content/Content.contentproj";

            return contentFolder;
        }



    }
}

#endif
