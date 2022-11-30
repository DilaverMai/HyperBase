using System;
using System.IO;
using System.Threading.Tasks;
using DBase;
using UnityEngine;

namespace Base.DataSystem
{
    public class DataManager : Singleton<DataManager>
    {
        public static Action<int, int> OnSetData;
        public static Action WhenAddCoin;
        public static Action ReLoadData;

        public Data PlayerData;
        private Data backupData;
        private string path;

        public Task Setup()
        {
            path = Application.persistentDataPath + "/gamedata.json";
            return Task.Run(() =>
            {
                if (File.Exists(path))
                {
                    PlayerData = JsonUtility.FromJson<Data>(File.ReadAllText(path));
                    backupData = new Data(PlayerData.coin, PlayerData.level, PlayerData.showingLevel);
                }
                else
                {
                    PlayerData = new Data(0, 0, 1);
                    Debug.Log("No save file found so we created a new one");
                    SaveGame();
                }

                Debug.Log("SAVE LOADED");
            });
        }


        public void SaveGame()
        {
            File.WriteAllText(path, JsonUtility.ToJson(PlayerData));
            backupData = new Data(PlayerData.coin, PlayerData.level, PlayerData.showingLevel);
        }

        public void SetData()
        {
            OnSetData?.Invoke(PlayerData.showingLevel, PlayerData.coin);
        }

        private void ReLoadSave()
        {
            Debug.Log("Reloading save");
            PlayerData = new Data(backupData.coin, backupData.level, backupData.showingLevel);
        }

        private void OnEnable()
        {
            EventManager.OnAfterLoadedLevel += SetData;
            ReLoadData += ReLoadSave;
        }

        private void OnDisable()
        {
            EventManager.OnAfterLoadedLevel -= SetData;
            ReLoadData -= ReLoadSave;
        }
    }
}