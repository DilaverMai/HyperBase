using System.Collections.Generic;
using UnityEngine;

public class CharacterDropItem : CharacterSetup
{
    public List<DropItem> DropList = new List<DropItem>();

    protected override void OnStart()
    {
    }

    protected override void OnUpdate()
    {
    }

    private void DropRandomItem()
    {
        var lucky = Random.Range(0, 11);
        List<DropItem> dropList = new List<DropItem>();

        foreach (var dropItem in DropList)
        {
            if (dropItem.Lucky < lucky)
            {
                dropList.Add(dropItem);
            }
        }

        if (dropList.Count > 0)
            dropList[Random.Range(0, dropList.Count)].DropTheItem(transform.position);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _character.OnDeath += DropRandomItem;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _character.OnDeath -= DropRandomItem;
    }
}