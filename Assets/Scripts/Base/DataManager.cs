using System;
using System.IO;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Action<int, int> OnSetData;
    public static Action ReLoadData;
    private string path;
    [SerializeField]
    private Data playerData;
    public Data PlayerData => playerData;
    private Data backupData;
    public int Coin => playerData.coin;
    public static DataManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Task CheckSave()
    {
        path = Application.persistentDataPath + "/gamedata.json";
        return Task.Run(() =>
        {
            if (File.Exists(path))
            {
                playerData = JsonUtility.FromJson<Data>(File.ReadAllText(path));
                backupData = new Data(playerData.coin, playerData.level, playerData.showingLevel);
            }
            else
            {
                playerData = new Data(0, 1, 1);
                Debug.Log("No save file found so we created a new one");
                SaveGame();
            }

            Debug.Log("SAVE LOADED");

        });
    }


    public void SaveGame()
    {
        File.WriteAllText(path, JsonUtility.ToJson(playerData));
        backupData = new Data(playerData.coin, playerData.level, playerData.showingLevel);
    }

    public void SetData()
    {
        OnSetData?.Invoke(playerData.showingLevel, playerData.coin);
    }

    //reload save
    private void ReLoadSave()
    {
        Debug.Log("Reloading save");
        playerData = new Data(backupData.coin, backupData.level, backupData.showingLevel);
    }
    private void AddCoinFunc(int _gold)
    {
        playerData.coin += _gold;
        SetData();
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

public static class DataExtension
{
    public static Data RunTimeGetData()
    {
        return DataManager.Instance.PlayerData;
    }

    public static void ClearData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamedata.json"))
        {
            var data = JsonUtility.FromJson<Data>(File.ReadAllText(Application.persistentDataPath + "/gamedata.json"));
            data.coin = 0;
            data.level = 0;
            data.showingLevel = 1;
            data.areDatas.Clear();
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
    
    public static void CoinAdd(this Datas data, int _coin)
    {
        if(data != Datas.Coin) return;
        DataManager.Instance.PlayerData.coin += _coin;
        DataManager.Instance.SetData();
    }
}

[System.Serializable]
public partial class Data
{
    public int coin;
    public int level;
    public int showingLevel;

    public Data(int _coin, int _level, int _showingLevel)
    {
        coin = _coin;
        level = _level;
        showingLevel = _showingLevel;
    }
    public void Res()
    {
        coin = 0;
        level = 0;
        showingLevel = 1;
    }

}
