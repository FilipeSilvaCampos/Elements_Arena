using UnityEngine;

namespace ElementsArena.Core
{
    public class DestroyAffterEffect : MonoBehaviour
    {
        private void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive()) Destroy(gameObject);
        }
    }
}
