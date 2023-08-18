using ElementsArena.Core;
using UnityEngine;

namespace ElementsArena.UI
{
    public class CharacterButton : MonoBehaviour
    {
        [SerializeField] Character character = null;
        GameManager gameManager;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        public void Invoke(int playerIndex)
        {
            gameManager.SetPlayerCharater(playerIndex, character);
        }
    }
}