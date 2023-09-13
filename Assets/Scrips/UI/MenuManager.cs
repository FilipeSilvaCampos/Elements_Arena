using ElementsArena.Control;
using ElementsArena.Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[Serializable]
struct PlayerPreview
{
    public PlayerEnum player;
    public PreviewCharacter previewRect;
}

public enum MenuState
{
    SelectingGameMode,
    SelectingCharacter
}

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject selectModeMenu;
    [SerializeField] GameObject gameQuitWarning;

    [Header("Character Selection Screen")]
    [SerializeField] GameObject selectCharacterScreen;
    [SerializeField] PlayerPreview[] previews;
    [SerializeField] GameObject player2Warning;
    [SerializeField] GameObject[] cursors;

    GameManager gameManager;
    PlayerInputManager playerInputManager;
    MenuState currentState = MenuState.SelectingGameMode;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        playerInputManager.onPlayerJoined += SetSelectSreen;

        SwitchMode(currentState);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentState)
            {
                case MenuState.SelectingGameMode:
                    gameQuitWarning.SetActive(true);
                    break;
                case MenuState.SelectingCharacter:
                    SwitchMode(MenuState.SelectingGameMode);
                    break;
            } 
        }

        player2Warning.SetActive(playerInputManager.playerCount == 1);

        if(currentState == MenuState.SelectingCharacter)
        {
            UpdateCharacterSceen();
        }     
    }

    void SetPreviewsCursors()
    {
        foreach (PlayerPreview preview in previews)
        {
            preview.previewRect.SetFollowCursor(cursors[(int)preview.player]);
        }
    }

    void SwitchMode(MenuState state)
    {
        switch(state)
        {
            case MenuState.SelectingGameMode:
                selectModeMenu.SetActive(true);
                selectCharacterScreen.SetActive(false);
                break;

            case MenuState.SelectingCharacter:
                selectModeMenu.SetActive(false);
                selectCharacterScreen.SetActive(true);
                SetSelectSreen(null);

                Vector3 cursorDeffaultPositon = FindObjectOfType<Selectable>().transform.position + Vector3.forward * -10;
                foreach (GameObject cursor in cursors)
                {
                    cursor.transform.position = cursorDeffaultPositon;
                }

                break;
        }
        currentState = state;
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

    void SetSelectSreen(PlayerInput player)
    {//The parameter is just for subscribe on OnPlyerJoined event
        for(int i = 0; i < playerInputManager.playerCount; i++)
        {
            GameObject currentPlayer = gameManager.GetPlayer(i).gameObject;
            currentPlayer.GetComponent<CursorController>().SetCursor(cursors[i]);
        }

        SetPreviewsCursors();
    }

    public void SetGameLocalMode()
    {
        SwitchMode(MenuState.SelectingCharacter);
        gameManager.isOnlineMode = false;
    }

    public void SetGameOnlineMode()
    {
        SwitchMode(MenuState.SelectingCharacter);
        gameManager.isOnlineMode = true;
    }
}
