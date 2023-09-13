using Photon.Pun;
using UnityEngine;

namespace ElementsArena.Combat
{
    [System.Serializable]
    class RockToInvoke
    {
        public GameObject prefab;
        public float elevateSpeed = 10;

        public Vector3 GetPrefabScale() => prefab.transform.localScale;
    }

    public class RockInvoke : Ability
    {
        [SerializeField] RockToInvoke[] rocksToInvoke;
        [SerializeField] float invokeDistance = 2;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] Transform launchTransform;

        RockToInvoke selectedRock;
        Vector2 selectionInput;

        GameObject currentRock;
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
            //currentRock = PhotonNetwork.Instantiate(selectedRock.prefab.name, GetInvokePosition(), launchTransform.rotation);
            currentRock = Instantiate(selectedRock.prefab, GetInvokePosition(), launchTransform.rotation);
        }

        Vector3 GetInvokePosition()
        {
            Vector3 invokePosition = transform.position + launchTransform.forward * (invokeDistance + selectedRock.GetPrefabScale().y / 2);
            invokePosition.y = GroundHeight() - selectedRock.GetPrefabScale().y / 2;

            return invokePosition;
        }

        void MoveToTarget()
        {
            currentRock.transform.position = Vector3.MoveTowards(currentRock.transform.position, GetTargetPosition(), selectedRock.elevateSpeed * Time.deltaTime);
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
