using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public StartMenu StartMenu;
    public PlayTimeMenu PlayTimeMenu;
    public PauseMenu PauseMenu;
    public FinishMenu FinishMenu;
    public static MenuManager Instance;
    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    public void Setup() {
        StartMenu = FindObjectOfType<StartMenu>();
        PlayTimeMenu = FindObjectOfType<PlayTimeMenu>();
        PauseMenu = FindObjectOfType<PauseMenu>();
        FinishMenu = FindObjectOfType<FinishMenu>();
    }


    private void OnEnable() {
        EventManager.WhenStartGame += ShowPlayTimeMenu;
        EventManager.OnPause += Pause;
        EventManager.FinishGame += WhenFinish;
        EventManager.OnAfterLoadedLevel += ShowStartMenu;
    }

    private void OnDisable() {
        EventManager.WhenStartGame -= ShowPlayTimeMenu;
        EventManager.FinishGame -= WhenFinish;
        EventManager.OnPause -= Pause;
        EventManager.OnAfterLoadedLevel -= ShowStartMenu;
    }
    public void ShowStartMenu() {
        StartMenu.Show();
    }

    public void ShowPlayTimeMenu() {
        PlayTimeMenu.Show();
    }

    private void WhenFinish(Enums.GameStat stat)
    {
        StartMenu.Hide();
        PlayTimeMenu.Hide();
        PauseMenu.Hide();
    }

    private void Pause(bool pause){
        if(pause) {
            GameBase.Instance.ChangeStat(Enums.GameStat.Paused);
            Time.timeScale = 0;
            PauseMenu.Show();
        } else {
            GameBase.Instance.ChangeStat(Enums.GameStat.Playing);
            Time.timeScale = 1;
            PauseMenu.Hide();
        }
    }
}
