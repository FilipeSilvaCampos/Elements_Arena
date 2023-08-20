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
        [SerializeField] int startSceneId = 0;
        [SerializeField] LayerMask[] playersLayers;
        [SerializeField] int levelId;
        [SerializeField] GameObject gameOverScreen;

        LevelManager currentLevel = null;
        //Only for update on Itch.io
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
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(levelId);

            currentLevel = FindObjectOfType<LevelManager>();

            if(players.Count > 1)
            {
                currentLevel.SetSplitScreen();
            }

            InitializeFighters();
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

        public void UnreadyPlayer(int playerIndex)
        {
            players[playerIndex].ready = false;
        }
        
        public void GameOver(GameObject loserObject)
        {
            if (hasWinner) return;

            foreach(GameObject fighter in fightersInGame)
            {
                fighter.GetComponentInChildren<CharacterMovement>().enabled = false;
                fighter.GetComponentInChildren<AbilityWrapper>().enabled = false;
                fighter.GetComponentInChildren<CameraController>().enabled = false;
            }

            for(int i = 0; i < fightersInGame.Length; i++)
            {
                if (loserObject == fightersInGame[i])
                {
                    gameOverScreen.SetActive(true);
                    gameOverScreen.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("Player {0} Win", i == 0? 2:1));
                    hasWinner = true;
                    break;
                }
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
            StartCoroutine(LoadLevel(levelId));
        }

        public void RestartGame()
        {
            hasWinner = false;
            gameOverScreen.SetActive(false);

            foreach(GameObject fighter in fightersInGame)
            {
                Destroy(fighter);
            }

            InitializeFighters();
        }

        public void BackToStartScene()
        {
            hasWinner = false;
            gameStarted = false;
            gameOverScreen.SetActive(false);

            for(int i = 0; i < players.Count; i++)
            {
                players[i].ready = false;
                players[i].gameObject.GetComponent<PlayerController>().enabled = false;
            }
            characterAI = null;

            foreach(GameObject fighter in fightersInGame) Destroy(fighter);

            SceneManager.LoadScene(startSceneId);
        }
        
        public LayerMask GetPlayerLayer(int playerIndex)
        {
            return playersLayers[playerIndex];
        }

        public Transform GetPlayerSpawn(int playerIndex)
        {
            return currentLevel.spawns[playerIndex];
        }

        public Player GetPlayer(int index)
        {
            if (!players.ContainsKey(index)) return null;

            return players[index];
        }
    }
}
