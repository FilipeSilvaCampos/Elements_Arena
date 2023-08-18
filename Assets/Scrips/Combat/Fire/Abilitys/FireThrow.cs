using ElementsArena.Movement;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class FireThrow : Ability
    {
        [SerializeField] float flyMaxSpeed = 5;
        [SerializeField] float flyAcceleration = 15;
        [SerializeField] float flyHeight = 3;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] GameObject throwEffect;
        [SerializeField] Transform footTransform;

        Animator animator;
        Rigidbody characterRb;
        CharacterMovement characterMovement;

        private void Awake()
        {
            characterRb = GetComponentInChildren<Rigidbody>();
            characterMovement = GetComponentInChildren<CharacterMovement>();
            animator = GetComponentInChildren<Animator>();
        }

        private void LateUpdate()
        {
            if (currentState == AbilityStates.active) FlyMovement();
        }

        protected override void OnReady()
        {
            if (called)
            {
                animator.SetBool(AnimationKeys.FlyBool, true);
                Destroy(Instantiate(throwEffect, footTransform), activeTime);
                characterRb.useGravity = false;
                FinishState();
            }
        }

        protected override void OnActive()
        {
            ControlHeight();

            if (TimeToChangeState())
            {
                characterRb.useGravity = true;
                animator.SetBool(AnimationKeys.FlyBool, false);
                FinishState();
            }
        }

        protected override void OnCooldown()
        {
            if (TimeToChangeState()) FinishState();
        }

        private float GroundHeight()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);
            Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer);

            return hit.point.y;
        }

        private void FlyMovement()
        {
            Vector3 currentVelocity = characterRb.velocity;
            Vector3 targetSpeed = characterMovement.GetDirection() * flyMaxSpeed;
            characterRb.velocity = Vector3.MoveTowards(currentVelocity, targetSpeed, flyAcceleration * Time.deltaTime);
        }

        private void ControlHeight()
        {
            if(transform.position.y - GroundHeight() < flyHeight)
            {
                transform.position += Vector3.up * flyMaxSpeed * Time.deltaTime;
            }

            if(transform.position.y - GroundHeight() > flyHeight)
            {
                transform.position += Vector3.down * flyMaxSpeed * Time.deltaTime;
            }
        }
    }
}
