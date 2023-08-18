using ElementsArena.Control;
using ElementsArena.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
struct PlayerPreview
{
    public PlayerEnum player;
    public PreviewCharacter previewRect;
}

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject selectModeMenu;
    [SerializeField] GameObject quitGameWarning;

    [Header("Character Selection Screen")]
    [SerializeField] GameObject selectCharacterScreen;
    [SerializeField] PlayerPreview[] previews;
    [SerializeField] TextMeshProUGUI player2Message;
    [SerializeField] GameObject[] cursors;

    GameManager gameManager;
    PlayerInputManager playerInputManager;
    GameState currentState;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        playerInputManager.onPlayerJoined += SetPlayerCursor;

        foreach (PlayerPreview preview in previews)
        {
            preview.previewRect.SetFollowCursor(cursors[(int)preview.player]);
        }    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentState)
            {
                case GameState.OnSelectGameMode:
                    quitGameWarning.SetActive(true);
                    break;
                case GameState.OnsSelectCharacter:
                    gameManager.gameState = GameState.OnSelectGameMode;
                    break;
            } 
        }

        player2Message.gameObject.SetActive(playerInputManager.playerCount == 1);

        if(gameManager.gameState != currentState) SwitchMode(gameManager.gameState);

        if(currentState == GameState.OnsSelectCharacter)
        {
            UpdateCharacterSceen();
        }
        
    }

    void SwitchMode(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.OnSelectGameMode:
                selectModeMenu.SetActive(true);
                selectCharacterScreen.SetActive(false);
                break;

            case GameState.OnsSelectCharacter:
                selectModeMenu.SetActive(false);
                selectCharacterScreen.SetActive(true);
                break;
        }
        currentState = gameState;
    }

    void UpdateCharacterSceen()
    {
        foreach(PlayerPreview preview in previews) 
        {
            Player player = gameManager.GetPlayer((int)preview.player);
            if (player != null)
            {
                preview.previewRect.SetPlayer(player);
            }
        }

        if(playerInputManager.playerCount == 1 && gameManager.GetPlayer(0).ready)
        {
            previews[(int)PlayerEnum.Player2].previewRect.SetFollowCursor(cursors[0]);
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void HideMenu(GameObject menuToHide)
    {
        menuToHide.SetActive(false);
    }

    void SetPlayerCursor(PlayerInput player)
    {
        player.gameObject.GetComponent<CursorController>().SetCursor(cursors[player.playerIndex]);
    }

    public void SetGameLocalMode()
    {
        gameManager.SetLocalMode();
    }

    public GameObject GetCursor(int playerIndex)
    {
        return cursors[playerIndex];
    }
}
