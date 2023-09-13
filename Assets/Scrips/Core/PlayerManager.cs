using Cinemachine;
using ElementsArena.Combat;
using ElementsArena.Movement;
using ElementsArena.Control;
using ElementsArena.Damage;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

namespace ElementsArena.Core
{
    public class PlayerManager : MonoBehaviour
    {
        GameManager gameManager;
        Character character;
        PlayerInput playerInput;
        PlayerController playerController;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            gameManager = FindObjectOfType<GameManager>();
            playerController = GetComponent<PlayerController>();
        }

        private void OnLoose()
        {
            gameManager.GameOver(gameObject);
        }

        public GameObject SetUpPlayer(Transform spawnPosition, LayerMask layer, Camera camera)
        {
            GameObject fighter = Instantiate(character.prefab, transform);
            //GameObject fighter = PhotonNetwork.Instantiate(character.prefab.name, transform.position, transform.rotation);
            IDamageable benderDamageable = fighter.GetComponent<IDamageable>();

            benderDamageable.OnDeath += OnLoose;
            SetCamera(fighter, layer);
            SetPosition(spawnPosition);
            SetHUD(camera, fighter);

            playerController.SetUpController
            (
            fighter.GetComponent<CharacterMovement>(),
            fighter.GetComponent<AbilityHolder>(),
            fighter.GetComponentInChildren<CameraController>()
            );

            return fighter;
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

        private void SetHUD(Camera camera, GameObject fighter)
        {
            GameObject uI = Instantiate(character.uIPrefab, transform);

            Canvas uICanvas = uI.GetComponentInChildren<Canvas>();
            uICanvas.worldCamera = camera;
            uICanvas.planeDistance = 1;

            uI.GetComponent<UIManager>().Initialize(fighter);
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
