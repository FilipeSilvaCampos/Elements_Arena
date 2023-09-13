using ElementsArena.Combat;
using UnityEngine;

[System.Serializable]
class BendState
{
    public Ability firstAbility;
    public Ability secondAbility;
}

[RequireComponent(typeof(AbilityHolder))]
public class BendStateManager : MonoBehaviour
{
    [SerializeField] BendState[] states;
    AbilityHolder abilityHolder;

    protected virtual void Start()
    {
        abilityHolder = GetComponent<AbilityHolder>();

        abilityHolder.firstAbility = states[0].firstAbility;
        abilityHolder.secondAbility = states[0].secondAbility;
    }

    protected virtual void Update()
    {
        if(abilityHolder.selectionInput != Vector2.zero)
        {
            switch(abilityHolder.selectionInput.x, abilityHolder.selectionInput.y)
            {
                case (0, 1):
                    SwitchState(0);
                    break;
                case (0, -1):
                    SwitchState(1);
                    break;
                case (1, 0):
                    SwitchState(2);
                    break;
                case (-1, 0):
                    SwitchState(3);
                    break;
            }
        }
    }

    void SwitchState(int stateIndex)
    {
        if (stateIndex > states.Length) return;

        abilityHolder.firstAbility = states[stateIndex].firstAbility;
        abilityHolder.secondAbility = states[stateIndex].secondAbility;
    }
}
