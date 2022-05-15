using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    public static FinishLine Instance;
    [SerializeField]
    private float zOffset;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else    
            Destroy(gameObject);
    }

    private void Start()
    {
        var texts = GetComponentsInChildren<TextMeshPro>();

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = FinishLineColors.multiplier[i];
            texts[i].transform.parent.GetComponent<MeshRenderer>().material.color = FinishLineColors.colors[i];
        }
    }


    void Finish()
    {
        Debug.Log("FinishLine");
        Base.ChangeStat(GameStat.FinishLine);
    }


    void OnEnable()
    {
        EventManager.FinishLine += Finish;
    }


    void OnDisable()
    {
        EventManager.FinishLine -= Finish;
        Instance = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            EventManager.FinishLine.Invoke();
        }
    }
}
public static class FinishLineColors
{
    public static Color[] colors = new Color[]
    {
        new Color32(15, 38, 166, 255),
        new Color32(242,114,70,95),
        new Color32(46,75,242,255),
        new Color32(117,242,22,255),
        new Color32(85,166,23,65),
        new Color32(0,239,244,255),
        new Color32(245,24,179,255),
        new Color32(1,245,50,255),
        new Color32(107,24,245,255),
        new Color32(245,144,24,255)
    };

    public static string[] multiplier = new string[]
    {
        "x1",
        "x1.1",
        "x1.2",
        "x1.3",
        "x1.4",
        "x1.5",
        "x1.6",
        "x1.7",
        "x1.8",
        "x1.9",
        "x2"
    };
}
