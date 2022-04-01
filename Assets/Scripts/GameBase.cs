using System;
using System.Collections;
using System.Collections.Generic;
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

    private void StartGame(){
        gameStat = GameStat.Playing;
    }

    private void OnEnable() {
        EventManager.WhenStartGame += StartGame;
    }

    private void OnDisable() {
        EventManager.WhenStartGame -= StartGame;
    }
}

public static class EventManager
{
    public static Action NextLevel;
    public static Action RestartLevel;
    public static Action<GameStat> FinishGame;
    public static Action WhenStartGame;
}

public static class Base { 
    public static bool IsPlaying()
    {
        return GameBase.Instance._GameStat == GameStat.Playing;
    }
}
