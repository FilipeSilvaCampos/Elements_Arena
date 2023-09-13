using UnityEngine;

namespace ElementsArena.Combat
{
    public class WaterSource : MonoBehaviour
    {
        [field : SerializeField] public float amount { get; private set; } = 100;

        public bool TakeWater(float amountToTake)
        {
            if(amountToTake > amount) return false;

            amount -= amountToTake;
            return true;
        }
    }
}