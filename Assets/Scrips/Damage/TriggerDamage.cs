using UnityEngine;

namespace ElementsArena.Damage
{
    public class TriggerDamage : MonoBehaviour
    {
        [SerializeField] float damage;

        public float Damage { get { return damage; } set { damage = value; } }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
