using ElementsArena.Movement;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class FrozenOnBirth : MonoBehaviour
    {
        [SerializeField] float lifeTime = 2;
        [SerializeField] LayerMask charactersLayer;

        CharacterMovement[] frozenCharacters;

        private void Start()
        {
            FrozenPlayers();

            Destroy(gameObject, lifeTime);
        }

        private void FrozenPlayers()
        {
            Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation, charactersLayer);

            frozenCharacters = new CharacterMovement[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                CharacterMovement character = hits[i].GetComponent<Collider>().GetComponent<CharacterMovement>();
                character.LockMovement(true);
                frozenCharacters[i] = character;
            }
        }

        private void OnDestroy()
        {
            foreach(CharacterMovement character in frozenCharacters)
            {
                character.LockMovement(false);
            }
        }
    }
}