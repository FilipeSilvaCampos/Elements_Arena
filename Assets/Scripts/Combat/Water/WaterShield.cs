using ElementsArena.Combat;
using ElementsArena.Damage;
using UnityEngine;

public class WaterShield : WaterAbility
{
    public GameObject shieldPrefab;
    public float waterNeded = 50;

    protected override void OnReady()
    {
       if(called && waterStateManager.TakeWater(waterNeded))
       {
            Shield shield = Instantiate(shieldPrefab, gameObject.transform).GetComponent<Shield>();
            shield.SetInstigator(GetComponent<IDamageable>());

            Destroy(shield.gameObject, activeTime);
       }
    }

    protected override void OnActive()
    {
       if(IsTimeToChangeState()) FinishState();
    }

    protected override void OnCooldown()
    {
        if(IsTimeToChangeState() ) FinishState();
    }
}
