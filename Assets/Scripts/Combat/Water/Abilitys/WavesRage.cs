using System.Collections;
using UnityEngine;

namespace ElementsArena.Combat
{
    [RequireComponent(typeof(WaterStateManager))]
    public class WavesRage : WaterAbility
    {
        [Header("Wave Properties")]
        [SerializeField] Transform launchTransform;
        [SerializeField] GameObject wavePrefab;
        [SerializeField] float waterNeedPerProjectile = 30;
        [Header("Delivery Attributes")]
        [SerializeField] GameObject deliveryPrefab;
        [SerializeField] [Min(7)] float deliverySpeed = 10;

        WaterProjectile currentAttack;
        float targetHeightToDelivery = 2f;

        WaterSource currenteSource;

        protected override void OnReady()
        {
            if(called && waterStateManager.TakeWater(waterNeedPerProjectile, out currenteSource))
            {
                StartCoroutine(LaunchProjectile(currenteSource.transform.position, launchTransform.position));
                FinishState();
            }
        }

        protected override void OnActive()
        {
            if (abilityHolder.suportButton)
            {
                FreezeCurrentAttack();
            }

            if (IsTimeToChangeState())
            {
                currentAttack = null;
                FinishState();
            }
        }

        protected override void OnCooldown()
        {
            if (IsTimeToChangeState()) FinishState();
        }

        void FreezeCurrentAttack()
        {
            if (currentAttack == null) return;

            currentAttack.Frozen();
        }

        IEnumerator LaunchProjectile(Vector3 startPosition, Vector3 targetPosition)
        {
            GameObject water = Instantiate(deliveryPrefab, startPosition, Quaternion.identity);

            Vector3 heightToUp = water.transform.position + Vector3.up * targetHeightToDelivery;
            while (water.transform.position != heightToUp)
            {
                water.transform.position = Vector3.MoveTowards(water.transform.position, heightToUp, Time.deltaTime * deliverySpeed);
                yield return null;
            }

            while (water.transform.position != targetPosition)
            {
                water.transform.position = Vector3.MoveTowards(water.transform.position, targetPosition, Time.deltaTime * deliverySpeed);
                yield return null;
            }

            currentAttack = Instantiate(wavePrefab, launchTransform.position, launchTransform.rotation).GetComponent<WaterProjectile>();
            Destroy(water);
        }
    }
}