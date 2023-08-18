using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Make New Character", order = 0)]
public class Character : ScriptableObject
{
    public Sprite characterSprite;
    public GameObject prefab;
}
