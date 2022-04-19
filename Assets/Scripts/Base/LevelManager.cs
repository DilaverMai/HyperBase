using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;
public class LevelManager : MonoBehaviour
{
    public static Action NextLevel;
    public static Action RestartLevel;
    public GameObject[] _Levels => levels;
    [SerializeField]
    private GameObject[] levels;
    public Transform LevelHolder;
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
    [Button]
    private void LoadLevelFunc()
    {
        Debug.Log("Loading Level");
        EventManager.OnBeforeLoadedLevel?.Invoke();

        var currentLevel = GameBase.Instance.DataManager.PlayerData.level;

        if (levels.Length == 0)
            Debug.LogError("No Levels Found");

        if (levels.Length <= currentLevel)
            currentLevel = 0;

        var level = levels[currentLevel];
        var levelObject = Instantiate(level, Vector3.zero, Quaternion.identity);
        LevelHolder = levelObject.transform;
        levelObject.transform.SetParent(transform);
        EventManager.OnAfterLoadedLevel?.Invoke();
    }
    [Button]
    private void NextLevelFunc()
    {
        Debug.Log("Next Level");
        DataManager.Instance.PlayerData.level++;
        var currentLevel = DataManager.Instance.PlayerData.level;
        DataManager.Instance.PlayerData.showingLevel++;
        EventManager.OnBeforeLoadedLevel?.Invoke();

        var nextLevel = currentLevel;

        if (nextLevel >= levels.Length)
        {
            nextLevel = Random.Range(0, levels.Length);
            DataManager.Instance.PlayerData.level = nextLevel;
        }

        if (LevelHolder != null)
            Destroy(LevelHolder.gameObject);

        var lvl = Instantiate(levels[nextLevel]);
        lvl.transform.SetParent(transform);
        LevelHolder = lvl.transform;

        EventManager.OnAfterLoadedLevel?.Invoke();
        DataManager.Instance.SaveGame();
    }
    [Button]
    private void RestartLevelFunc()
    {
        Debug.Log("Restart Level");
        DataManager.ReLoadData?.Invoke();
        EventManager.OnBeforeLoadedLevel?.Invoke();

        var currentLevel = GameBase.Instance.DataManager.PlayerData.level;

        if (LevelHolder != null)
            Destroy(LevelHolder.gameObject);

        var lvl = Instantiate(levels[currentLevel]);
        lvl.transform.SetParent(transform);
        LevelHolder = lvl.transform;
        EventManager.OnAfterLoadedLevel?.Invoke();
    }

    private void OnEnable()
    {
        EventManager.NextLevel += NextLevelFunc;
        EventManager.RestartLevel += RestartLevelFunc;
        // EventManager.WhenLose += RestartLevelFunc;
        // EventManager.WhenWin += NextLevelFunc;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        EventManager.NextLevel -= NextLevelFunc;
        EventManager.RestartLevel -= RestartLevelFunc;
        // EventManager.WhenLose -= RestartLevelFunc;
        // EventManager.WhenWin -= NextLevelFunc;
    }

#if UNITY_EDITOR
    [Button]
    private void OverrideLoadLevels()
    {
        levels = Resources.LoadAll<GameObject>("Levels");
    }
#endif
}
