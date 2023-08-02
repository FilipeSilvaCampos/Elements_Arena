using ElementsArena.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    GameObject cursor;
    PlayerManager playerManager;
    PlayerInput playerInput;
    Selectable currentSelectable;
    Selectable nextSelectButton;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerManager = GetComponent<PlayerManager>();
    }

    public void SetCursor(GameObject cursor)
    {
        this.cursor = cursor;
        currentSelectable = FindObjectOfType<Selectable>();
        this.cursor.transform.position = currentSelectable.transform.position;
    }

    public void MoveCursor(InputAction.CallbackContext context)
    {
        if (cursor == null) return;

        Vector2 input = context.ReadValue<Vector2>();
        switch(input.x, input.y)
        {
            case (0, 1):
                MoveToTop();
                break;
            case (0, -1):
                MoveToBotton();
                break;
            case (1, 0):
                MoveToRight();
                break;
            case (-1, 0):
                MoveToLeft();
                break;
        }
    }

    public void Select(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if(currentSelectable == null) return;

            Character character = currentSelectable.GetComponent<ParamaterHolder>().GetCharacter();
            playerManager.SetCharacter(character);
        }
    }

    private void MoveCursor()
    {
        currentSelectable = nextSelectButton;
        cursor.transform.position = currentSelectable.transform.position;
    }

    private void MoveToTop()
    {
        nextSelectButton = currentSelectable.FindSelectableOnUp();
        if (nextSelectButton != null )
        {
            MoveCursor();
        }
    }

    private void MoveToBotton()
    {
        nextSelectButton = currentSelectable.FindSelectableOnDown();
        if(nextSelectButton != null)
        {
            MoveCursor();
        }
    }

    private void MoveToLeft()
    {
        nextSelectButton = currentSelectable.FindSelectableOnLeft();
        if(nextSelectButton != null)
        {
            MoveCursor();
        }
    }

    private void MoveToRight()
    {
        nextSelectButton = currentSelectable.FindSelectableOnRight();
        if (nextSelectButton != null)
        {
            MoveCursor();
        }

    }
}
