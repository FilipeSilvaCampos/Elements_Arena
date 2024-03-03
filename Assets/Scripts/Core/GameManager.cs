using ElementsArena.Control;
using ElementsArena.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ElementsArena.Core
{
	public class Player
	{
		public Player(GameObject player)
		{
			gameObject = player;
			playerManager = player.GetComponent<PlayerManager>();
		}

		public PlayerManager playerManager;
		public GameObject gameObject;
	}


	public class GameManager : MonoBehaviour
	{
		[SerializeField] LayerMask[] playersLayers;
		[SerializeField] Fader fader;

		public bool isVersusAI = false;
		public bool isAllPlayersReady = false;
		public event Action onNewPlayerIsRegistered;
		public List<Player> players { get; private set; } = new List<Player>();
		public Character characterAI = null;

		private void Start()
		{
			GetComponent<PlayerInputManager>().onPlayerJoined += RegisterNewPlayer;
			DontDestroyOnLoad(gameObject);
		}

		private void RegisterNewPlayer(PlayerInput playerInput)
		{
			GameObject player = playerInput.gameObject;
			player.GetComponent<CursorController>().onSelecteCharacter += CheckPlayers;
			DontDestroyOnLoad(player);

			Player newPlayer = new Player(player);
			players.Add(newPlayer);

			if (players.Count > 1) isVersusAI = false;
			onNewPlayerIsRegistered.Invoke();
		}

		public void LoadGame(int levelToLoad)
		{
			fader.FaderOutImmediate();
			SceneManager.LoadScene(levelToLoad);
		}

		public void AbleAllFightersControls(bool value)
		{
			for (int i = 0; i < players.Count; i++)
			{
				if (value)
					players[i].gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap(PlayerActionMapsKeys.InGame);
				else
					players[i].gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap(PlayerActionMapsKeys.UI);
			}
		}

		private void CheckPlayers(Character character)
		{
			if (isVersusAI) characterAI = character;
			if (players.Count == 1 && characterAI == null)
			{
				isVersusAI = true;
				return;
			}


			for (int i = 0; i < players.Count; i++)
			{
				if (!players[i].playerManager.isReady) return;
			}

			isAllPlayersReady = true;
		}

		public void ResetGame()
		{
			for (int i = 0; i < players.Count; i++)
			{
				players[i].playerManager.SetCharacter(null);
				players[i].playerManager.UndoReady();
				players[i].gameObject.GetComponent<PlayerController>().SetAvailable(false);
			}
			characterAI = null;
			isAllPlayersReady = false;
		}

		public Player GetPlayer(int index)
		{
			if (index >= players.Count || index < 0) return null;

			return players[index];
		}
	}
}
