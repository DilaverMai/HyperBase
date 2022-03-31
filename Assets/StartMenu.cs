
using UnityEngine;
using TMPro;
using DG.Tweening;
public class StartMenu : MonoBehaviour
{
    public TextMeshProUGUI StartText;
    private Transform BG;

    private void Awake()
    {
        BG = transform.GetChild(0);
        StartText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartTextAnim();
    }

    private void OnEnable()
    {
        EventManager.WhenStartGame += CloseMenu;
    }

    private void OnDisable()
    {
        EventManager.WhenStartGame -= CloseMenu;
    }

    private void CloseMenu(){
        BG.gameObject.SetActive(false);
    }

    private void StartTextAnim()
    {
        StartText.transform.DOScale(Vector3.one * 0.99F, 0.5F).
        SetLoops(-1, LoopType.Yoyo);
    }


}
