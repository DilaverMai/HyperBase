using System;
using System.IO;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Action<int, int> OnSetData;
    public static Action<int> AddCoin;
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
        //CreateData<Data>.CreateMyAsset("backupData");
    }

    private void SetData()
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
        AddCoin += AddCoinFunc;
        ReLoadData += ReLoadSave;
    }

    private void OnDisable()
    {
        EventManager.OnAfterLoadedLevel -= SetData;
        AddCoin -= AddCoinFunc;
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
            data.level = 1;
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
