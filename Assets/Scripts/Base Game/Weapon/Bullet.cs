public class Bullet : Damage
{
    protected override void Contact(CharacterHealth health)
    {
        base.Contact(health);
        gameObject.SetActive(false);
    }
}
