using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Quarx
{
    public class HighScoreEntry
    {
        string name;
        int score;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Score
        {
            set { score = value; }
            get { return score; }
        }

        public HighScoreEntry()
        {
  
        }

        public HighScoreEntry(string inName, int inScore)
        {
            this.name = inName;
            this.score = inScore;
        }

        public HighScoreEntry(StreamReader sr)
        {
            this.name = sr.ReadLine();
            this.score = Int32.Parse(sr.ReadLine());
        }

        public void Write(StreamWriter sw)
        {
            sw.WriteLine(this.name);
            sw.WriteLine(this.score.ToString());
        }
    }

    public class HighScoreEntryComparer : IComparer<HighScoreEntry>
    {
        public int Compare(HighScoreEntry h1, HighScoreEntry h2)
        {
            return h2.Score - h1.Score;
        }
    }
}
