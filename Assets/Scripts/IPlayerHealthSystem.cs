public interface IPlayerHealthSystem
{
    bool TakeDamage(int damage);
    bool IsDead();
    void OnDeath();
}
