using UnityEngine;

namespace ElementsArena.Combat
{
    public class AbilityWrapper : MonoBehaviour
    {
        public Ability primaryAbility;
        public Ability secundaryAbility;
        public Ability evadeAbility;

        public Vector2 selectionInput { get; set; }

        public void CallPrimaryAbility(bool value)
        {
            if (enabled)
                primaryAbility.called = value;
        }

        public void CallSecundaryAbility(bool value)
        {
            if (enabled)
                secundaryAbility.called = value;
        }

        public void CallEvadeAbility(bool value)
        {
            if (enabled)
                evadeAbility.called = value;
        }
    }
}
