using UnityEngine;

namespace ElementsArena.Combat
{
    [CreateAssetMenu(fileName = "Fire Projectile", menuName = "Make New Fire Projectile", order = 0)]
    public class FireScrtiptableProjectile : ScriptableObject
    {
        public GameObject prefab;
        public float launchTime = 0.5f;
    }
}
