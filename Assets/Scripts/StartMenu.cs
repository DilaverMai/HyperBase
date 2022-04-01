
using UnityEngine;
using TMPro;
using DG.Tweening;
public class StartMenu : BaseMenu
{
    public TextMeshProUGUI StartText;
    private Transform BG;

    protected override void Awake()
    {
        BG = transform.Find("BG");
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
