using ElementsArena.Core;
using ElementsArena.UI;
using System;
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
    [SerializeField] GameObject gameQuitWarning;
    [SerializeReference] Menu[] menus;

    //Control Vars
	int menuIndex = 0;
    public GameManager gameManager { get; private set; }
    public PlayerInputManager playerInputManager { get; private set; }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerInputManager = gameManager.GetComponent<PlayerInputManager>();

        for(int i = 1; i < menus.Length; i++)
        {
            menus[i].Hide();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuIndex == 0)
            {
                gameQuitWarning.SetActive(true);
            }
            else
            {
                BackMenu();
            }
        }
    }

    public void ThrowMenu()
    {
        menus[menuIndex].Hide();

        menuIndex++;
        menus[menuIndex].Show();
    }

    public void BackMenu()
    {
        menus[menuIndex].Hide();

        menuIndex--;
        menus[menuIndex].Show();
    }
    
    public void HideMenu(GameObject menuToHide)
    {
        menuToHide.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
