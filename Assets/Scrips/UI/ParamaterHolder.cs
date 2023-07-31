using UnityEngine;


public class ParamaterHolder : MonoBehaviour
{
    [SerializeField] Character character;

    public Character GetCharacter()
    {
        return character;
    }
}
