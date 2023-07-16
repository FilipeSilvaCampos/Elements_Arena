using UnityEngine;

namespace ElementsArena.Movement
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] float maxSpeed = 2;
        [SerializeField] float acceleration = 15;

        [Header("Rotation Movement")]
        [SerializeField] Transform cameraTarget;
        [SerializeField] float cameraSpeed = 5;

        [Header("Ground Check")]
        [SerializeField] float playerHeith;
        [SerializeField] LayerMask whatIsGround;

        bool available = true;
        bool limiteSpeed = true;
        protected Rigidbody characterRb;

        protected Vector2 cameraInput;
        protected Vector3 moveInput;
        public bool grounded { get; protected set; }

        protected virtual void Awake()
        {
            characterRb = GetComponent<Rigidbody>();
        }

        protected virtual void Update()
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeith * 0.5f + 0.2f, whatIsGround);
        }

        protected virtual void FixedUpdate()
        {
            CameraMovement();
            GroundMovement();
        }

        private void GroundMovement()
        {
            if (!available) return;

            if (grounded)
            {
                characterRb.AddRelativeForce(moveInput * acceleration, ForceMode.Force);

                Vector3 limitedSpeed;
                if (limiteSpeed && characterRb.velocity.magnitude > maxSpeed)
                {
                    limitedSpeed = characterRb.velocity.normalized * maxSpeed;
                    characterRb.velocity = new Vector3(limitedSpeed.x, characterRb.velocity.y, limitedSpeed.z);
                }
            }
        }

        private void CameraMovement()
        {
            transform.eulerAngles += Vector3.up * cameraInput.x * cameraSpeed;
            cameraTarget.eulerAngles += Vector3.right * cameraInput.y * cameraSpeed;
        }

        public void SetLimiter(bool value)
        {
            limiteSpeed = value;
        }

        public void SetAvailable(bool value)
        {
            available = value;
        }

        public void SetInput(Vector2 input)
        {
            moveInput = new Vector3(input.x, 0, input.y);
        }

        public void SetCameraInput(Vector2 input)
        {
            cameraInput = input;
        }

        public Vector3 GetDirection()
        {
            return moveInput;
        }

        public void BreakMovement()
        {
            characterRb.velocity = new Vector3(0, 0, 0);
        }
    }
}
