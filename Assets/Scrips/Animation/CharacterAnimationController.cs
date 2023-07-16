using ElementsArena.Combat;
using ElementsArena.Movement;
using UnityEngine;

namespace ElementsArena.Animation
{
    public class AnimationKeys
    {
        public const string HorizontalFloat = "vSpeed";
        public const string VerticalFloat = "hSpeed";
        public const string PrimaryAbilityBool = "PrimaryAbility";
    }

    public class CharacterAnimationController : MonoBehaviour
    {
        Animator animator;
        CharacterMovement characterMovement;
        AbilityWrapper abilityWrapper;
        private void Awake()
        {
            abilityWrapper = GetComponent<AbilityWrapper>();
            animator = GetComponentInChildren<Animator>();
            characterMovement = GetComponent<CharacterMovement>();
        }

        private void Update()
        {
            animator.SetFloat(AnimationKeys.HorizontalFloat, characterMovement.GetDirection().z);
            animator.SetFloat(AnimationKeys.VerticalFloat, characterMovement.GetDirection().x);
            animator.SetBool(AnimationKeys.PrimaryAbilityBool, abilityWrapper.primaryAbility.called);
        }
    }
}
