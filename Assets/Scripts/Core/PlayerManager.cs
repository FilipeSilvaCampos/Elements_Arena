using Cinemachine;
using ElementsArena.Combat;
using ElementsArena.Movement;
using ElementsArena.Control;
using ElementsArena.Damage;
using UnityEngine;

namespace ElementsArena.Core
{
    public class PlayerActionMapsKeys
    {
        public const string UI = "UI";
        public const string InGame = "Player";
    }

    public class PlayerManager : MonoBehaviour
    {
        LevelManager levelManager;
        Character character;
        PlayerController playerController;
        public bool isReady { get; private set; } = false;

        private void Awake()
        {
            levelManager = FindObjectOfType<LevelManager>();
            playerController = GetComponent<PlayerController>();
        }

        private void OnLoose()
        {
            levelManager.GameOver(gameObject);
        }

        public GameObject SetUpPlayer(Transform spawnPosition, LayerMask layer, Camera camera)
        {
            GameObject fighter = Instantiate(character.prefab, transform.position, transform.rotation);
            IDamageable benderDamageable = fighter.GetComponent<IDamageable>();

            benderDamageable.OnDeath += OnLoose;
            SetCamera(fighter, layer);
            SetPosition(fighter ,spawnPosition);
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

        private void SetPosition(GameObject fighter , Transform spawn)
        {
            fighter.transform.position = spawn.position;
            fighter.transform.rotation = spawn.rotation;
        }

        private void SetHUD(Camera camera, GameObject fighter)
        {
            GameObject uI = Instantiate(character.uIPrefab, fighter.transform);

            Canvas uICanvas = uI.GetComponentInChildren<Canvas>();
            uICanvas.worldCamera = camera;
            uICanvas.planeDistance = 1;

            uI.GetComponent<UIManager>().Initialize(fighter);
        }

        public void UndoReady()
        {
            isReady = false;
        }

        public void SetCharacter(Character character)
        {
            if (isReady) return;

            this.character = character;
            isReady = true;
        }

        public Character GetCharacter()
        {
            return character;
        }
    }
}
