using UnityEngine;

namespace ElementsArena.Combat
{
    public class FireRage : Ability
    {
        [SerializeField] FireScrtiptableProjectile[] projectiles;
        [SerializeField] Transform launchTransform;
        [SerializeField] float timeWithoutReaction = 1.8f;

        float timeSinceLastLaunch = 0;
        int counter = 0;

        protected override void Update()
        {
            base.Update();
            timeSinceLastLaunch += Time.deltaTime;
        }

        protected override void OnReady()
        {
            if(called)
            {
                LaunchAttack();
                FinishState();
            }
        }

        protected override void OnActive()
        {
            if (counter >= projectiles.Length || timeSinceLastLaunch > timeWithoutReaction) 
            {
                counter = 0;
                FinishState();
                return;
            }

            if(called)
            {
                if(timeSinceLastLaunch > projectiles[counter].launchTime)
                {
                    LaunchAttack();
                }
            }
        }

        protected override void OnCooldown()
        {
            if (TimeToChangeState()) FinishState();
        }

        private void LaunchAttack()
        {
            Instantiate(projectiles[counter].prefab, launchTransform.position, launchTransform.rotation);
            timeSinceLastLaunch = 0;
            counter++;
        }
    }
}
