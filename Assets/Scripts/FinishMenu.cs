using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Enums;

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
    private void OpenScreen(GameStat stat)
    {
        WinEmoji.gameObject.SetActive(false);
        LoseEmoji.gameObject.SetActive(false);
        switch (stat)
        {
            case GameStat.Lose:
                FinishText.text = "YOU LOSE";
                FinishText.color = Color.red;
                LoseEmoji.gameObject.SetActive(true);
                break;
            case GameStat.Win:
                FinishText.text = "YOU WIN";
                FinishText.color = Color.green;
                WinEmoji.gameObject.SetActive(true);
                break;
        }

        BG.gameObject.SetActive(true);
    }
}
