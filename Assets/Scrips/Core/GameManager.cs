using UnityEngine;
using UnityEngine.InputSystem;

namespace ElementsArena.Prototype
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Transform[] spawns;
        [SerializeField] Camera[] cameras;

        PlayerInputManager playerInputManager;

        private void Start()
        {
            playerInputManager = FindObjectOfType<PlayerInputManager>();
        }

        public Transform GetSpawn(int playerIndex)
        {
            if (playerIndex < spawns.Length) return spawns[playerIndex];
            else return spawns[0];
        }

        public Camera GetCamera(int playerIndex)
        {
            if (playerIndex < cameras.Length)
            {
                cameras[playerIndex].gameObject.SetActive(true);
                return cameras[playerIndex];
            }
            else
            {
                cameras[0].gameObject.SetActive(true);
                return cameras[0];
            }
        }

        public void SetSplitScreen()
        {
            if (playerInputManager.playerCount < 2) return;

            for (int i = 0; i < cameras.Length; i++)
            {
                if (i == 0) cameras[i].rect = new Rect(0, 0.5f, 1, 1);
                else cameras[i].rect = new Rect(0, -0.5f, 1, 1);
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
