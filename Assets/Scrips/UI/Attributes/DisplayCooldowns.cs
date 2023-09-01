using ElementsArena.Combat;
using TMPro;
using UnityEngine;

public class DisplayCooldowns : MonoBehaviour
{
    [SerializeField] AbilityHolder abilityHolder;
    [Header("Abilitys Display")]
    [SerializeField] GameObject primary;
    [SerializeField] GameObject secundary;
    [SerializeField] GameObject evade;

    private void Update()
    {
        if (abilityHolder == null) return;

        UpdateCooldownText(primary, abilityHolder.firstAbility);
        UpdateCooldownText(secundary, abilityHolder.secondAbility);
        UpdateCooldownText(evade, abilityHolder.evadeAbility);
    }

    void UpdateCooldownText(GameObject display, Ability ability)
    {
        if (Mathf.Approximately(ability.GetCooldownTimer(), 0f) || Mathf.Approximately(ability.GetCooldownTimer(), ability.CooldownTime))
        {
            display.SetActive(false);
            return;
        }

        display.SetActive(true);
        display.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("{0:.0}", ability.GetCooldownTimer()));
    }
}
