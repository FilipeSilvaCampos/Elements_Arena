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
        [SerializeField] LayerMask[] playerLayers;

        PlayerInput playerInput;
        GameManager gameManager;
        PlayerController playerController;
        GameObject currentBender;
        GameObject selectedBender;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            playerController = GetComponent<PlayerController>();
            playerInput = GetComponent<PlayerInput>();

            ConfigCamera();
        }

        private void SetUpNewBender()
        {
            currentBender = Instantiate(selectedBender, transform);
            
            SetSpawn();
            SetCameraTarget(currentBender.transform.Find("CameraTarget"));
            playerController.SetUpController(currentBender.GetComponent<CharacterMovement>(), currentBender.GetComponent<AbilityWrapper>());
        }

        private void ConfigCamera()
        {
            GetComponentInChildren<Canvas>().worldCamera = gameManager.GetCamera(playerInput.playerIndex);
            GameObject camera = transform.Find("VCam").gameObject;
            camera.layer = playerLayers[playerInput.playerIndex].value;
        }

        private void SetCameraTarget(Transform cameraTarget)
        {
            CinemachineVirtualCamera vcam = GetComponentInChildren<CinemachineVirtualCamera>();

            vcam.Follow = cameraTarget;
            vcam.LookAt = cameraTarget;
        }

        private void SetSpawn()
        {
            int playerIndex = playerInput.playerIndex;

            transform.position = gameManager.GetSpawn(playerIndex).position;
            transform.rotation = gameManager.GetSpawn(playerIndex).rotation;
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
    }
}
