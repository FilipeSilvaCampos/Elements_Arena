using ElementsArena.Combat;
using ElementsArena.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ElementsArena.Control
{
    public class PlayerController : MonoBehaviour
    {
        CharacterMovement characterMovement;
        CameraController cameraController;
        AbilityWrapper abilityWrapper;

        Vector2 moveInput = Vector2.zero;
        Vector2 cameraInput = Vector2.zero;

        private void Awake()
        {
            enabled = false;
        }

        public void SetUpController(CharacterMovement characterMovement, AbilityWrapper abilityWrapper, CameraController cameraController)
        {
            enabled = true;

            this.characterMovement = characterMovement;
            this.cameraController = cameraController;
            this.abilityWrapper = abilityWrapper;
        }

        private void Update()
        {
            if (!enabled) return;

            //Update move and camera inputs
            cameraController.IncrementCameraRotation(new Vector2(cameraInput.y, cameraInput.x));
            characterMovement.SetInput(new CharacterMovementInput
            {
                MoveInput = moveInput,
                LookRotation = cameraController.LookRotation
            });
        }

        public void MovePlayer(InputAction.CallbackContext context)
        {
            if (enabled)
                moveInput = context.ReadValue<Vector2>();
        }

        public void MoveCamera(InputAction.CallbackContext context)
        {
            if (enabled)
                cameraInput = context.ReadValue<Vector2>();
        }

        public void SetSelectionInput(InputAction.CallbackContext context)
        {
            if (enabled)
                abilityWrapper.selectionInput = context.ReadValue<Vector2>();
        }

        public void CallPrimaryAbility(InputAction.CallbackContext context)
        {
            if (enabled)
                abilityWrapper.CallPrimaryAbility(context.ReadValueAsButton());
        }

        public void CallSecundaryAbility(InputAction.CallbackContext context)
        {
            if (enabled)
                abilityWrapper.CallSecundaryAbility(context.ReadValueAsButton());
        }

        public void CallEvadeAbility(InputAction.CallbackContext context)
        {
            if (enabled)
                abilityWrapper.CallEvadeAbility(context.ReadValueAsButton());
        }
    }
}
