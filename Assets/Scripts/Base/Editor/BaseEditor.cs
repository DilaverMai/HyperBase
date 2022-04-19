using System.IO;
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

    [Title("Data System")]
    public Data playerData;
    [Button]
    public void ClearData()
    {
        DataExtension.ClearData();
    }
    [Button]
    public void SaveData()
    {
        DataExtension.SaveData(playerData);
    }

    [Title("Level System")]
    
    protected override void OnEnable()
    {
        base.OnEnable();
        playerData = DataExtension.GetData();
    }
}
