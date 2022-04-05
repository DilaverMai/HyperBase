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
                playerData = new Data(0,1,1);
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

#if UNITY_EDITOR

    [Button]
    private void ClearData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamedata.json"))
        {
            var data = JsonUtility.FromJson<Data>(File.ReadAllText(Application.persistentDataPath + "/gamedata.json"));
            data.Res();
            File.WriteAllText(Application.persistentDataPath + "/gamedata.json", JsonUtility.ToJson(data));
        }
    }
    [Button]
    private void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamedata.json"))
        {
            playerData = JsonUtility.FromJson<Data>(File.ReadAllText(Application.persistentDataPath + "/gamedata.json"));
        }
    }
    [Button]
    private void SaveData()
    {
        File.WriteAllText(Application.persistentDataPath + "/gamedata.json", JsonUtility.ToJson(playerData));
    }




#endif
}
[System.Serializable]
public class Data
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
        level = 1;
        showingLevel = 1;
    }

}
