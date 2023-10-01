using ElementsArena.Damage;
using UnityEngine;

namespace ElementsArena.Combat
{
    [CreateAssetMenu(fileName = "Fire Projectile", menuName = "Make New Fire Projectile", order = 0)]
    public class FireScrtiptableProjectile : ScriptableObject
    {
        public GameObject prefab;
        public float launchTime = 0.5f;
        public float breathCost = 10;

        public void Lauch(IDamageable instigator, Vector3 position, Quaternion rotation)
        {
            ProjectileBehaviour projectile = Instantiate(prefab, position, rotation).GetComponent<ProjectileBehaviour>();

            projectile.SetInstigator(instigator);
        }
    }
}
