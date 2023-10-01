using ElementsArena.Damage;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class LaunchRock : Ability
    {
        [SerializeField] GameObject deffaultAttack;
        [SerializeField] Transform launchTransform;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] LayerMask rockLayer;
        [SerializeField] float domainDistance = 3;
        [SerializeField] float moveRockSpeed = 2;

        GameObject currentRock;
        Animator animator;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponentInChildren<Animator>();
        }

        protected override void OnReady()
        {
            if (called)
            {
                LockCharacterMovement();
                if(GetRockOnForward())
                {
                    FinishState();
                    return;
                }

                InvokeNewRock();
                FinishState();
            }
        }

        bool isLaunching = false;
        protected override void OnActive()
        {
            if (isLaunching) return;

            if (!OnTarget())
            {
                MoveToTarget();
                return;
            };

            animator.SetTrigger(AnimationKeys.LaunchTrigger);
            isLaunching = true;
        }

        protected override void OnCooldown()
        {
            if (IsTimeToChangeState()) FinishState();
        }

        //Animation Event
        public void Launch()
        {
            currentRock.GetComponent<Rock>().Launch(GetComponent<IDamageable>());
            UnlockCharacterMovement();
            isLaunching = false;
            FinishState();
        }

        bool GetRockOnForward()
        {
            Vector3 overlapPosition = transform.position + transform.forward * domainDistance;
            Collider[] hits = Physics.OverlapBox(overlapPosition, new Vector3(domainDistance, domainDistance, domainDistance), transform.rotation, rockLayer);

            if (hits.Length != 0)
            {
                currentRock = hits[0].gameObject;
                return true;
            }
            else return false;
        }

        void InvokeNewRock()
        {
            currentRock = Instantiate(deffaultAttack, GetInvokePosition(), launchTransform.rotation);
        }

        bool OnTarget()
        {
            bool onPosition = currentRock.transform.position == GetTargetPosition() ? true : false;
            bool onRotation = currentRock.transform.rotation == launchTransform.rotation ? true : false;

            return onPosition && onRotation;
        }

        void MoveToTarget()
        {
            currentRock.transform.position = Vector3.MoveTowards(currentRock.transform.position, GetTargetPosition(), moveRockSpeed * Time.deltaTime);

            currentRock.transform.rotation = launchTransform.rotation;
        }

        Vector3 GetTargetPosition()
        {
            Vector3 targetPosition = launchTransform.position;
            targetPosition.y = GroundHeight() + 1.8f; //Height character    

            return targetPosition;
        }

        Vector3 GetInvokePosition()
        {
            Vector3 invokePosition = launchTransform.position;
            invokePosition.y = GroundHeight() - deffaultAttack.transform.localScale.y / 2;

            return invokePosition;
        }

        float GroundHeight()
        {
            RaycastHit hit;

            Ray ray = new Ray(transform.position, Vector3.down);
            Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer);

            return hit.point.y;
        }
    }
}

