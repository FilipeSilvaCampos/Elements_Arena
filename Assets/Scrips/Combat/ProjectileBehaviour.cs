using ElementsArena.Damage;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField] float speed = 1.8f;
        [SerializeField] float lifeTime = 3;
        [SerializeField] GameObject impactEffect;
        [SerializeField] GameObject[] destroyOnHit;
        [SerializeField] float lifeAfterImpact = 0.2f;
        [SerializeField] bool destroyOnCollide = false;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision) 
        {
            Debug.Log("Collide");
            if(destroyOnCollide)
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            foreach(GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject, lifeAfterImpact);
        }
    }
}