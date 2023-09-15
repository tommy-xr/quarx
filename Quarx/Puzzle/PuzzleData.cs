using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Quarx.Puzzle
{
    public class PuzzleData : ICloneable
    {
        int currentPuzzle = 1;
        public int CurrentPuzzle
        {
            get { return currentPuzzle; }
            set { currentPuzzle = value; }
        }

        public object Clone()
        {
            PuzzleData outData = new PuzzleData();
            outData.currentPuzzle = this.currentPuzzle;
            return outData;
        }

        public static void Save(PuzzleData puzzle,string path)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter sw = new BinaryWriter(fs);
            sw.Write(puzzle.currentPuzzle);
            sw.Close();
            fs.Close();
        }

        public static PuzzleData Load(string path)
        {
            PuzzleData pd = new PuzzleData();

            if (!File.Exists(path))
                return null;

            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader br = new BinaryReader(fs);
            pd.currentPuzzle = br.ReadInt32();
            br.Close();
            fs.Close();

            return pd;
        }
    }
}
