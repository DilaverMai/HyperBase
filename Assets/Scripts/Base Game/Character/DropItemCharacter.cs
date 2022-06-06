using System.Collections.Generic;
public class DropItemCharacter : CharacterSetup
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
        DropList.DropRandomItem(transform.position);
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
