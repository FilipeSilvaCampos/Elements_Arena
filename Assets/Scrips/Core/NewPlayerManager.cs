using Cinemachine;
using ElementsArena.Combat;
using ElementsArena.Damage;
using ElementsArena.Movement;
using ElementsArena.Prototype;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerManager : MonoBehaviour
{
    NewGameManager gameManager;
    PlayerInput playerInput;
    CursorController cursorController;
    PlayerController playerController;

    Character selectedCharacter = null; 
    public bool ready;
    private void Awake()
    {
        cursorController = GetComponent<CursorController>();
        playerInput = GetComponent<PlayerInput>();
        gameManager = FindObjectOfType<NewGameManager>();
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        gameManager.onLocalMultiplayerMode += SetCursor;
    }

    private void SetCursor()
    {
        cursorController.Cursor = gameManager.GetCursor(playerInput.playerIndex);
    }

    private void SetCamera(Transform cameraTarget, LayerMask playerLayer)
    {
        CinemachineVirtualCamera vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        GameObject camera = vcam.gameObject;

        vcam.Follow = cameraTarget;
        vcam.LookAt = cameraTarget;
        camera.layer = (int)Mathf.Log(playerLayer.value, 2);
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

        SetCamera(bender.transform.Find("CameraTarget"), playerLayer);
        playerController.SetUpController(bender.GetComponent<CharacterMovement>(), bender.GetComponent<AbilityWrapper>());
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