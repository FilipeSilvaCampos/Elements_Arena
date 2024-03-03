using ElementsArena.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace ElementsArena.UI
{
	[System.Serializable]
	class Display
	{
		public Image uiImage;
		public Image foreground;
	}

	public class AbilityStateDisplay : MonoBehaviour
	{
		[Header("Abilitys Display")]
		[SerializeField] Display primary;
		[SerializeField] Display secundary;

		[HideInInspector] public AbilityHolder abilityHolder;

		//Colors
		Color readyColor = Color.white;
		Color cooldownColor = new Color(.5f, .5f, .5f, 1);

		void Update()
		{
			if (abilityHolder == null) return;

			UpdateDisplay(primary, abilityHolder.firstAbility);
			UpdateDisplay(secundary, abilityHolder.secondAbility);
		}

		private void UpdateDisplay(Display display, Ability ability)
		{
			float nextAmount = ability.GetCooldownTimer() / ability.CooldownTime;

			display.foreground.fillAmount = nextAmount;
			display.foreground.gameObject.SetActive(nextAmount == 1 ? false : true);

			display.uiImage.color = ability.GetCurrentState() == AbilityStates.cooldown ? cooldownColor : readyColor;
			if (ability.uiSprite != null)
			{
				display.uiImage.sprite = ability.uiSprite;
			}
		}
	}
}
