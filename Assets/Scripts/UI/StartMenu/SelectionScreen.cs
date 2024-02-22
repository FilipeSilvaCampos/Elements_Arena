using ElementsArena.Control;
using ElementsArena.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ElementsArena.UI
{
	public class SelectionScreen : Menu
	{
		[SerializeField] PlayerPreview[] previews;
		[SerializeField] GameObject player2Warning;
		[SerializeField] GameObject[] cursors;
		[SerializeField] GameObject menuObject;

		//Control Vars
		MenuManager menuManager;
		bool isShowed = false;

		#region MonoBehaviour CallBacks
		private void Awake()
		{
			menuManager = GetComponent<MenuManager>();
		}

		private void Start()
		{
			menuManager.gameManager.onNewPlayerIsRegistered += SetSelectSreen;
		}

		private void Update()
		{
			if (!isShowed) return;

			foreach (PlayerPreview preview in previews)
			{
				Player player = menuManager.gameManager.GetPlayer((int)preview.player);
				if (player != null)
				{
					preview.previewRect.SetPlayer(player.playerManager);
				}
			}

			if (menuManager.playerInputManager.playerCount == 1 && menuManager.gameManager.GetPlayer(0).playerManager.isReady)
			{
				previews[(int)PlayerEnum.Player2].previewRect.SetFollowCursor(cursors[0]);
			}
		}
		#endregion

		public override void Hide()
		{
			isShowed = false;
			menuObject.SetActive(false);
		}

		public override void Show()
		{
			menuObject.SetActive(true);
			isShowed = true;
			menuManager.gameManager.isVersusAI = false;

			Vector3 cursorDeffaultPositon = FindObjectOfType<Selectable>().transform.position + Vector3.forward * -10;
			foreach (GameObject cursor in cursors)
			{
				cursor.transform.position = cursorDeffaultPositon;
			}
		}

		void SetSelectSreen()
		{
			for (int i = 0; i < menuManager.playerInputManager.playerCount; i++)
			{
				GameObject currentPlayer = menuManager.gameManager.GetPlayer(i).gameObject;
				currentPlayer.GetComponent<CursorController>().SetCursor(cursors[i]);
			}

			SetPreviewsCursors();
		}

		void SetPreviewsCursors()
		{
			foreach (PlayerPreview preview in previews)
			{
				preview.previewRect.SetFollowCursor(cursors[(int)preview.player]);
			}
		}
	}
}
