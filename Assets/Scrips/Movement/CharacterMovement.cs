using UnityEngine;

public static class AnimationKeys
{
    public const string HorizontalFloat = "hSpeed";
    public const string VerticalFloat = "vSpeed";
    public const string FlyBool = "FlyBool";
    public const string LaunchTrigger = "LaunchTrigger";
    public const string RollTrigger = "RollTrigger";
}

namespace ElementsArena.Movement
{
    public struct CharacterMovementInput
    {
        public Vector2 MoveInput;
        public Quaternion LookRotation;
    }

    public class CharacterMovement : MonoBehaviour
    {
        [Header("Ground Movement")]
        [SerializeField] float maxSpeed = 2;
        [SerializeField] float acceleration = 15;
        [SerializeField] float rotationSpeed = 10;

        [Header("Ground Check")]
        [SerializeField] float playerHeith;
        [SerializeField] LayerMask whatIsGround;

        public bool grounded { get; private set; }
        bool moveLock = false;
        Animator animator;
        Vector3 moveInput;
        Rigidbody characterRb;

        private void Awake()
        {
            characterRb = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeith * 0.5f + 0.2f, whatIsGround);
            animator.SetFloat(AnimationKeys.VerticalFloat, characterRb.velocity.z);
            animator.SetFloat(AnimationKeys.HorizontalFloat, characterRb.velocity.x);
        }

        private void LateUpdate()
        {
            if (moveLock) return;

            UpdateRotation();
            if (grounded)
            {
                GroundMovement();
            }
        }

        public void SetInput(CharacterMovementInput input)
        {
            moveInput = new Vector3(input.MoveInput.x, 0, input.MoveInput.y);

            moveInput = input.LookRotation * moveInput;
            moveInput.y = 0;
            moveInput.Normalize();
        }

        void GroundMovement()
        {
            Vector3 currentVelocity = characterRb.velocity;
            Vector3 targetSpeed = moveInput * maxSpeed;
            characterRb.velocity = Vector3.MoveTowards(currentVelocity, targetSpeed, acceleration * Time.deltaTime);
        }

        void UpdateRotation()
        {
            if (moveInput == Vector3.zero) return;

            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        public Vector3 GetDirection()
        {
            return moveInput;
        }

        public void BreakMovement()
        {
            characterRb.velocity = Vector3.zero;
        }

        public void LockMovement(bool value)
        {
            moveLock = value;
        }
    }
}
