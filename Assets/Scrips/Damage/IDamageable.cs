using System;

namespace ElementsArena.Damage
{
    public interface IDamageable
    {
        float life { get; }
        event Action DeathEvent;
        void TakeDamage(float damage);
    }
}
