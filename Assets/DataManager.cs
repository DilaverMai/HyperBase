using System.IO;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class DataManager : MonoBehaviour
{
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

#if UNITY_EDITOR

    [Button]
    private void ClearData()
    {
        playerData.Res();
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
