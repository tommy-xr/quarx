using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine;

namespace Quarx
{

    public enum QuarxDifficulty
    {
        Easy = 0,
        Medium,
        Hard, 
        Custom,
        Multiplayer
    }

    public class QuarxGameSettings
    {
        double fallSpeed;
        int isotopes;
        int blocks;
        int height;
        IAnarchyGamer gamer;

        public double FallSpeed
        {
            get { return fallSpeed; }
            set { fallSpeed = value; }
        }

        public int Isotopes
        {
            get { return isotopes; }
            set { isotopes = value; }
        }

        public int Blocks
        {
            get { return blocks; }
            set { blocks = value; }
        }

        public int MaxHeight
        {
            get { return height; }
            set { height = value; }
        }

        public IAnarchyGamer Gamer
        {
            get { return gamer; }
            set { gamer = value; }
        }

        QuarxDifficulty difficulty;
        public QuarxDifficulty Difficulty
        {
            get { return difficulty; }
        }

        public QuarxGameSettings()
        {
            difficulty = QuarxDifficulty.Custom ;
        }


        public QuarxGameSettings(double inSpeed, int inIsotopes, QuarxDifficulty difficulty)
        {
            this.difficulty = difficulty;
            fallSpeed = inSpeed;
            isotopes = inIsotopes;
            blocks = 0;

            height = (isotopes / 8) + 2;

            if (height < 5)
                height += 4;
        }

        public void IncrementLevel()
        {
            fallSpeed -= 0.1;
            if (fallSpeed < 0.15)
                fallSpeed = 0.15;

            isotopes += 4;
            if (isotopes > 80)
                isotopes = 80;

            blocks = 0;

            height += 1;
            if (height < isotopes / 8)
                height = (isotopes / 8) + 1;


            if (height > 12)
                height = 12;


        }

        public static QuarxGameSettings Easy = new QuarxGameSettings(1.0, 5, QuarxDifficulty.Easy);
        public static QuarxGameSettings Medium = new QuarxGameSettings(0.70, 30, QuarxDifficulty.Medium);
        public static QuarxGameSettings Hard = new QuarxGameSettings(0.4, 60, QuarxDifficulty.Hard);

   

        
    }
}
