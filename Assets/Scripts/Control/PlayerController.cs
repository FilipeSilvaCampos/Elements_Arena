using ElementsArena.Combat;
using ElementsArena.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ElementsArena.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float cameraSpeed = 1.5f;
        CharacterMovement characterMovement;
        CameraController cameraController;
        AbilityHolder abilityHolder;

        Vector2 moveInput = Vector2.zero;
        Vector2 cameraInput = Vector2.zero;
        bool available = false;

        public void SetUpController(CharacterMovement characterMovement, AbilityHolder abilityHolder, CameraController cameraController)
        {
            available = true;

            this.characterMovement = characterMovement;
            this.cameraController = cameraController;
            this.abilityHolder = abilityHolder;
        }

        private void Update()
        {
            if (!available) return;

            //Update move and camera inputs
            cameraController.IncrementCameraRotation(new Vector2(cameraInput.y, cameraInput.x) * cameraSpeed);
            characterMovement.SetInput(new CharacterMovementInput
            {
                MoveInput = moveInput,
                LookRotation = cameraController.LookRotation
            });
        }

        public void SetAvailable(bool value) => available = value;

        #region PlayerInput Callbacks
        public void SetMoveInput(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        public void SetCameraInput(InputAction.CallbackContext context)
        {
            cameraInput = context.ReadValue<Vector2>();
        }

        public void SetSelectionInput(InputAction.CallbackContext context)
        {
            if (!available) return;

            abilityHolder.selectionInput = context.ReadValue<Vector2>();
        }

        public void CallFirstAbility(InputAction.CallbackContext context)
        {
            if (!available) return;

            abilityHolder.firstAbility.called = context.ReadValueAsButton();
        }

        public void CallSecondAbility(InputAction.CallbackContext context)
        {
            if (!available) return;

            abilityHolder.secondAbility.called = context.ReadValueAsButton();
        }

        public void CallEvadeAbility(InputAction.CallbackContext context)
        {
            if (!available) return;

            abilityHolder.evadeAbility.called = context.ReadValueAsButton();
        }

        public void SupportButton(InputAction.CallbackContext context)
        {
            if(!available) return;

            abilityHolder.suportButton = context.ReadValueAsButton();
        }
        #endregion
    }
}
