using ElementsArena.Combat;
using UnityEngine;

[System.Serializable]
public class BendState
{
    public Sprite iconState;
    public Ability firstAbility;
    public Ability secondAbility;
}

[RequireComponent(typeof(AbilityHolder))]
public class BendStateManager : MonoBehaviour
{
    [SerializeField] BendState[] states;
    AbilityHolder abilityHolder;

    BendState currentState;

    protected virtual void Start()
    {
        abilityHolder = GetComponent<AbilityHolder>();

        SwitchState(0);
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

        currentState = states[stateIndex];
    }

    public BendState GetCurrentState() => currentState;
}
