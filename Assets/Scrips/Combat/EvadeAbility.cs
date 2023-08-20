using ElementsArena.Movement;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class EvadeAbility : Ability
    {
        [SerializeField] float speed = 10;

        CharacterMovement characterMovement;
        Rigidbody characterRb;

        private void Awake()
        {
            characterMovement = GetComponentInChildren<CharacterMovement>();
            characterRb = GetComponentInChildren<Rigidbody>();
        }

        protected override void OnReady()
        {
            if (called)
            {
                characterRb.velocity = characterMovement.GetDirection().normalized * speed;

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
