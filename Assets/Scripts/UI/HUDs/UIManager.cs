using ElementsArena.Combat;
using ElementsArena.Damage;
using ElementsArena.UI;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] AbilityStateDisplay abilityStateDisplay;

    public virtual void Initialize(GameObject fighter)
    {
        healthBar.characterDamageable = fighter.GetComponent<IDamageable>();
        abilityStateDisplay.abilityHolder = fighter.GetComponent<AbilityHolder>();
    }
}
