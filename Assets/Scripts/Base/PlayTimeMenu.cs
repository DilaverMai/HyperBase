using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayTimeMenu : BaseMenu
{
    public Button PauseButton;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI GoldText;
    public Image GoldImage;

    private void Start() {
        PauseButton.onClick.AddListener(PauseButtonFunc);
    }

    private void PauseButtonFunc()
    {
        EventManager.OnPause?.Invoke(true);
    }

    private void OnEnable() {
        DataManager.OnSetData += SetDatas;
    }

    private void OnDisable() {
        DataManager.OnSetData -= SetDatas;
    }

    private void SetDatas(int level, int gold) {
        LevelText.text = "LEVEL " + level;
        GoldText.text = gold.ToString();
    }
}
