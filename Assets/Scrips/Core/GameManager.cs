using ElementsArena.SceneManagement;
using System;
using System.Collections;
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
            playerInput = player.GetComponent<PlayerInput>();
            playerManager = player.GetComponent<PlayerManager>();
        }

        public PlayerInput playerInput;
        public PlayerManager playerManager;
        public GameObject gameObject;
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameObject startScreen;
        [SerializeField] GameObject selectCharacterScreen;
        [SerializeField] GameObject[] cursors;
        [SerializeField] LayerMask[] playersLayers;
        [SerializeField] Scene level = null;

        public event Action onSelectScreen;
        bool gameStarted = false;
        PlayerInputManager playerInputManager;
        Dictionary<int, Player> players = new Dictionary<int, Player>();

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

        private IEnumerator LoadLevel(Scene level)
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(level.id);

            if (players.Count == 2)
            {
                FindObjectOfType<LevelManager>().SetSplitScreen();
            }

            SetPlayers(level);
        }

        private void SetPlayers(Scene level)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].playerManager.SetUpPlayer(playersLayers[i]);
                players[i].gameObject.transform.position = level.spawns[i].position;
                players[i].gameObject.transform.rotation = level.spawns[i].rotation;
            }
        }

        public GameObject GetCursor(int playerIndex)
        {
            cursors[playerIndex].SetActive(true);
            return cursors[playerIndex];
        }

        public void ShowSelectionScreen()
        {
            startScreen.SetActive(false);
            selectCharacterScreen.SetActive(true);
            playerInputManager.DisableJoining();
            onSelectScreen.Invoke();
        }

        public void StartGame()
        {
            if (gameStarted) return;

            for (int i = 0; i < players.Count; i++)
            {
                if (!players[i].playerManager.ready) return;
            }

            gameStarted = true;
            StartCoroutine(LoadLevel(level));
        }
    }
}
