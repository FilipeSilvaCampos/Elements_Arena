using ElementsArena.Combat;
using TMPro;
using UnityEngine;

public class DisplayCooldowns : MonoBehaviour
{
    [SerializeField] AbilityWrapper abilityWrapper;
    [Header("Abilitys Display")]
    [SerializeField] GameObject primary;
    [SerializeField] GameObject secundary;
    [SerializeField] GameObject evade;

    private void Update()
    {
        if (abilityWrapper == null) return;

        UpdateCooldownText(primary, abilityWrapper.primaryAbility);
        UpdateCooldownText(secundary, abilityWrapper.secundaryAbility);
        UpdateCooldownText(evade, abilityWrapper.evadeAbility);
    }

    void UpdateCooldownText(GameObject display, Ability ability)
    {
        if (ability.GetCooldownTimer() == 0 || ability.GetCooldownTimer() == ability.CooldownTime)
        {
            display.SetActive(false);
            return;
        }

        display.SetActive(true);
        display.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("{0:.0}", ability.GetCooldownTimer()));
    }
}
