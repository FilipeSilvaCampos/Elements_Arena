using System;
using UnityEngine;

namespace ElementsArena.Damage
{
    public class TriggerDamage : MonoBehaviour
    {
        [SerializeField] float damage;
        [SerializeField][Tooltip("To destory on OnDestroy()")] GameObject parent;

        IDamageable instigator;
        public float Damage { get { return damage; } set { damage = value; } }

        public void SetInstigator(IDamageable instigator)
        {
            this.instigator = instigator;
        }

        public IDamageable GetInstigator() { return instigator; }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null && damageable != instigator)
            {
                damageable.TakeDamage(damage);
            }
        }

        private void OnDestroy()
        {
            Destroy(parent);
        }
    }
}
