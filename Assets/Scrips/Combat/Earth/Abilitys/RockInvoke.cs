using UnityEngine;

namespace ElementsArena.Combat
{
    public class RockInvoke : Ability
    {
        [SerializeField] EarthScriptableAttack[] attacks;
        [SerializeField] float invokeDistance = 2;
        [SerializeField] LayerMask groundLayer;

        EarthScriptableAttack selectedAttack;
        GameObject currentRock;
        bool execute = false;
        AbilityWrapper abilityWrapper;
        Vector2 selectionInput;

        private void Start()
        {
            abilityWrapper = GetComponent<AbilityWrapper>();    
        }

        protected override void Update()
        {
            base.Update();
            selectionInput = abilityWrapper.selectionInput;
            execute = selectionInput != Vector2.zero ? true : false;
        }

        protected override void OnReady()
        {
            if(called)
            {
                AvailableToMove(false);
                if(execute)
                {
                    ChangeRock();
                    InvokeNewRock();
                    FinishState();
                    return;
                }
                AvailableToMove(true);
            }
        }

        protected override void OnActive()
        {
            if (!AboveGround())
            {
                ElevateRock();
                return;
            }
            AvailableToMove(true);
            FinishState();
        }

        protected override void OnCooldown()
        {
            if (TimeToChangeState()) FinishState();
        }

        private void InvokeNewRock()
        {
            currentRock = Instantiate(selectedAttack.prefab, GetInvokePosition(), transform.rotation);
        }

        private void ChangeRock()
        {
            switch(selectionInput.x, selectionInput.y)
            {
                case (0, 1):
                    selectedAttack = attacks[0];
                    break;
                case (1, 0):
                    selectedAttack = attacks[1];
                    break;
                case (0, -1):
                    selectedAttack = attacks[2];
                    break;
                case (-1, 0):
                    selectedAttack = attacks[3];
                    break;
            }
        }

        private Vector3 GetInvokePosition()
        {
            Vector3 invokePosition = transform.position;
            invokePosition += transform.forward * (invokeDistance + selectedAttack.GetPrefabScale().y /2);
            invokePosition.y = GroundHeight() - selectedAttack.GetPrefabScale().y / 2;

            return invokePosition;
        }

        private void ElevateRock()
        {
            currentRock.transform.position += Vector3.up * selectedAttack.elevateSpeed * Time.deltaTime;
        }

        private bool AboveGround()
        {
            Vector3 rockPosition = currentRock.transform.position;

            if (rockPosition.y - selectedAttack.GetPrefabScale().y / 2 > GroundHeight()) return true;
            else return false;
        }

        private float GroundHeight()
        {
            RaycastHit hit;
            Vector3 position = transform.position + Vector3.forward * invokeDistance;

            Ray ray = new Ray(position, Vector3.down);
            Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer);

            return hit.point.y;
        }
    }
}
