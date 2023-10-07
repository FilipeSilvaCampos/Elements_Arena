using UnityEngine;

namespace ElementsArena.Core
{
    public class DestroyAffterEffect : MonoBehaviour
    {
        [SerializeField] GameObject toDestroy;

        private void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive()) Destroy(toDestroy);
        }
    }
}
