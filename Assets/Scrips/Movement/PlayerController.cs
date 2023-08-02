using ElementsArena.Combat;
using ElementsArena.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ElementsArena.Prototype
{
    public class PlayerController : MonoBehaviour
    {
        CharacterMovement characterMovement;
        CameraController cameraController;
        AbilityWrapper abilityWrapper;
        Ability primaryAbility;
        Ability secundaryAbility;
        Ability evadeAbility;

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

        public void MovePlayer(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            Vector2 input = context.ReadValue<Vector2>();
            characterMovement.SetInput(new CharacterMovementInput()
            {
                MoveInput = input,
            });
        }

        public void MoveCamera(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            Vector2 input = context.ReadValue<Vector2>();
            cameraController.IncrementCameraRotation(new Vector2(-input.y, input.x));
        }

        public void SetSelectionInput(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            abilityWrapper.selectionInput = context.ReadValue<Vector2>();
        }

        public void CallPrimaryAbility(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            primaryAbility.called = context.ReadValueAsButton();
        }

        public void CallSecundaryAbility(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            secundaryAbility.called = context.ReadValueAsButton();
        }

        public void CallEvadeAbility(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            evadeAbility.called = context.ReadValueAsButton();
        }
    }
}
