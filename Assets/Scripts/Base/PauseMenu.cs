using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    public Button ShakeButton;
    public Button SoundButton;
    public Button ExitButton;

    private void Start() {
        ShakeButton.onClick.AddListener(ShakeButtonFunc);
        SoundButton.onClick.AddListener(SoundButtonFunc);
        ExitButton.onClick.AddListener(ExitButtonFunc);
    }

    private void ShakeButtonFunc()
    {

    }

    private void SoundButtonFunc()
    {

    }

    private void ExitButtonFunc()
    {
        EventManager.OnPause.Invoke(false);
    }
}
