using ElementsArena.Combat;
using ElementsArena.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using ElementsArena.Damage;

namespace ElementsArena.Prototype
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] GameObject startMenu;
        [SerializeField] GameObject attributesMenu;
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

            startMenu.SetActive(true);
            attributesMenu.SetActive(false);
            ConfigCamera();
        }

        private void SetUpNewBender()
        {
            currentBender = Instantiate(selectedBender, transform);
            IDamageable benderDamageable = currentBender.GetComponent<IDamageable>();


            SetSpawn();
            SetCameraTarget(currentBender.transform.Find("CameraTarget"));
            playerController.SetUpController(currentBender.GetComponent<CharacterMovement>(), currentBender.GetComponent<AbilityWrapper>());

            attributesMenu.GetComponent<AttributesDisplay>().SetUpAttributes(benderDamageable);
            benderDamageable.DeathEvent += OnBenderDeath;
        }

        private void ConfigCamera()
        {
            GetComponentInChildren<Canvas>().worldCamera = gameManager.GetCamera(playerInput.playerIndex);
            GameObject camera = transform.Find("VCam").gameObject;
            camera.layer = (int)Mathf.Log(playerLayers[playerInput.playerIndex].value, 2);
            GetComponentInChildren<Canvas>().planeDistance = 1;
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

        private void OnBenderDeath()
        {
            playerController.alive = false;
            attributesMenu.SetActive(false);
            startMenu.SetActive(true);
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
            attributesMenu.SetActive(true);
        }
    }
}
