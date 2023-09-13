using ElementsArena.Damage;
using ElementsArena.Movement;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class WaterProjectile : ProjectileBehaviour
    {
        [Header("Frozen Attributes")]
        [SerializeField] GameObject frozenVersion;
        [SerializeField] GameObject frozenEffect;

        public void Frozen()
        {
            Instantiate(frozenEffect, transform.position, transform.rotation);
            Instantiate(frozenVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}