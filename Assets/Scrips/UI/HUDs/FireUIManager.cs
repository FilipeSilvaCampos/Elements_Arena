using ElementsArena.Attributes;
using ElementsArena.Combat;
using UnityEngine;


public class FireUIManager : UIManager
{
    [SerializeField] BreathBar breathBar;

    public override void Initialize(GameObject fighter)
    {
        base.Initialize(fighter);
        breathBar.characterBreath = fighter.GetComponent<FireBreath>();
    }
}
