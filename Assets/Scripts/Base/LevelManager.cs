using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Sirenix.OdinInspector;

public class LevelManager : MonoBehaviour
{
    public static Action NextLevel;
    public static Action RestartLevel;
    public GameObject[] _Levels => levels;
    [SerializeField]
    private GameObject[] levels;

    public void LoadLevel()
    {
        if (levels.Length > 0)
        {
            Array.Clear(levels, 0, levels.Length);
        }

        levels = Resources.LoadAll<GameObject>("Levels");
        var currentLevel = GameBase.Instance.DataManager.PlayerData.level;

        if (levels.Length == 0)
        {
            Debug.LogError("No Levels Found");
        }

        if (levels.Length < currentLevel)
        {
            currentLevel = 1;
        }

        LoadLevelFunc();
    }
    private void LoadLevelFunc()
    {
        EventManager.OnBeforeLoadedLevel?.Invoke();

        var currentLevel = GameBase.Instance.DataManager.PlayerData.level;
        if (levels.Length == 0)
        {
            Debug.LogError("No Levels Found");
        }

        if (levels.Length < currentLevel)
        {
            currentLevel = 1;
        }

        var level = levels[currentLevel - 1];
        var levelObject = Instantiate(level, Vector3.zero, Quaternion.identity);
        levelObject.transform.SetParent(transform);
        EventManager.OnAfterLoadedLevel?.Invoke();

    }
    private void NextLevelFunc()
    {
        var currentLevel = GameBase.Instance.DataManager.PlayerData.level;
        GameBase.Instance.DataManager.PlayerData.showingLevel += 1;
        EventManager.OnBeforeLoadedLevel?.Invoke();

        var nextLevel = currentLevel + 1;

        if (nextLevel > levels.Length)
        {
            nextLevel = 1;
        }

        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        var lvl = Instantiate(levels[nextLevel - 1]);
        lvl.transform.SetParent(transform);
        EventManager.OnAfterLoadedLevel?.Invoke();

        GameBase.Instance.DataManager.SaveGame();
    }

    private void RestartLevelFunc()
    {
        EventManager.OnBeforeLoadedLevel?.Invoke();
        DataManager.ReLoadData?.Invoke();
        var currentLevel = GameBase.Instance.DataManager.PlayerData.level;
        //GameBase.Instance.DataManager.PlayerData.showingLevel = currentLevel;

        var nextLevel = currentLevel;

        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        var lvl = Instantiate(levels[nextLevel - 1]);
        lvl.transform.SetParent(transform);
        EventManager.OnAfterLoadedLevel?.Invoke();
    }

    private void OnEnable()
    {
        EventManager.NextLevel += NextLevelFunc;
        EventManager.RestartLevel += RestartLevelFunc;
        // EventManager.WhenLose += RestartLevelFunc;
        // EventManager.WhenWin += NextLevelFunc;
    }

#if UNITY_EDITOR
    [Button]
    private void OverrideLoadLevels()
    {
        levels = Resources.LoadAll<GameObject>("Levels");
    }
#endif
}