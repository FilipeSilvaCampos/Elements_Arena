using UnityEngine;

namespace ElementsArena.Combat
{
    [CreateAssetMenu(fileName = "Earth Attack", menuName = "Make New Earth Attack", order = 0)]
    public class EarthScriptableAttack : ScriptableObject
    {
        public GameObject prefab;
        public float elevateSpeed = 2;

        public Vector3 GetPrefabScale()
        {
            return prefab.transform.localScale;
        }
    }
}