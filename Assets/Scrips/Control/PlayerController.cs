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
        Ability primaryAbility;
        Ability secundaryAbility;
        Ability evadeAbility;

        Vector2 moveInput = Vector2.zero;
        Vector2 cameraInput = Vector2.zero;
        public bool alive { get; set; }

        public void SetUpController(CharacterMovement characterMovement, AbilityWrapper abilityWrapper, CameraController cameraController)
        {
            alive = true;

            this.characterMovement = characterMovement;
            this.cameraController = cameraController;
            this.abilityWrapper = abilityWrapper;
            primaryAbility = abilityWrapper.primaryAbility;
            secundaryAbility = abilityWrapper.secundaryAbility;
            evadeAbility = abilityWrapper.evadeAbility;
        }

        private void Update()
        {
            if (!alive) return;

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
            if (alive == false) return;

            moveInput = context.ReadValue<Vector2>();
        }

        public void MoveCamera(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            cameraInput = context.ReadValue<Vector2>();
        }

        public void SetSelectionInput(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            abilityWrapper.selectionInput = context.ReadValue<Vector2>();
        }

        public void CallPrimaryAbility(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            abilityWrapper.CallPrimaryAbility(context.ReadValueAsButton());
        }

        public void CallSecundaryAbility(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            abilityWrapper.CallSecundaryAbility(context.ReadValueAsButton());
        }

        public void CallEvadeAbility(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            abilityWrapper.CallEvadeAbility(context.ReadValueAsButton());
        }
    }
}
