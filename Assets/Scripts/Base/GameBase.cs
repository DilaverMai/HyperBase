using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Collections.Generic;
public class GameBase : MonoBehaviour
{
    public static GameBase Instance;
    [HideInInspector]
    public DataManager DataManager;
    [HideInInspector]
    public LevelManager LevelManager;
    [HideInInspector]
    public MenuManager MenuManager;
    [HideInInspector]
    public PoolManager PoolManager;
    public GameStat _GameStat => gameStat;
    private GameStat gameStat;
    public List<Action> OnFail = new List<Action>();
    public List<Action> OnWin = new List<Action>();
    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DataManager = GetComponent<DataManager>();
        LevelManager = GetComponent<LevelManager>();
        PoolManager = GetComponent<PoolManager>();

        gameStat = GameStat.Start;
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        await Task.Delay(100);
        MenuManager = GetComponent<MenuManager>();
        MenuManager.Setup();

        //First DataManager
        await DataManager.CheckSave();
        LevelManager.LoadLevel();
        PoolManager.StartPool();
    }

    public void ChangeStat(GameStat stat)
    {
        gameStat = stat;
    }

    private void StartGame()
    {
        gameStat = GameStat.Playing;
    }

    private void OnEnable()
    {
        EventManager.FirstTouch += StartGame;
        EventManager.BeforeFinishGame += ClearActions;
    }

    private void OnDisable()
    {
        EventManager.FirstTouch -= StartGame;
        EventManager.BeforeFinishGame -= ClearActions;
    }

    private void ClearActions(GameStat stat)
    {
        if (stat == GameStat.Win)
            foreach (var item in OnWin)
                item.Invoke();
        else
            foreach (var item in OnFail)
                item.Invoke();


        OnFail.Clear();
        OnWin.Clear();
    }
}

public static class EventManager
{
    public static Action<GameStat> BeforeFinishGame;
    public static Action<GameStat> FinishGame;
    public static Action NextLevel;
    public static Action RestartLevel;
    public static Action FirstTouch;
    public static Action<bool> OnPause;
    public static Action OnBeforeLoadedLevel;
    public static Action OnAfterLoadedLevel;
}

public static class Base
{
    public static Transform GetLevelHolder()
    {
        return GameBase.Instance.LevelManager.LevelHolder;
    }
    public static bool IsPlaying()
    {
        return GameBase.Instance._GameStat == GameStat.Playing;
    }

    public async static void FinisGame(GameStat gameStat, float time = 0f)
    {
        if (GameBase.Instance._GameStat == GameStat.Playing) GameBase.Instance.ChangeStat(gameStat);
        EventManager.BeforeFinishGame?.Invoke(gameStat);
        await Task.Delay((int)time * 1000);
        if (!Application.isPlaying) return;
        EventManager.FinishGame?.Invoke(gameStat);
    }

    public static void StartGameAddFunc(Action func)
    {
        EventManager.FirstTouch += func;
    }

    public static void NextLevelAddFunc(Action func)
    {
        EventManager.NextLevel += func;
    }

    public static void RestartLevelAddFunc(Action func)
    {
        EventManager.RestartLevel += func;
    }

    public static void FinishGameAddFunc(Action<GameStat> func)
    {
        EventManager.FinishGame += func;
    }

    public static void WinTimeAddFunc(Action func)
    {
        GameBase.Instance.OnWin.Add(func);
    }

    public static void FailTimeAddFunc(Action func)
    {
        GameBase.Instance.OnFail.Add(func);
    }

}
