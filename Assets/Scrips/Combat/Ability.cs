using UnityEngine;

namespace ElementsArena.Combat
{
    public enum AbilityStates
    {
        ready,
        active,
        cooldown
    }

    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] float activeTime = 1;
        [SerializeField] float cooldownTime = 1.5f;

        float timeSinceLastChangeState;
        protected AbilityStates currentState;
        public bool called;

        protected virtual void Update()
        {
            switch(currentState)
            {
                case AbilityStates.ready:
                    OnReady();
                    break;
                case AbilityStates.active:
                    OnActive();
                    break;
                case AbilityStates.cooldown:
                    OnCooldown();
                    break;
            }
            timeSinceLastChangeState += Time.deltaTime;
        }

        protected abstract void OnReady();
        protected abstract void OnActive();
        protected abstract void OnCooldown();

        protected void FinishState()
        {
            timeSinceLastChangeState = 0;
            switch (currentState)
            {
                case AbilityStates.ready:
                    currentState = AbilityStates.active;
                    break;
                case AbilityStates.active:
                    currentState = AbilityStates.cooldown;
                    break;
                case AbilityStates.cooldown:
                    currentState = AbilityStates.ready;
                    break;
            }
        }

        protected bool TimeToChangeState()
        {
            switch (currentState)
            {
                case AbilityStates.ready:
                    return false;
                case AbilityStates.active:
                    if (timeSinceLastChangeState > activeTime) return true;
                    else return false;
                case AbilityStates.cooldown:
                    if (timeSinceLastChangeState > cooldownTime) return true;
                    else return false;
            }
            return false;
        }
    }
}
