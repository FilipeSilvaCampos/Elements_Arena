using UnityEngine;

namespace ElementsArena.Combat
{
    public class LaunchRock : Ability
    {
        [SerializeField] GameObject rock;
        [SerializeField] Transform launchTransform;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] LayerMask rockLayer;
        [SerializeField] float domainDistance = 2;
        [SerializeField] float moveRockSpeed = 2;
        [SerializeField] float rotateRockSpeed = 10;

        GameObject currentRock;
        Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        protected override void OnReady()
        {
            if (called)
            {
                AvailableToMove(false);
                if(GetRockOnForward())
                {
                    FinishState();
                    return;
                }

                InvokeNewRock();
                FinishState();
            }
        }

        protected override void OnActive()
        {
            if (OnTarget())
            {
                animator.SetTrigger(AnimationKeys.LaunchTrigger);
                AvailableToMove(true);
                FinishState();
                return;
            };
            
            MoveToTarget();
        }

        protected override void OnCooldown()
        {
            if (TimeToChangeState()) FinishState();
        }

        private void InvokeNewRock()
        {
            currentRock = Instantiate(rock, GetInvokePosition(), launchTransform.rotation);
        }

        private void MoveToTarget()
        {
            currentRock.transform.position = Vector3.MoveTowards(currentRock.transform.position, launchTransform.position, moveRockSpeed * Time.deltaTime);

            currentRock.transform.rotation = Quaternion.RotateTowards(currentRock.transform.rotation, launchTransform.rotation, rotateRockSpeed * Time.deltaTime);
        }

        private bool GetRockOnForward()
        {
            RaycastHit hit;

            Ray ray = new Ray(transform.position, Vector3.forward);
            Physics.Raycast(ray, out hit, domainDistance, rockLayer);

            if (hit.collider != null)
            {
                currentRock = hit.collider.gameObject;
                return true;
            }
            else return false;
        }

        private Vector3 GetInvokePosition()
        {
            Vector3 invokePosition = launchTransform.position;
            invokePosition.y = GroundHeight() - rock.transform.localScale.y / 2;

            return invokePosition;
        }

        private bool OnTarget()
        {
            bool onPosition = currentRock.transform.position == launchTransform.position ? true : false;
            bool onRotation = currentRock.transform.rotation == launchTransform.rotation ? true : false;

            return onPosition && onRotation;
        }

        //Animation Event
        public void Launch()
        {
            currentRock.GetComponent<RockBehaviour>().Launch();
        }

        private float GroundHeight()
        {
            RaycastHit hit;

            Ray ray = new Ray(transform.position, Vector3.down);
            Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer);

            return hit.point.y;
        }
    }
}

