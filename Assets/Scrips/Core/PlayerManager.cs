using Cinemachine;
using ElementsArena.Combat;
using ElementsArena.Movement;
using ElementsArena.Control;
using ElementsArena.Damage;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ElementsArena.Core
{
    public class PlayerManager : MonoBehaviour
    {
        GameManager gameManager;
        Character character;
        PlayerInput playerInput;
        CursorController cursorController;
        PlayerController playerController;

        private void Awake()
        {
            cursorController = GetComponent<CursorController>();
            playerInput = GetComponent<PlayerInput>();
            gameManager = FindObjectOfType<GameManager>();
            playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            gameManager.OnSelectScreen += SetCursor;
        }

        private void SetCursor()
        {
            cursorController.SetCursor(FindObjectOfType<MenuManager>().GetCursor(playerInput.playerIndex));
        }

        private void SetCamera(GameObject fighter, LayerMask playerLayer)
        {
            Transform cameraTarget = fighter.GetComponentInChildren<CameraController>().GetCameraTarget();
            CinemachineVirtualCamera vcam = fighter.GetComponentInChildren<CinemachineVirtualCamera>();
            GameObject cameraObject = vcam.gameObject;

            vcam.Follow = cameraTarget;
            vcam.LookAt = cameraTarget;
            cameraObject.layer = (int)Mathf.Log(playerLayer.value, 2);
        }

        private void SetPosition(Transform spawn)
        {
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
        }

        private void OnLoose()
        {
            gameManager.GameOver(gameObject);
        }

        public GameObject SetUpPlayer()
        {
            GameObject fighter = Instantiate(character.prefab, transform);
            IDamageable benderDamageable = fighter.GetComponent<IDamageable>();

            benderDamageable.OnDeath += OnLoose;
            SetCamera(fighter, gameManager.GetPlayerLayer(playerInput.playerIndex));
            SetPosition(gameManager.GetPlayerSpawn(playerInput.playerIndex));

            playerController.SetUpController
            (
            fighter.GetComponentInChildren<CharacterMovement>(),
            fighter.GetComponent<AbilityWrapper>(),
            fighter.GetComponentInChildren<CameraController>()
            );

            return fighter;
            //attributesMenu.GetComponent<AttributesDisplay>().SetUpAttributes(benderDamageable);
            //benderDamageable.DeathEvent += OnBenderDeath;
        }

        public void UndoReady()
        {
            gameManager.UnreadyPlayer(playerInput.playerIndex);
        }

        public void SetCharacter(Character character)
        {
            this.character = character;
        }

        public Character GetCharacter()
        {
            return character;
        }
    }
}
