using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Contactable
{
    protected override void Contant(GameObject _gObject)
    {
        Datas.Coin.CoinAdd(Value);
        if (AfterDestory)
            Destroy(gameObject);
    }
}
