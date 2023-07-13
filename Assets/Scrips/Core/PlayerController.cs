using ElementsArena.Combat;
using ElementsArena.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ElementsArena.Prototype
{
    public class PlayerController : MonoBehaviour
    {
        CharacterMovement characterMovement;
        AbilityWrapper abilityWrapper;
        Ability primaryAbility;
        Ability secundaryAbility;
        Ability evadeAbility;

        public bool alive { get; set; }

        public void SetUpController(CharacterMovement characterMovement, AbilityWrapper abilityWrapper)
        {
            alive = true;

            this.characterMovement = characterMovement;
            this.abilityWrapper = abilityWrapper;
            primaryAbility = abilityWrapper.primaryAbility;
            secundaryAbility = abilityWrapper.secundaryAbility;
            evadeAbility = abilityWrapper.evadeAbility;
        }

        public void MovePlayer(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            Vector2 input = context.ReadValue<Vector2>();
            characterMovement.SetDirection(new Vector3(input.x, 0, input.y));    
        }

        public void MoveCamera(InputAction.CallbackContext context)
        {
            if (alive == false) return;

            characterMovement.SetCameraInput(context.ReadValue<Vector2>());
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
