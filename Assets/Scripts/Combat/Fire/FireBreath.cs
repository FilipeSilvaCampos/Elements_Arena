using System.Collections;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class FireBreath : MonoBehaviour
    {
        [SerializeField] float maxBreathAmount = 100;
        [SerializeField] float recoveryAmount = .5f;

        public float breatAmount;

        private void Awake()
        {
            breatAmount = maxBreathAmount;
        }

        private void Update()
        {
            if (breatAmount < maxBreathAmount)
                breatAmount += recoveryAmount * Time.deltaTime;
        }

        public bool TakeBreath(float amountToTake)
        {
            if(amountToTake > breatAmount) return false;

            breatAmount -= amountToTake;
            return true;
        }

        public float GetFraction()
        {
            return breatAmount / maxBreathAmount;
        }
    }
}