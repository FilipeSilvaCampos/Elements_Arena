using UnityEngine;

namespace ElementsArena.Combat
{
    public class WaterStateManager : BendStateManager
    {
        [SerializeField] float waterSearchDistance = 3;
        [SerializeField] LayerMask waterLayer;

        WaterSource[] nearSources;

        protected override void Update()
        {
            base.Update();
            FindWaterOnNear();
        }

        void FindWaterOnNear()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, waterSearchDistance, waterLayer);

            nearSources = new WaterSource[hits.Length];
            for(int i = 0; i < hits.Length; i++)
            {
                nearSources[i] = hits[i].GetComponent<WaterSource>();
            }
        }


        public bool TakeWater(float amountToTake)
        {
            foreach(WaterSource soruce in nearSources)
            {
                if(soruce.amount >= amountToTake)
                {
                    soruce.TakeWater(amountToTake);
                    return true;
                }
            }
            return false;
        }

        public bool HaveWater(float amount) 
        {
            foreach (WaterSource soruce in nearSources)
            {
                if (soruce.amount >= amount) return true;
            }

            return false;
        }
    }
}