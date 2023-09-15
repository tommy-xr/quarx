using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sxe.Engine
{

    public class GlobalContentLoader : ILoadable
    {
        string contentToLoad;

        public GlobalContentLoader(string content)
        {
            contentToLoad = content;
        }

        public void Load()
        {
            GlobalContent.LoadContent(contentToLoad);
        }
    }

    //public class GlobalContentLoader : ILoadable
    //{
    //    List<string> contentItems = new List<string>();
    //    int currentLoaded = 0;
    //    bool isComplete = false;

    //    Thread loadThread;
    //    public float PercentComplete
    //    {
    //        get { return (float)currentLoaded / (float)contentItems.Count; }
    //    }

    //    public bool IsComplete
    //    {
    //        get { return isComplete; }
    //    }

    //    public void Add(string contentItem)
    //    {
            
    //        if(loadThread == null && !isComplete)
    //        contentItems.Add(contentItem);
    //    }

    //    public void Load()
    //    {
    //        loadThread = new Thread(new ThreadStart(LoadThread));
    //        loadThread.Start();
    //    }

    //    void LoadThread()
    //    {
           
    //        foreach (string item in contentItems)
    //        {

    //            GlobalContent.LoadContent(item);
    //            currentLoaded++;
    //            System.Threading.Thread.Sleep(1);
    //        }
    //        isComplete = true;
    //    }
    //}
}
