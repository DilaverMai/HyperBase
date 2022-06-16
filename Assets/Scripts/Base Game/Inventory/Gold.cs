using UnityEngine;

public class Gold : Contactable
{
    public int GoldCount;
    protected override void Contant(GameObject _gObject)
    {
        base.Contant(_gObject);
        Datas.Coin.CoinAdd(GoldCount);
        ParticleExtension.PlayCoinEffect(transform.position, GoldCount);
    }
}
