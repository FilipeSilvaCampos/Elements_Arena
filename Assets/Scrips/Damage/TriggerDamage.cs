using UnityEngine;

namespace ElementsArena.Damage
{
    public class TriggerDamage : MonoBehaviour
    {
        [SerializeField] float damage;

        IDamageable instigator;
        public float Damage { get { return damage; } set { damage = value; } }

        public void SetInstigator(IDamageable instigator)
        {
            this.instigator = instigator;
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null && damageable != instigator)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
