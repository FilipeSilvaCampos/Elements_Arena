using DG.Tweening;
using ElementsArena.Core;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCharacter : MonoBehaviour
{
    [SerializeField] float punchDuration = .3f;

    Image image;
    GameObject followCursor;
    PlayerManager player;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if(player != null && player.isReady)
        {
            image.sprite = player.GetCharacter().characterSprite;
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
            if(character.sprite != image.sprite)
            {
                ChangePreviewImage(character.sprite);
            }
        }
    }

    void ChangePreviewImage(Sprite sprite)
    {
        transform.DOPunchPosition(Vector3.down, punchDuration, 20);
        image.sprite = sprite;
    }

    public void SetPlayer(PlayerManager player)
    {
        this.player = player;
    }

    public void SetFollowCursor(GameObject cursor)
    {
        followCursor = cursor;
    }
}
