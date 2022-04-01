using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public StartMenu StartMenu;
    public PlayTimeMenu PlayTimeMenu;
    public PauseMenu PauseMenu;
    public FinishMenu FinishMenu;

    public void Setup() {
        StartMenu = FindObjectOfType<StartMenu>();
        PlayTimeMenu = FindObjectOfType<PlayTimeMenu>();
        PauseMenu = FindObjectOfType<PauseMenu>();
        FinishMenu = FindObjectOfType<FinishMenu>();
    }
}
