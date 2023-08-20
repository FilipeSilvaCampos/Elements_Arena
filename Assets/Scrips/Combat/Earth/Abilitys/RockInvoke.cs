using UnityEngine;

namespace ElementsArena.Combat
{
    public class RockInvoke : Ability
    {
        [SerializeField] EarthScriptableAttack[] attacks;
        [SerializeField] float invokeDistance = 2;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] Transform launchTransform;

        EarthScriptableAttack selectedAttack;
        AbilityWrapper abilityWrapper;
        Vector2 selectionInput;

        GameObject currentRock;
        bool execute = false;

        private void Awake()
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
                if(execute)
                {
                    SelectAttack();
                    InvokeNewRock();
                    FinishState();
                    return;
                }
            }
        }

        protected override void OnActive()
        {
            if (!AboveGround())
            {
                ElevateRock();
                return;
            }

            FinishState();
        }

        protected override void OnCooldown()
        {
            if (TimeToChangeState()) FinishState();
        }

        void SelectAttack()
        {
            switch (selectionInput.x, selectionInput.y)
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

        void InvokeNewRock()
        {
            currentRock = Instantiate(selectedAttack.prefab, GetInvokePosition(), launchTransform.rotation);
        }

        Vector3 GetInvokePosition()
        {
            Vector3 invokePosition = transform.position + launchTransform.forward * (invokeDistance + selectedAttack.GetPrefabScale().y / 2);
            invokePosition.y = GroundHeight() - selectedAttack.GetPrefabScale().y / 2;

            return invokePosition;
        }

        void ElevateRock()
        {
            currentRock.transform.position += Vector3.up * selectedAttack.elevateSpeed * Time.deltaTime;
        }

        bool AboveGround()
        {
            Vector3 rockPosition = currentRock.transform.position;

            if (rockPosition.y - selectedAttack.GetPrefabScale().y / 2 > GroundHeight()) return true;
            else return false;
        }

        float GroundHeight()
        {
            RaycastHit hit;
            Vector3 position = transform.position + Vector3.forward * invokeDistance;

            Ray ray = new Ray(position, Vector3.down);
            Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer);

            return hit.point.y;
        }
    }
}
