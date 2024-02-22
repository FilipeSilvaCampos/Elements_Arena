using UnityEngine;

namespace ElementsArena.UI
{
	public class GameModeScreen : Menu
	{
		[SerializeField] GameObject menuObject;

		public override void Hide()
		{
			menuObject.SetActive(false);
		}

		public override void Show()
		{
			menuObject.SetActive(true);
		}
	}
}