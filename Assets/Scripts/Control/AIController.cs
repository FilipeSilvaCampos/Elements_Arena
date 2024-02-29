using ElementsArena.Core;
using ElementsArena.Damage;
using ElementsArena.Movement;
using UnityEngine;

namespace ElementsArena.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] Vector2 debugInput;

        CharacterMovement characterMovement;

        private void Start()
        {
            characterMovement = GetComponent<CharacterMovement>();
            GetComponent<IDamageable>().OnDeath += OnLoose;
        }

        private void Update()
        {
            characterMovement.SetInput(new CharacterMovementInput
            {
                MoveInput = debugInput
            });    
        }

        void OnLoose()
        {
            FindObjectOfType<LevelManager>().GameOver(gameObject);
        }
    }
}