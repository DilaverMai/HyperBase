using System;
using System.IO;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Action<int, int> OnSetData;
    public static Action<int> AddCoin;

    private string path;
    [SerializeField]
    private Data playerData;
    public Data PlayerData => playerData;
    public Task CheckSave()
    {
        path = Application.persistentDataPath + "/gamedata.json";
        return Task.Run(() =>
        {
            if (File.Exists(path))
            {
                playerData = JsonUtility.FromJson<Data>(File.ReadAllText(path));
            }
            else
            {
                playerData = new Data();
                playerData.Res();
                SaveGame();
            }
        });
    }


    public void SaveGame()
    {
        File.WriteAllText(path, JsonUtility.ToJson(playerData));
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void SetData()
    {
        OnSetData?.Invoke(playerData.showingLevel, playerData.coin);
    }

    private void AddCoinFunc(int _gold)
    {
        playerData.coin += _gold;
        SetData();
    }

    private void OnEnable()
    {
        EventManager.OnBeforeLoadedLevel += SetData;
        AddCoin += AddCoinFunc;
    }

    private void OnDisable()
    {
        EventManager.OnBeforeLoadedLevel -= SetData;
        AddCoin -= AddCoinFunc;
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

    public void Res()
    {
        coin = 0;
        level = 1;
        showingLevel = 1;
    }

}
