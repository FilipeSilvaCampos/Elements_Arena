using ElementsArena.Combat;
using ElementsArena.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace ElementsArena.Prototype
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] GameObject startMenu;

        GameManager gameManager;
        PlayerController playerController;
        GameObject currentBender;
        GameObject selectedBender;


        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            playerController = GetComponent<PlayerController>();    
        }

        public void SelectBender(GameObject benderPrefab)
        {
            selectedBender = benderPrefab;
        }

        public void StartGame()
        {
            if (selectedBender == null) return;

            if (currentBender != null) Destroy(currentBender);
            SetUpNewBender();
            startMenu.SetActive(false);
        }

        private void SetUpNewBender()
        {
            currentBender = Instantiate(selectedBender, transform);
            
            SetSpawn();
            SetCameraTarget(currentBender.transform.Find("CameraTarget"));
            playerController.SetUpController(currentBender.GetComponent<CharacterMovement>(), currentBender.GetComponent<AbilityWrapper>());
        }

        private void SetCameraTarget(Transform cameraTarget)
        {
            CinemachineVirtualCamera vcam = GetComponentInChildren<CinemachineVirtualCamera>();

            vcam.Follow = cameraTarget;
            vcam.LookAt = cameraTarget;
        }

        private void SetSpawn()
        {
            int playerIndex = GetComponent<PlayerInput>().playerIndex;

            transform.position = gameManager.GetSpawn(playerIndex).position;
            transform.rotation = gameManager.GetSpawn(playerIndex).rotation;
        }
    }
}
