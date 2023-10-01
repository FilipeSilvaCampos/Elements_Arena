using System.Collections;
using UnityEngine;

namespace ElementsArena.Combat
{
    public abstract class WaterAbility : Ability
    {
        protected WaterStateManager waterStateManager;

        protected virtual void Start()
        {
            waterStateManager = GetComponent<WaterStateManager>();
        }
    }
}