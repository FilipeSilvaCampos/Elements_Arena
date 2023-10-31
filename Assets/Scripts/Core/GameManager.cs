using ElementsArena.Control;
using ElementsArena.Damage;
using ElementsArena.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] int mainSceneId = 0;
        [SerializeField] LayerMask[] playersLayers;
        [SerializeField] GameObject gameOverScreen;
        [SerializeField] Fader fader;
        [SerializeField] float fadeInTime = 2;

        //Level vars
        LevelManager currentLevel = null;
        GameObject[] fightersInGame = new GameObject[2];
        bool hasWinner = false;
        //

        public bool VersusAI { get { return versusAI; } set { versusAI = value; } }
        bool gameStarted = false;
        PlayerInputManager playerInputManager;
        Dictionary<int, Player> players = new Dictionary<int, Player>();
        Character characterAI = null;
        bool versusAI = false;
        bool gamePaused = false;

        #region MonoBehaviour Callbacks
        private void Awake()
        {
            playerInputManager = GetComponent<PlayerInputManager>();
        }

        private void Start()
        {
            playerInputManager.onPlayerJoined += RegisterNewPlayer;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && gameStarted && !hasWinner)
            {
                PauseGame();
            }
        }
        #endregion

        private void RegisterNewPlayer(PlayerInput playerInput)
        {
            GameObject player = playerInput.gameObject;

            player.GetComponent<CursorController>().selectedCharacter += StartGame;
            Player newPlayer = new Player(player);
            players.Add(playerInput.playerIndex, newPlayer);
            DontDestroyOnLoad(player);

            if (players.Count > 1) versusAI = false;
        }

        private IEnumerator LoadLevel(int levelToLoad)
        {
            fader.FaderOutImmediate();
            yield return SceneManager.LoadSceneAsync(levelToLoad);

            currentLevel = FindObjectOfType<LevelManager>();

            if(players.Count > 1)
            {
                currentLevel.SetSplitScreen();
            }

            InitializeFighters();
            AbleAllFightersControls(false);
            yield return fader.FadeIn(1);
            AbleAllFightersControls(true);
        }

        private GameObject SetAI(Transform spawn, Camera camera)
        {
            GameObject ai = Instantiate(characterAI.prefab, spawn.position, spawn.rotation);
            ai.GetComponent<AIController>().enabled = true;
            return ai;
        }

        private void InitializeFighters()
        {
            AbleAllFightersControls(true);
            gamePaused = false;
            for(int i = 0; i < players.Count; i++)
            {
                if (players.ContainsKey(i))
                {
                    fightersInGame[i] = players[i].playerManager.SetUpPlayer(
                        currentLevel.spawns[i],
                        playersLayers[i],
                        currentLevel.cameras[i]
                        );
                }
            }

            if (characterAI != null)
            {
                fightersInGame[1] = SetAI(currentLevel.spawns[1], currentLevel.cameras[1]);
            }
        }

        public void GameOver(GameObject loserObject)
        {
            if (hasWinner) return;
            AbleAllFightersControls(false);

            for (int i = 0; i < fightersInGame.Length; i++)
            {
                if (loserObject == fightersInGame[i])
                {
                    gameOverScreen.SetActive(true);
                    gameOverScreen.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("Player {0} Win", i == 0 ? 2 : 1));
                    hasWinner = true;
                    break;
                }
            }
        }

        private void AbleAllFightersControls(bool value)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (value)
                    players[i].gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap(PlayerActionMapsKeys.InGame);
                else
                    players[i].gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap(PlayerActionMapsKeys.UI);
            }
        }

        public void StartGame(Character character)
        { 
            if (gameStarted) return;

            if (versusAI) characterAI = character;
            if (players.Count == 1 && characterAI == null)
            {
                
                versusAI = true;
                return;
            }
            

            for (int i = 0; i < players.Count; i++)
            {
                if (!players[i].playerManager.isReady) return;
            }

            gameStarted = true;
            StartCoroutine(LoadLevel(1)); //Defalut teste arena
        }

        private void PauseGame()
        {
            gamePaused = !gamePaused;

            gameOverScreen.SetActive(gamePaused);
            gameOverScreen.GetComponentInChildren<TextMeshProUGUI>().text = "Game Paused";
            AbleAllFightersControls(!gamePaused);
        }

        public void RestartGame()
        {
            ResetLevel();

            fader.FaderOutImmediate();
            InitializeFighters();
            fader.FadeIn(fadeInTime);
        }

        public void BackToStartScene()
        {
            ResetLevel();
            gameStarted = false;

            for(int i = 0; i < players.Count; i++)
            {
                players[i].playerManager.SetCharacter(null);
                players[i].playerManager.UndoReady();
                players[i].gameObject.GetComponent<PlayerController>().SetAvailable(false);
            }
            characterAI = null;

            SceneManager.LoadScene(mainSceneId);
        }

        private void ResetLevel()
        {
            hasWinner = false;
            gameOverScreen.SetActive(false);

            foreach (GameObject fighter in fightersInGame)
            {
                Destroy(fighter);
            }
        }

        public Player GetPlayer(int index)
        {
            if (!players.ContainsKey(index)) return null;

            return players[index];
        }
    }
}
