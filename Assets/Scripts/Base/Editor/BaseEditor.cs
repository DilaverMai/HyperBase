using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class BaseEditor : OdinEditorWindow
{
    [MenuItem("Base/Editor")]
    private static void OpenWindow()
    {
        GetWindow<BaseEditor>().Show();
    }
    [InfoBox("F1 = GAME PAUSE // GAME RESUME")]
    [Title("Data System")]
    public Data playerData;
    [Button]
    public void ClearData()
    {
        DataExtension.ClearData();
        playerData = DataExtension.GetData();
    }
    [Button]
    public void SaveData()
    {
        DataExtension.SaveData(playerData);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        playerData = DataExtension.GetData();
    }
    [Title("Level System")]
    [Button]
    public void NextLevel()
    {
        EventManager.NextLevel?.Invoke();
    }

    [Button]
    public void RestartLevel()
    {
        EventManager.RestartLevel?.Invoke();
    }
    [Button]
    public void WinLevel()
    {
        EventManager.FinishGame?.Invoke(GameStat.Win);
    }
    [Button]
    public void LoseLevel()
    {
        EventManager.FinishGame?.Invoke(GameStat.Lose);
    }
}
