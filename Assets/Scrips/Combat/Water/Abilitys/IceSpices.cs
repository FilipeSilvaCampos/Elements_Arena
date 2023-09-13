using UnityEngine;

namespace ElementsArena.Combat
{
    public class IceSpices : WaterAbility
    {
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] Transform launchTransform;
        [SerializeField] float timeBetweenLaunchs = .3f;
        [SerializeField] int countWithoutWater = 3;
        [SerializeField] int countWithWater = 10;
        [SerializeField] float projectileWater = 2;

        int counter = 0;
        float timeSinceLastLaunch;

        protected override void OnReady()
        {
            if(called)
            {
                LaunchProjectile(projectilePrefab);
                FinishState();
            }
        }

        protected override void OnActive()
        {
            if (counter == countWithWater)
            {
                counter = 0;
                FinishState();
                return;
            }
            

            if (called && timeSinceLastLaunch > timeBetweenLaunchs)
            {
                if(counter >= countWithoutWater && !waterStateManager.TakeWater(projectileWater))
                {
                    counter = 0;
                    FinishState();
                    return;
                }

                LaunchProjectile(projectilePrefab);
            }

            timeSinceLastLaunch += Time.deltaTime;
        }

        protected override void OnCooldown()
        {
            if(IsTimeToChangeState()) FinishState();
        }

        void LaunchProjectile(GameObject projectile)
        {
            Instantiate(projectile, launchTransform.position, launchTransform.rotation);
            timeSinceLastLaunch = 0;
            counter++;
        }
    }
}