using Unity.VisualScripting;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class WaterSource : MonoBehaviour
    {
        [SerializeField] float maxAmount = 100;
        [SerializeField] float timeRefil = 2;

        [field: SerializeField] public float amount { get; private set; }

        private void Start()
        {
            amount = maxAmount;
        }

        private void Update()
        {
            amount = Mathf.Min(maxAmount, amount + timeRefil * Time.deltaTime);    
        }

        public bool TakeWater(float amountToTake)
        {
            if(amountToTake > amount) return false;

            amount -= amountToTake;
            return true;
        }
    }
}