using UnityEngine;

namespace ElementsArena.Combat
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField] float speed = 1.8f;
        [SerializeField] float damage = 5;
        [SerializeField] float lifeTime = 3;
        [SerializeField] GameObject[] destroyOnHit;
        [SerializeField] float lifeAfterImpact = 0.2f;

        private void Start()
        {
            Destroy(gameObject, lifeTime);    
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            //TODO damage
            foreach(GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }
            Destroy(gameObject, lifeAfterImpact);
        }
    }
}