using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenu : MonoBehaviour
{
    private Transform BG;

    protected virtual void Awake() {
        BG = transform.Find("BG");

        BG.gameObject.SetActive(false);
    }

    public void OpenCloseMenu(bool _open) {
        BG.gameObject.SetActive(_open);
    }
}
