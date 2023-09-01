using ElementsArena.Combat;
using ElementsArena.Control;
using ElementsArena.Movement;
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
        public bool ready = false;
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] int mainSceneId = 0;
        [SerializeField] LayerMask[] playersLayers;
        [SerializeField] int levelToLoadId = 1;
        [SerializeField] GameObject gameOverScreen;
        [SerializeField] Fader fader;
        [SerializeField] float fadeInTime = 2;

        //Level vars
        LevelManager currentLevel = null;
        GameObject[] fightersInGame = new GameObject[2];
        bool hasWinner = false;
        //

        bool gameStarted = false;
        PlayerInputManager playerInputManager;
        Dictionary<int, Player> players = new Dictionary<int, Player>();
        Character characterAI = null;

        private void Awake()
        {
            playerInputManager = GetComponent<PlayerInputManager>();
        }

        private void Start()
        {
            playerInputManager.onPlayerJoined += RegisterNewPlayer;
        }

        private void RegisterNewPlayer(PlayerInput playerInput)
        {
            GameObject player = playerInput.gameObject;

            Player newPlayer = new Player(player);
            players.Add(playerInput.playerIndex, newPlayer);
            DontDestroyOnLoad(player);
        }

        private IEnumerator LoadLevel(int levelId)
        {
            fader.FaderOutImmediate();
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(levelId);

            currentLevel = FindObjectOfType<LevelManager>();

            if(players.Count > 1)
            {
                currentLevel.SetSplitScreen();
            }

            InitializeFighters();
            AbleAllFightersControls(false);
            yield return fader.FadeIn(fadeInTime);
            AbleAllFightersControls(true);
        }

        private GameObject SetAI(Transform spawn, Camera camera)
        {
            GameObject ai = Instantiate(characterAI.prefab, spawn.position, spawn.rotation);
            ai.GetComponent<AIController>().enabled = true;
            ai.GetComponentInChildren<Canvas>().worldCamera = camera;
            return ai;
        }

        public void SetPlayerCharater(int playerIndex, Character character)
        {
            if (players[playerIndex].ready && players.Count == 1)
            {
                characterAI = character;
                StartGame();
                return;
            }

            players[playerIndex].playerManager.SetCharacter(character);
            players[playerIndex].ready = true;

            StartGame();
        }

        private void InitializeFighters()
        {
            for(int i = 0; i < 2;i++)
            {
                if (players.ContainsKey(i))
                {
                    fightersInGame[i] = players[i].playerManager.SetUpPlayer(
                        currentLevel.spawns[i],
                        playersLayers[i],
                        currentLevel.cameras[i]
                        );
                }
                else
                {
                    fightersInGame[i] = SetAI(currentLevel.spawns[i], currentLevel.cameras[i]);
                }
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
            foreach (GameObject fighter in fightersInGame)
            {
                fighter.GetComponentInChildren<CharacterMovement>().enabled = value;
                fighter.GetComponentInChildren<AbilityHolder>().enabled = value;
                fighter.GetComponentInChildren<CameraController>().enabled = value;
            }
        }

        public void StartGame()
        {
            if (gameStarted) return;
            if (players.Count == 1 && characterAI == null) return;

            for (int i = 0; i < players.Count; i++)
            {
                if (!players[i].ready) return;
            }

            gameStarted = true;
            StartCoroutine(LoadLevel(levelToLoadId));
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
                players[i].ready = false;
                players[i].gameObject.GetComponent<PlayerController>().enabled = false;
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

        public void UnreadyPlayer(int playerIndex)
        {
            players[playerIndex].ready = false;
        }

        public Player GetPlayer(int index)
        {
            if (!players.ContainsKey(index)) return null;

            return players[index];
        }
    }
}
