using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Storage;

using Sxe.Engine;
using Sxe.Engine.Storage;

namespace Quarx
{
    public static class HighScores
    {
 
        static readonly string easyFileName = "easy.txt";
        static readonly string mediumFileName = "medium.txt";
        static readonly string hardFileName = "hard.txt";
        static readonly string customFileName = "custom.txt";

        public static HighScoreList Easy = new HighScoreList() { FileName = easyFileName };
        public static HighScoreList Medium = new HighScoreList() { FileName = mediumFileName };
        public static HighScoreList Hard = new HighScoreList() { FileName = hardFileName };
        public static HighScoreList Custom = new HighScoreList() { FileName = customFileName };

        static LoadResult<HighScoreList> easyResult;
        static LoadResult<HighScoreList> mediumResult;
        static LoadResult<HighScoreList> hardResult;
        static LoadResult<HighScoreList> customResult;


        static IStorageDeviceService storageDeviceService;

        public static HighScoreList HighScoresFromDifficulty(QuarxDifficulty difficulty)
        {
            switch (difficulty)
            {
                case QuarxDifficulty.Easy:
                    return Easy;
                case QuarxDifficulty.Medium:
                    return Medium;
                case QuarxDifficulty.Hard:
                    return Hard;
                default:
                case QuarxDifficulty.Custom:
                    return Custom;
            }
        }

        public static bool HasLoaded
        {
            get { return Easy.Loaded && Medium.Loaded && Hard.Loaded && Custom.Loaded; }
        }

        public static void Load(IStorageDeviceService storage)
        {
            storageDeviceService = storage;

            if (!Easy.Loaded && easyResult == null)
                easyResult = storage.Load<HighScoreList>(Easy.FileName, HighScoreList.Load);

            if (!Medium.Loaded && mediumResult == null)
                mediumResult = storage.Load<HighScoreList>(Medium.FileName, HighScoreList.Load);

            if (!Hard.Loaded && hardResult == null)
                hardResult = storage.Load<HighScoreList>(Hard.FileName, HighScoreList.Load);

            if (!Custom.Loaded && customResult == null)
                customResult = storage.Load<HighScoreList>(Custom.FileName, HighScoreList.Load);
        }

        public static void CheckResult(ref LoadResult<HighScoreList> result , ref HighScoreList list)
        {
            if (result != null)
            {
                if (result.IsComplete)
                {
                    if(result.LoadedData != null)
                    list = result.LoadedData;

                    if (list == null)
                        list = new HighScoreList();

                    result = null;
                    list.Loaded = true;
                }
            }
        }
 

        public static void Update()
        {

            CheckResult(ref easyResult, ref  Easy);
            CheckResult(ref mediumResult, ref Medium);
            CheckResult(ref hardResult, ref Hard);
            CheckResult(ref customResult, ref Custom);
           
        }


 

        //public static void Save()
        //{
        //    storageDeviceComponent.Save<HighScoreList>(easyFileName, Easy);
        //    storageDeviceComponent.Save<HighScoreList>(mediumFileName, Medium);
        //    storageDeviceComponent.Save<HighScoreList>(hardFileName, Hard);
        //    storageDeviceComponent.Save<HighScoreList>(customFileName, Custom);

        //    //if (device != null)
        //    //{
        //    //    if (device.IsConnected)
        //    //    {
        //    //        Easy.Save(device);
        //    //        Medium.Load(device);
        //    //        Hard.Load(device);
        //    //        Custom.Load(device);
        //    //    }
        //    //}
        //}
    }
}
