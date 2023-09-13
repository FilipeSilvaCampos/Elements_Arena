using ElementsArena.Damage;
using Photon.Pun;
using System.IO;
using UnityEngine;

namespace ElementsArena.Combat
{
    [CreateAssetMenu(fileName = "Fire Projectile", menuName = "Make New Fire Projectile", order = 0)]
    public class FireScrtiptableProjectile : ScriptableObject
    {
        public GameObject prefab;
        public float launchTime = 0.5f;
        public float breathCost = 10;

        public void Lauch(IDamageable instigator,  Transform spawnTransform)
        {
            ProjectileBehaviour projectile = PhotonNetwork.Instantiate(prefab.name, spawnTransform.position, spawnTransform.rotation).GetComponent<ProjectileBehaviour>();

            projectile.SetInstigator(instigator);
        }
    }
}
