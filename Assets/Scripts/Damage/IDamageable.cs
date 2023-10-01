using System;

namespace ElementsArena.Damage
{
    public interface IDamageable
    {
        float life { get; }
        event Action OnDeath;
        void TakeDamage(float damage);

        float GetFraction();
    }
}
