using ElementsArena.Combat;
using ElementsArena.Core;
using ElementsArena.Damage;
using ElementsArena.Movement;
using UnityEngine;

namespace ElementsArena.Control
{
    public class AIController : MonoBehaviour
    {
        CharacterMovement characterMovement;
        AbilityWrapper abilityWrapper;

        private void Start()
        {
            GetComponent<IDamageable>().OnDeath += OnLoose;
        }

        void OnLoose()
        {
            FindObjectOfType<GameManager>().GameOver(gameObject);
        }
    }
}