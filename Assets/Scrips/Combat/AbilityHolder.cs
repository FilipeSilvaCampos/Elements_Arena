using UnityEngine;

namespace ElementsArena.Combat
{
    public class AbilityHolder : MonoBehaviour
    {
        public Ability firstAbility;
        public Ability secondAbility;
        public Ability evadeAbility;

        public Vector2 selectionInput { get; set; }
        public bool usingPrimary { get; set; } = false;
    }
}
