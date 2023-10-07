using Photon.Pun;
using System;
using UnityEngine;

namespace ElementsArena.Damage
{
    public class OnTakeDamage : MonoBehaviour, IDamageable
    {
        [SerializeField] float maxLife;
        [field: SerializeField] public float life { get; private set; } // Just for visualize in editor

        public event Action OnDeath;

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
            life = Mathf.Max(life - damage, 0);

            if (life == 0)
            {
                OnDeath.Invoke();
            }
        }

        public float GetFraction()
        {
            return life / maxLife;
        }
    }
}
