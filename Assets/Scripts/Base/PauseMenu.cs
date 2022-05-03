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
        Haptics.hapticOff = !Haptics.hapticOff;
        if (Haptics.hapticOff)
            ShakeButton.GetComponent<Image>().color = Color.gray;
        else
            ShakeButton.GetComponent<Image>().color = Color.white;
    }

    private void SoundButtonFunc()
    {

    }

    private void ExitButtonFunc()
    {
        EventManager.OnPause.Invoke(false);
    }
}
