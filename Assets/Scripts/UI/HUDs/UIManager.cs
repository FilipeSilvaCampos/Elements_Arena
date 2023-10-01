using ElementsArena.Combat;
using ElementsArena.Damage;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] DisplayCooldowns displayCooldowns;

    public virtual void Initialize(GameObject fighter)
    {
        healthBar.characterDamageable = fighter.GetComponent<IDamageable>();
        displayCooldowns.abilityHolder = fighter.GetComponent<AbilityHolder>();
    }
}
