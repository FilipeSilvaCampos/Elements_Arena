using UnityEngine;

namespace ElementsArena.Combat
{
    public class AbilityWrapper : MonoBehaviour
    {
        public Ability primaryAbility;
        public Ability secundaryAbility;
        public Ability evadeAbility;

        public bool available = true;

        public Vector2 selectionInput { get; set; }

        public void CallPrimaryAbility(bool value)
        {
            if (!available) return;
            primaryAbility.called = value;
        }

        public void CallSecundaryAbility(bool value)
        {
            if (!available) return;
            secundaryAbility.called = value;
        }

        public void CallEvadeAbility(bool value)
        {
            if (!available) return;
            evadeAbility.called = value;
        }
    }
}
