using UnityEngine;

namespace ElementsArena.Combat
{
    public class RockBehaviour : MonoBehaviour
    {
        [SerializeField] float launchSpeed = 2;

        float speed = 0;
        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void Launch()
        {
            speed = launchSpeed;
        }
    }
}