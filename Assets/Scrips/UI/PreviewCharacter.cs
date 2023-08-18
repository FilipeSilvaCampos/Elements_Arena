using ElementsArena.Core;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCharacter : MonoBehaviour
{
    Image image;
    GameObject followCursor;
    Player player;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if(player != null && player.ready)
        {
            image.sprite = player.playerManager.GetCharacter().characterSprite;
            return;
        }

        CastCharacter();
    }

    void CastCharacter()
    {
        RaycastHit hit;
        if(Physics.Raycast(followCursor.transform.position, Vector3.forward, out hit))
        {
            Image character = hit.collider.GetComponent<Image>();
            if(character != null)
            {
                image.sprite = character.sprite;
            }
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void SetFollowCursor(GameObject cursor)
    {
        followCursor = cursor;
    }
}
