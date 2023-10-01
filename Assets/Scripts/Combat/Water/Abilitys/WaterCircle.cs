using System.Collections;
using UnityEngine;

namespace ElementsArena.Combat
{
    public class WaterCircle : Ability
    {
        [SerializeField] GameObject circlePrefab;

        WaterSource circleSource;
        protected override void OnReady()
        {
            if(called)
            {
                circleSource = Instantiate(circlePrefab, transform).GetComponent<WaterSource>();
                Destroy(circleSource.gameObject, activeTime);
                FinishState();
            }
        }

        protected override void OnActive()
        {
            if(IsTimeToChangeState() || circleSource == null) FinishState();

            if (Mathf.Approximately(circleSource.amount, 0))
            {
                Destroy(circleSource.gameObject);
                FinishState();
            }
        }

        protected override void OnCooldown()
        {
            if (IsTimeToChangeState()) FinishState(); ;
        }
    }
}