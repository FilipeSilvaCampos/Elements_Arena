using UnityEngine;

namespace ElementsArena.Combat
{
    [RequireComponent(typeof(WaterStateManager))]
    public class WavesRage : WaterAbility
    {
        [SerializeField] Transform launchTransform;
        [SerializeField] GameObject prefab;
        [SerializeField] float waterNeedPerProjectile = 30;

        WaterProjectile currentAttack;

        protected override void OnReady()
        {
            if(called && waterStateManager.TakeWater(waterNeedPerProjectile))
            {
                LaunchWave(prefab);
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

        void LaunchWave(GameObject prefab)
        {
            currentAttack = Instantiate(prefab, launchTransform.position, launchTransform.rotation).GetComponent<WaterProjectile>();
        }

        void FreezeCurrentAttack()
        {
            if (currentAttack == null) return;

            currentAttack.Frozen();
        }
    }
}