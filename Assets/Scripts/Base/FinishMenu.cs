using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Enums;

public class FinishMenu : BaseMenu
{
    public Button thisButton;
    public TextMeshProUGUI FinishText;
    public Image WinEmoji, LoseEmoji;
    private TextMeshProUGUI buttonText;
    protected override void Awake()
    {
        thisButton = GetComponentInChildren<Button>();
        buttonText = thisButton.GetComponentInChildren<TextMeshProUGUI>();
        base.Awake();
    }

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
                EventManager.WhenLose?.Invoke();
                buttonText.text = "Try Again";
                thisButton.onClick.RemoveAllListeners();
                thisButton.onClick.AddListener(() =>
                {
                    EventManager.RestartLevel?.Invoke();
                    Hide();
                });
                break;
            case GameStat.Win:
                FinishText.text = "YOU WIN";
                FinishText.color = Color.green;
                WinEmoji.gameObject.SetActive(true);
                EventManager.WhenWin?.Invoke();
                buttonText.text = "Next Level";
                thisButton.onClick.RemoveAllListeners();
                thisButton.onClick.AddListener(() =>
                {
                    EventManager.NextLevel?.Invoke();
                    Hide();
                });
                break;
        }

        BG.gameObject.SetActive(true);
    }

}
