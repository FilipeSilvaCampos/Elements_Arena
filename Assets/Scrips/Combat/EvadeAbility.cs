using ElementsArena.Movement;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class EvadeAbility : Ability
    {
        [SerializeField] float speed = 10;

        CharacterMovement characterMovement;
        Rigidbody characterRb;
        Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            characterMovement = GetComponent<CharacterMovement>();
            characterRb = GetComponent<Rigidbody>();
        }

        protected override void OnReady()
        {
            if (called)
            {
                characterRb.velocity = characterMovement.GetDirection().normalized.x * speed * transform.right;
                animator.SetTrigger(AnimationKeys.RollTrigger);

                FinishState();
            }
        }

        protected override void OnActive()
        {
            if(TimeToChangeState())
            {
                FinishState();
            }
        }

        protected override void OnCooldown()
        {
            if (TimeToChangeState()) FinishState();
        }
    }
}
