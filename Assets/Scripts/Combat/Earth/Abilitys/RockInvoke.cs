using UnityEngine;

namespace ElementsArena.Combat
{
    [System.Serializable]
    class RockToInvoke
    {
        public GameObject prefab;
        public float timeToElevate = 10;

        public Vector3 GetPrefabScale() => prefab.transform.localScale;
    }

    public class RockInvoke : EarthAbility
    {
        [Header("Invoke Attributes")]
        [SerializeField] RockToInvoke[] rocksToInvoke;
        [SerializeField] float invokeDistance = 2;
        [SerializeField] Transform launchTransform;

        RockToInvoke selectedRock;
        Vector2 selectionInput;

        GameObject currentRock;
        float vToElevate;

        bool execute = false;

        protected override void Update()
        {
            base.Update();
            selectionInput = abilityHolder.selectionInput;
            execute = selectionInput != Vector2.zero ? true : false;
        }

        protected override void OnReady()
        {
            if(called)
            {
                if (HaveRockOnForward(out GameObject NA)) return;

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
            if (!OnTargetPosition())
            {
                MoveToTarget();
                return;
            }

            FinishState();
        }

        protected override void OnCooldown()
        {
            if (IsTimeToChangeState()) FinishState();
        }

        void SelectAttack()
        {
            switch (selectionInput.x, selectionInput.y)
            {
                case (0, 1):
                    selectedRock = rocksToInvoke[0];
                    break;
                case (1, 0):
                    selectedRock = rocksToInvoke[1];
                    break;
                case (0, -1):
                    selectedRock = rocksToInvoke[2];
                    break;
                case (-1, 0):
                    selectedRock = rocksToInvoke[3];
                    break;
            }
        }

        void InvokeNewRock()
        {
            currentRock = Instantiate(selectedRock.prefab, GetInvokePosition(), launchTransform.rotation);
            vToElevate = (Vector3.Distance(GetTargetPosition(), currentRock.transform.position) / selectedRock.timeToElevate) * Time.deltaTime;
        }

        Vector3 GetInvokePosition()
        {
            Vector3 invokePosition = transform.position + launchTransform.forward * (invokeDistance + selectedRock.GetPrefabScale().y / 2);
            invokePosition.y = GroundHeight() - selectedRock.GetPrefabScale().y / 2;

            return invokePosition;
        }

        void MoveToTarget()
        {
            currentRock.transform.position = Vector3.MoveTowards(currentRock.transform.position, GetTargetPosition(),vToElevate);
            //currentRock.transform.position = Vector3.MoveTowards(currentRock.transform.position, GetTargetPosition(), selectedRock.elevateTime * Time.timeScale);
        }

        Vector3 GetTargetPosition()
        {
            Vector3 targetPosition = currentRock.transform.position;
            targetPosition.y = GroundHeight() + selectedRock.GetPrefabScale().y / 2;

            return targetPosition;
        }

        bool OnTargetPosition()
        {
            return currentRock.transform.position == GetTargetPosition();
        }
    }
}
