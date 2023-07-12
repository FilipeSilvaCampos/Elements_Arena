using UnityEngine;

namespace ElementsArena.Damage
{
    public class TriggerDamage : MonoBehaviour
    {
        int damage;

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
