using UnityEngine;

namespace ElementsArena.Combat
{
    public class WaterStateManager : BendStateManager
    {
        [SerializeField] float waterSearchDistance = 3;
        [SerializeField] LayerMask waterLayer;

        public WaterSource[] nearSources;
        public float totalAmount { get; private set; }

        protected override void Update()
        {
            base.Update();
            FindWaterOnNear();
        }

        void FindWaterOnNear()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, waterSearchDistance, waterLayer);
            totalAmount = 0;

            nearSources = new WaterSource[hits.Length];
            for(int i = 0; i < hits.Length; i++)
            {
                nearSources[i] = hits[i].GetComponent<WaterSource>();
                totalAmount += nearSources[i].amount;
            }
        }

        #region Public Methods
        public bool TakeWater(float amountToTake)
        {
            foreach(WaterSource source in nearSources)
            {
                if(source.amount >= amountToTake)
                {
                    source.TakeWater(amountToTake);
                    return true;
                }
            }
            return false;
        }

        public bool TakeWater(float amountToTake, out WaterSource sourceToReturn)
        {
            foreach(WaterSource source in nearSources)
            {
                if(source.amount >= amountToTake)
                {
                    source.TakeWater(amountToTake);
                    sourceToReturn = source;
                    return true;
                }
            }
            sourceToReturn = null;
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
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, waterSearchDistance);
        }
    }
}