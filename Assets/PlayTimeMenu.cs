using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayTimeMenu : MonoBehaviour
{
    private Transform BG;
    public Button PauseButton;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI GoldText;

    private void Awake() {
        BG = transform.GetChild(0);
    }


}
