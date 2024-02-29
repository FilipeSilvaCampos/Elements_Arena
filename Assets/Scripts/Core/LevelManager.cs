using ElementsArena.Control;
using ElementsArena.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ElementsArena.Core
{
	public class LevelManager : MonoBehaviour
	{
		public Transform[] spawns = null;
		public Camera[] cameras = null;
		[SerializeField] LayerMask[] playerLayers;
		[SerializeField] GameObject gameOverScreen;

		//Control Vars
		public GameObject[] fightersInGame = new GameObject[2];
		bool isGamePaused = false;
		bool hasWinner = false;
		GameManager gameManager;
		Fader fader;

		private void Awake()
		{
			gameManager= FindObjectOfType<GameManager>();
			fader = FindObjectOfType<Fader>();
		}

		private void Start()
		{
			if (gameManager.players.Count > 1)
			{
				SetSplitScreen();
			}

			InitializeFighters();
			StartCoroutine(StartGame());
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Escape)) 
			{
				PauseGame();
			}	
		}

		public void SetSplitScreen()
		{
			for (int i = 0; i < cameras.Length; i++)
			{
				cameras[i].gameObject.SetActive(true);
				if (i == 0) cameras[0].rect = new Rect(0, 0.5f, 1, 1);
				else cameras[i].rect = new Rect(0, -0.5f, 1, 1);
			}
		}

		public void ShowGameOverScreen(string exibeText)
		{
			gameOverScreen.SetActive(true);
			gameOverScreen.GetComponentInChildren<TextMeshProUGUI>().SetText(exibeText);
		}

		public void GameOver(GameObject loserObject)
		{
			if (hasWinner) return;
			gameManager.AbleAllFightersControls(false);

			for (int i = 0; i < fightersInGame.Length; i++)
			{
				if (loserObject == fightersInGame[i])
				{
					ShowGameOverScreen(string.Format("Player {0} Win", i == 0 ? 2 : 1));
					hasWinner = true;
					break;
				}
			}
		}

		public void ResetLevel()
		{
			hasWinner = false;
			isGamePaused = false;
			Time.timeScale = 1;

			gameOverScreen.SetActive(false);

			foreach (GameObject fighter in fightersInGame)
			{
				Destroy(fighter);
			}
		}

		void PauseGame()
		{
			isGamePaused = !isGamePaused;

			gameManager.AbleAllFightersControls(!isGamePaused);
			gameOverScreen.SetActive(isGamePaused);
			gameOverScreen.GetComponentInChildren<TextMeshProUGUI>().text = "Game Paused";

			Time.timeScale = isGamePaused ? 0 : 1;
		}

		private void InitializeFighters()
		{
			gameManager.AbleAllFightersControls(true);
			for (int i = 0; i < gameManager.players.Count; i++)
			{
				if (gameManager.players[i] != null)
				{
					fightersInGame[i] = gameManager.players[i].playerManager.SetUpPlayer(
						spawns[i],
						playerLayers[i],
						cameras[i]
						);
				}
			}

			if (gameManager.characterAI != null)
			{
				fightersInGame[1] = SetAI(spawns[1], cameras[1]);
			}
		}

		private GameObject SetAI(Transform spawn, Camera camera)
		{
			GameObject ai = Instantiate(gameManager.characterAI.prefab, spawn.position, spawn.rotation);
			ai.GetComponent<AIController>().enabled = true;
			return ai;
		}

		IEnumerator StartGame()
		{
			gameManager.AbleAllFightersControls(false);
			yield return fader.FadeIn(1);
			gameManager.AbleAllFightersControls(true);
		}

		public void RestartGame()
		{
			ResetLevel();

			fader.FaderOutImmediate();
			InitializeFighters();

			StartCoroutine(StartGame());
		}

		public void BackToStartScene()
		{
			ResetLevel();

			gameManager.ResetGame();

			SceneManager.LoadScene(0);
		}
	}
}
