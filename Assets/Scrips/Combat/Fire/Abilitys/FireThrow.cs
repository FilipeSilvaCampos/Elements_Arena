using ElementsArena.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class FireThrow : Ability
    {
        [SerializeField] float maxFlySpeed = 5;
        [SerializeField] float flyAcceleration = 15;
        [SerializeField] float flyHeight = 3;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] GameObject throwEffect;
        [SerializeField] Transform footTransform;

        Rigidbody characterRb;
        CharacterMovement characterMovement;

        private void Start()
        {
            characterRb = GetComponent<Rigidbody>();
            characterMovement = GetComponent<CharacterMovement>();
        }

        private void FixedUpdate()
        {
            if (currentState == AbilityStates.active) FlyMovement();
        }

        protected override void OnReady()
        {
            if (called)
            {
                Destroy(Instantiate(throwEffect, footTransform), activeTime);
                characterRb.useGravity = false;
                FinishState();
            }
        }

        protected override void OnActive()
        {
            ControlHeight();
            FlyMovement();

            if (TimeToChangeState()) 
            {
                characterRb.useGravity = true;
                FinishState();
            }
        }

        protected override void OnCooldown()
        {
            if (TimeToChangeState()) FinishState();
        }

        private float GroundHeight()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);
            Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer);

            return hit.point.y;
        }

        private void FlyMovement()
        {
            characterRb.AddRelativeForce(characterMovement.GetDirection() * flyAcceleration, ForceMode.Force);

            Vector3 limitedSpeed;
            if (characterRb.velocity.magnitude > maxFlySpeed)
            {
                limitedSpeed = characterRb.velocity.normalized * maxFlySpeed;
                characterRb.velocity = new Vector3(limitedSpeed.x, characterRb.velocity.y, limitedSpeed.z);
            }
        }

        private void ControlHeight()
        {
            if(transform.position.y - GroundHeight() < flyHeight)
            {
                transform.position += Vector3.up * maxFlySpeed * Time.deltaTime;
            }

            if(transform.position.y - GroundHeight() > flyHeight)
            {
                transform.position += Vector3.down * maxFlySpeed * Time.deltaTime;
            }
        }
    }
}
