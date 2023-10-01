using ElementsArena.Movement;
using UnityEngine;

namespace ElementsArena.Combat
{
    public enum AbilityStates
    {
        ready,
        active,
        cooldown
    }

    [RequireComponent(typeof(AbilityHolder))]
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] protected float activeTime = 1;
        [SerializeField] protected float cooldownTime = 1.5f;
        [SerializeField] private bool isPrimary = false;

        public float CooldownTime { get { return cooldownTime; } }
        float timeSinceLastStateChange;
        protected AbilityStates currentState { get; private set; }
        public bool called;
        protected AbilityHolder abilityHolder;

        protected virtual void Awake()
        {
            abilityHolder = GetComponent<AbilityHolder>();
        }

        protected virtual void Update()
        {
            switch(currentState)
            {
                case AbilityStates.ready:
                    if (isPrimary && abilityHolder.usingPrimary) break;
                    OnReady();
                    break;
                case AbilityStates.active:
                    OnActive();
                    break;
                case AbilityStates.cooldown:
                    OnCooldown();
                    break;
            }
            timeSinceLastStateChange += Time.deltaTime;
        }

        protected abstract void OnReady();
        protected abstract void OnActive();
        protected abstract void OnCooldown();

        protected void FinishState()
        {
            timeSinceLastStateChange = 0;
            switch (currentState)
            {
                case AbilityStates.ready:
                    currentState = AbilityStates.active;
                    if (isPrimary)
                    {
                        abilityHolder.usingPrimary = true;
                    }
                    break;
                case AbilityStates.active:
                    currentState = AbilityStates.cooldown;
                    abilityHolder.usingPrimary = false;
                    break;
                case AbilityStates.cooldown:
                    called = false;
                    currentState = AbilityStates.ready;
                    break;
            }
        }

        protected bool IsTimeToChangeState()
        {
            switch (currentState)
            {
                case AbilityStates.ready:
                    return false;
                case AbilityStates.active:
                    if (timeSinceLastStateChange > activeTime) return true;
                    else return false;
                case AbilityStates.cooldown:
                    if (timeSinceLastStateChange > cooldownTime) return true;
                    else return false;
            }
            return false;
        }

        protected void LockCharacterMovement()
        {
            CharacterMovement characterMovement = GetComponent<CharacterMovement>();
            characterMovement.LockMovement(true);
            characterMovement.BreakMovement();
        }

        protected void UnlockCharacterMovement()
        {
            CharacterMovement characterMovement = GetComponent<CharacterMovement>();
            characterMovement.LockMovement(false);
        }

        public float GetCooldownTimer()
        {
            if (currentState != AbilityStates.cooldown) return cooldownTime;

            return cooldownTime - timeSinceLastStateChange;
        }
    }
}
