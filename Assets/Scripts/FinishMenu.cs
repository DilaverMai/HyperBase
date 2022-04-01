using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FinishMenu : BaseMenu
{
    public Transform BG;
    public TextMeshProUGUI FinishText;
    public Image WinEmoji, LoseEmoji;

    private void OnEnable()
    {
        EventManager.FinishGame += OpenScreen;
    }

    private void OnDisable()
    {
        EventManager.FinishGame -= OpenScreen;
    }
    private void OpenScreen(GameBase.GameStat stat)
    {
        WinEmoji.gameObject.SetActive(false);
        LoseEmoji.gameObject.SetActive(false);
        switch (stat)
        {
            case GameBase.GameStat.Lose:
                FinishText.text = "YOU LOSE";
                FinishText.color = Color.red;
                LoseEmoji.gameObject.SetActive(true);
                break;
            case GameBase.GameStat.Win:
                FinishText.text = "YOU WIN";
                FinishText.color = Color.green;
                WinEmoji.gameObject.SetActive(true);
                break;
        }

        BG.gameObject.SetActive(true);
    }
}
