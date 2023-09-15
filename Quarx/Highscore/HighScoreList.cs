using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework.Storage;

using Sxe.Engine.Storage;

namespace Quarx
{
    public class HighScoreList : ICloneable
    {
        

        const int HighScoreEntries = 8;

        List<HighScoreEntry> highScoreEntries = new List<HighScoreEntry>();
        //string fileName;

        HighScoreEntryComparer comparer = new HighScoreEntryComparer();

        public string FileName
        {
            get;
            set;
        }

        public bool Loaded
        {
            get;
            set;
        }

        public HighScoreEntry this[int index]
        {
            get { return highScoreEntries[index]; }
        }

        public object Clone()
        {
            HighScoreList newList = new HighScoreList();
            newList.highScoreEntries = new List<HighScoreEntry>();
            newList.highScoreEntries.AddRange(this.highScoreEntries);
            newList.FileName = this.FileName;
            return newList;
        }

        public HighScoreList()
        {
            this.Loaded = false;
            for (int i = 0; i < HighScoreEntries; i++)
            {
                HighScoreEntry entry = new HighScoreEntry("Ionixx", (i+1) * 200);
                highScoreEntries.Add(entry);
            }
            highScoreEntries.Sort(comparer);
        }

        public bool IsHighScore(int score)
        {
            highScoreEntries.Sort(comparer);
            if (score > highScoreEntries[HighScoreEntries - 1].Score)
                return true;

            return false;
        }

        public bool AddHighScore(string name, int score)
        {
            if (!IsHighScore(score))
                return false;

            highScoreEntries.Add(new HighScoreEntry(name, score));

            highScoreEntries.Sort(comparer);

            highScoreEntries.RemoveAt(HighScoreEntries);

            //this.Save(HighScores.StorageDevice);

            return true;
        }


        public static HighScoreList Load(string path)
        {

            HighScoreList list = new HighScoreList();


            FileStream fs = new FileStream(
                path, FileMode.Open, FileAccess.Read);


            StreamReader sr = new StreamReader(fs);

            list.FileName = sr.ReadLine();
            list.highScoreEntries.Clear();
            for (int i = 0; i < HighScoreEntries; i++)
            {
                list.highScoreEntries.Add(new HighScoreEntry(sr));
            }
           
            sr.Close();
            fs.Close();

            return list;
        }

        public void Save(IStorageDeviceService device)
        {
            device.Save<HighScoreList>(this.FileName, this, Save);
        }

        public static void Save(HighScoreList list, string path)
        {


            FileStream fs = new FileStream(
                path, FileMode.OpenOrCreate, FileAccess.Write);


            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(list.FileName);
            for (int i = 0; i < HighScoreEntries; i++)
                list.highScoreEntries[i].Write(sw);

            sw.Close();
            fs.Close();

   
        }
    }
}
