using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button ShakeButton;
    public Button SoundButton;
    public Button ExitButton;
    public Transform BG;

    private void Awake() {
        BG = transform.GetChild(0);
    }
}
