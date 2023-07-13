using ElementsArena.Damage;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class RockBehaviour : MonoBehaviour
    {
        [SerializeField] float launchSpeed = 2;
        [SerializeField] float damage = 10;
        [SerializeField] GameObject destroyEffect;

        bool launched = false;
        private void Update()
        {
            if (!launched) return;

            transform.Translate(Vector3.forward * launchSpeed * Time.deltaTime);
        }

        public void Launch()
        {
            GetComponent<TriggerDamage>().Damage = damage;
            launched = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (launched)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}