using Base;
using Base.DataSystem;
using DBase;
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
    [BoxGroup("Data System")]
    public Data playerData;

    [BoxGroup("Data System")]
    [Button]
    public void ClearData()
    {
        DataExtension.ClearData();
        playerData = DataExtension.GetData();
    }
    [BoxGroup("Data System")]
    [Button]
    public void SaveData()
    {
        DataExtension.SaveData(playerData);
    }


    [BoxGroup("Level System")]
    [Button]
    public void NextLevel()
    {
        EventManager.NextLevel?.Invoke();
    }
    [BoxGroup("Level System")]
    [Button]
    public void RestartLevel()
    {
        EventManager.RestartLevel?.Invoke();
    }
    [BoxGroup("Level System")]
    [Button]
    public void WinLevel()
    {
        DBase.Base.FinisGame(GameStat.Win, 0);
    }
    
    [Button]
    [BoxGroup("Level System")]
    public void LoseLevel()
    {
        DBase.Base.FinisGame(GameStat.Lose, 0);
    }

    [BoxGroup("Override Funcs")]
    public int Level;
    
    [BoxGroup("Override Funcs")]
    [Button]
    public void StartGameOverrideLevel()
    {
        playerData.level = Level;
        DataExtension.SaveData(playerData);
        EditorApplication.isPlaying = true;
    }

    [BoxGroup("Data System")]
    [Button]
    private void GoPlayerControllerData()
    {
        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/Resources/Settings/PlayerData.asset");
    }
    [BoxGroup("Data System")]
    [Button]
    private void GoGameData()
    {
        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/Resources/Settings/GameData.asset");
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        playerData = DataExtension.GetData();
        // if (Application.isPlaying)
        //     RefreshPools();
    }

    // [Title("Pool System")]
    // public string[] PoolNames;
    //
    // [Button]
    // public void ClearPool()
    // {
    //     RefreshPools();
    // }

    // private void RefreshPools()
    // {
    //     PoolNames = PoolManager.Instance.GetPoolNames();
    // }
}