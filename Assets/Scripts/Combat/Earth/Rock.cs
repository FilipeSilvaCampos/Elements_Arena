using ElementsArena.Damage;
using System;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class Rock : MonoBehaviour, IDamageable
    {
        [SerializeField] float durability = 20;
        [SerializeField] float launchSpeed = 2;
        [SerializeField] float damage = 10;
        [SerializeField] GameObject destroyEffect;

        public float showd;

        public float life { get; private set; }
        public event Action OnDeath;
        bool launched = false;

        private void Awake()
        {
            life = durability;
        }

        private void Update()
        {
            showd = life;
            if (!launched) return;

            transform.Translate(Vector3.forward * launchSpeed * Time.deltaTime);
        }

        public void Launch(IDamageable instigator)
        {
            GetComponentInChildren<TriggerDamage>().SetInstigator(instigator);
            launched = true;
            GetComponentInChildren<TriggerDamage>().Damage = damage;
            Destroy(gameObject, 20 /*Time to dont live eternaly*/);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (launched)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }   
        }

        public void TakeDamage(float damage)
        {
            Debug.Log("Take Damage was called");
            life = Mathf.Max(life - damage, 0);

            if(life == 0)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        public float GetFraction()
        {//Not implemented
            return 0;
        }
    }
}