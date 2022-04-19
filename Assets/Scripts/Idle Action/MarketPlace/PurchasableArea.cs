using System.Collections;
using UnityEngine;
using TMPro;
public class PurchasableArea : MonoBehaviour
{
    public string AreaName;
    [SerializeField]
    private int price;
    private int depositPrice;
    [SerializeField]
    private bool bought;

    public int Price => Price;
    public int DepositPrice => depositPrice;
    public bool Bought => bought;

    private Collider _collider;
    public GameObject CloseType;
    public GameObject OpenType;
    private TextMeshPro priceText;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        priceText = GetComponentInChildren<TextMeshPro>();
    }

    public void Setup(bool _bought,int _price)
    {
        bought = _bought;
        depositPrice = _price;

        CloseType.SetActive(false);
        OpenType.SetActive(false);

        if (!bought)
        {
            CloseType.SetActive(true);
            priceText.text = price+"/"+depositPrice;
        }
        else
        {
            _collider.enabled = false;
            OpenType.SetActive(true);
            priceText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerControllerJoyStick>(out PlayerControllerJoyStick playerControllerJoyStick))
        {
            Debug.Log("Starting Buy");
            if (!bought)
            {
                StartCoroutine("MoneyTransfer");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerControllerJoyStick>(out PlayerControllerJoyStick playerControllerJoyStick))
        {
            Debug.Log("Stoptted Buy");
            StopCoroutine("MoneyTransfer");
        }
    }

    private IEnumerator MoneyTransfer()
    {
        if (bought) yield break;

        while (true)
        {
            AddDeposit(1);
            priceText.text = price+"/"+depositPrice;
            if (bought) break;
            yield return new WaitForSeconds(0.01f);
        }

    }

    public virtual void AddDeposit(int v)
    {
        depositPrice += v;

        if (depositPrice >= price)
        {
            depositPrice = 0;
            Buy();
        }
    }

    public virtual void Buy()
    {
        CloseType.SetActive(false);
        OpenType.SetActive(true);
        priceText.gameObject.SetActive(false);
        _collider.enabled = false;
        bought = true;
        Debug.Log("Buy");
    }
}
