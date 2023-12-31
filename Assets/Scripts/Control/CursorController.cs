﻿using ElementsArena.Core;
using ElementsArena.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace ElementsArena.Control
{
    public class CursorController : MonoBehaviour
    {
        PlayerManager playerManager;
        GameObject cursor;
        Selectable currentSelectable;
        Selectable nextSelectButton;

        public delegate void SelectedCharacter(Character character);
        public SelectedCharacter selectedCharacter;

        private void Start()
        {
            playerManager = GetComponent<PlayerManager>();
        }

        public void SetCursor(GameObject cursor)
        {
            cursor.SetActive(true);
            this.cursor = cursor;
            currentSelectable = FindObjectOfType<Selectable>();
        }

        public void MoveCursor(InputAction.CallbackContext context)
        {
            if (cursor == null) return;

            Vector2 input = context.ReadValue<Vector2>();
            switch (input.x, input.y)
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
                if (currentSelectable == null) return;

                SelectCharacter();
            }
        }

        public void UndoSelection(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
                GetComponent<PlayerManager>().UndoReady();
        }

        public void SelectCharacter()
        {
            Ray ray = new Ray(cursor.transform.position, Vector3.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray, 60);

            foreach (RaycastHit hit in hits)
            {
                CharacterButton button = hit.transform.GetComponent<CharacterButton>();
                if (button != null)
                {
                    playerManager.SetCharacter(button.character);
                    selectedCharacter(button.character);
                }
            }
        }

        private void MoveCursor()
        {
            currentSelectable = nextSelectButton;

            Vector3 nextPosition = currentSelectable.transform.position;
            cursor.transform.position = new Vector3(nextPosition.x, nextPosition.y, -10);
        }

        private void MoveToTop()
        {
            nextSelectButton = currentSelectable.FindSelectableOnUp();
            if (nextSelectButton != null)
            {
                MoveCursor();
            }
        }

        private void MoveToBotton()
        {
            nextSelectButton = currentSelectable.FindSelectableOnDown();
            if (nextSelectButton != null)
            {
                MoveCursor();
            }
        }

        private void MoveToLeft()
        {
            nextSelectButton = currentSelectable.FindSelectableOnLeft();
            if (nextSelectButton != null)
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
}
