using System;
using UnityEngine;

namespace ElementsArena.Core
{
    public class AnimationEventTravler : MonoBehaviour
    {
        public event Action OnHitEvent;

        //Animation Event
        private void Hit()
        {
            OnHitEvent.Invoke();
        }
    }
}