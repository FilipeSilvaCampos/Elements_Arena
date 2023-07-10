using UnityEngine;

namespace ElementsArena.Movement
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] float maxSpeed = 2;
        [SerializeField] float acceleration = 15;

        [Header("Camera Movement")]
        [SerializeField] Transform cameraTarget;
        [SerializeField] float cameraSpeed = 0.5f;

        [Header("Ground Check")]
        [SerializeField] float playerHeith;
        [SerializeField] LayerMask whatIsGround;

        bool limiteSpeed;
        protected Rigidbody characterRb;

        protected Vector2 cameraInput;
        protected Vector3 direction;
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
            if (grounded)
            {
                characterRb.AddRelativeForce(direction * acceleration, ForceMode.Force);

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

        public void SetDirection(Vector3 direction)
        {
            this.direction = direction;
        }

        public void SetCameraInput(Vector2 input)
        {
            cameraInput = input;
        }

        public Vector3 GetDirection()
        {
            return direction;
        }

        public void BreakMovement()
        {
            characterRb.velocity = new Vector3(0, 0, 0);
        }
    }
}
