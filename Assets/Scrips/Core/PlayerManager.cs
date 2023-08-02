using Cinemachine;
using ElementsArena.Combat;
using ElementsArena.Damage;
using ElementsArena.Movement;
using ElementsArena.Prototype;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ElementsArena.Core
{
    public class PlayerManager : MonoBehaviour
    {
        GameManager gameManager;
        PlayerInput playerInput;
        CursorController cursorController;
        PlayerController playerController;

        Character selectedCharacter = null;
        public bool ready;
        private void Awake()
        {
            cursorController = GetComponent<CursorController>();
            playerInput = GetComponent<PlayerInput>();
            gameManager = FindObjectOfType<GameManager>();
            playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            gameManager.onSelectScreen += SetCursor;
        }

        private void SetCursor()
        {
            cursorController.SetCursor(gameManager.GetCursor(playerInput.playerIndex));
        }

        private void SetCamera(GameObject bender, LayerMask playerLayer)
        {
            Transform cameraTarget = bender.GetComponentInChildren<CameraController>().GetCameraTarget();
            CinemachineVirtualCamera vcam = bender.GetComponentInChildren<CinemachineVirtualCamera>();
            GameObject cameraObject = vcam.gameObject;

            vcam.Follow = cameraTarget;
            vcam.LookAt = cameraTarget;
            cameraObject.layer = (int)Mathf.Log(playerLayer.value, 2);
        }

        private void SetSpawn(Spawn spawn)
        {
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
        }

        public void SetUpPlayer(LayerMask playerLayer)
        {
            GameObject bender = Instantiate(selectedCharacter.prefab, transform);
            IDamageable benderDamageable = bender.GetComponent<IDamageable>();

            SetCamera(bender, playerLayer);

            playerController.SetUpController
            (
            bender.GetComponent<CharacterMovement>(),
            bender.GetComponent<AbilityWrapper>(),
            bender.GetComponentInChildren<CameraController>()
            );
            //attributesMenu.GetComponent<AttributesDisplay>().SetUpAttributes(benderDamageable);
            //benderDamageable.DeathEvent += OnBenderDeath;
        }

        public void SetCharacter(Character character)
        {
            selectedCharacter = character;
            ready = true;
            gameManager.StartGame();
        }

        public Character GetCharacter()
        {
            return selectedCharacter;
        }
    }
}
