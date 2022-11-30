using System.IO;
using UnityEngine;

namespace Base.DataSystem
{
    public static class DataExtension
    {
        public static void ClearData()
        {
            if (File.Exists(Application.persistentDataPath + "/gamedata.json"))
            {
                var data = JsonUtility.FromJson<Data>(File.ReadAllText(Application.persistentDataPath + "/gamedata.json"));
                data.coin = 0;
                data.level = 0;
                data.showingLevel = 1;
                File.WriteAllText(Application.persistentDataPath + "/gamedata.json", JsonUtility.ToJson(data));
                Debug.LogWarning("Cleared data");
            }
            else Debug.LogWarning("NO DATA FOUND");
        }

        public static void SaveData(Data playerData)
        {
            File.WriteAllText(Application.persistentDataPath + "/gamedata.json", JsonUtility.ToJson(playerData));
        }

        public static Data GetData()
        {
            return JsonUtility.FromJson<Data>(File.ReadAllText(Application.persistentDataPath + "/gamedata.json"));
        }

        public static void CoinAdd(this Datas data, int _coin)
        {
            if (data != Datas.Coin) return;
            DataManager.Instance.PlayerData.coin += _coin;
            DataManager.WhenAddCoin?.Invoke();
            DataManager.Instance.SetData();
        }
    
        public static int GetData(this Datas data)
        {
            switch (data)
            {
                case Datas.Level:
                    return DataManager.Instance.PlayerData.level;
                case Datas.Coin:
                    return DataManager.Instance.PlayerData.coin;
            }

            return -1;
        }
    
        public static void SetData(this Datas data, int value)
        {
            switch (data)
            {
                case Datas.Level:
                    DataManager.Instance.PlayerData.level = value;
                    break;
                case Datas.Coin:
                    DataManager.Instance.PlayerData.coin = value;
                    DataManager.Instance.SetData();
                    break;
            }
        }
    
        public static int GetExtraData(this Datas data,string Key)
        {
            foreach (var extra in DataManager.Instance.PlayerData.ExtraDatas)
            {
                if (extra.DataName == Key)
                {
                    return extra.Value;
                }
            }
            return -1;
        }
    
        public static void SetExtraData(string dataName, int value)
        {
            foreach (var item in DataManager.Instance.PlayerData.ExtraDatas)
            {
                if (item.DataName == dataName)
                {
                    item.SetValue(value);
                    break;
                }
            }
        }
    }
}