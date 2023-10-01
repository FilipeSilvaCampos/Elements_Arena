using ElementsArena.Movement;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class FireThrow : Ability
    {
        [Header("Fly Details")]
        [SerializeField] float flyMaxSpeed = 5;
        [SerializeField] float flyAcceleration = 15;
        [SerializeField] float flyHeight = 3;
        [SerializeField] float initialBreathCost = 10;
        [SerializeField] float breathCostPerSecond = 1;

        [SerializeField] LayerMask groundLayer;
        [SerializeField] GameObject throwEffect;
        [SerializeField] Transform footTransform;

        FireBreath breath;
        Animator animator;
        Rigidbody characterRb;
        CharacterMovement characterMovement;

        protected override void Awake()
        {
            base.Awake();

            breath = GetComponent<FireBreath>();
            characterRb = GetComponent<Rigidbody>();
            characterMovement = GetComponent<CharacterMovement>();
            animator = GetComponentInChildren<Animator>();
        }

        private void LateUpdate()
        {
            if (currentState == AbilityStates.active) FlyMovement();
        }

        protected override void OnReady()
        {
            if (called && breath.breatAmount > initialBreathCost)
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

            if (IsTimeToChangeState() || !breath.TakeBreath(breathCostPerSecond * Time.deltaTime))
            {
                characterRb.useGravity = true;
                animator.SetBool(AnimationKeys.FlyBool, false);
                FinishState();
            }
        }

        protected override void OnCooldown()
        {
            if (IsTimeToChangeState()) FinishState();
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
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = transform.position;
            targetPosition.y = GroundHeight() + flyHeight;
            
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, flyAcceleration * Time.deltaTime);
        }
    }
}
