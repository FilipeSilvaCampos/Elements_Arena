using System;
using UnityEngine;

namespace ElementsArena.Damage
{
    public class OnTakeDamage : MonoBehaviour, IDamageable
    {
        [SerializeField] float maxLife;
        public float life { get; private set; }

        public event Action DeathEvent;

        private void Start()
        {
            life = maxLife;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                TakeDamage(15.5f);
            }
        }

        public void TakeDamage(float damage)
        {
            life -= damage;
            if (life > 0)
            {
                return;
            }

            DeathEvent.Invoke();
        }
    }
}
