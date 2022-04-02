using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using static Enums;
public class GameBase : MonoBehaviour
{
    public static GameBase Instance;

    [HideInInspector]
    public DataManager DataManager;
    [HideInInspector]
    public LevelManager LevelManager;
    [HideInInspector]
    public MenuManager MenuManager;
    public GameStat _GameStat => gameStat;
    private GameStat gameStat;

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

        gameStat = GameStat.Start;
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        await Task.Delay(100);
        MenuManager = GetComponent<MenuManager>();
        MenuManager.Setup();

        //First DataManager
        await DataManager.CheckSave();
        LevelManager.LoadLevel();
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
        EventManager.WhenStartGame += StartGame;
    }

    private void OnDisable()
    {
        EventManager.WhenStartGame -= StartGame;
    }
}

public static class EventManager
{
    public static Action WhenLose;
    public static Action WhenWin;
    public static Action NextLevel;
    public static Action RestartLevel;
    public static Action<GameStat> FinishGame;
    public static Action WhenStartGame;
    public static Action<int> OnLevelChanged;
    public static Action<bool> OnPause;
    public static Action OnBeforeLoadedLevel;
    public static Action OnAfterLoadedLevel;
}

public static class Base
{
    public static bool IsPlaying()
    {
        return GameBase.Instance._GameStat == GameStat.Playing;
    }

    public async static void FinisGame(GameStat gameStat, float time = 0f)
    {
        if (GameBase.Instance._GameStat == GameStat.Playing) GameBase.Instance.ChangeStat(gameStat);
        await Task.Delay((int)time * 1000);
        if (!Application.isPlaying) return;
        EventManager.FinishGame?.Invoke(gameStat);
    }

    public static void StartGameAddFunc(Action func)
    {
        EventManager.WhenStartGame += func;
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
}
