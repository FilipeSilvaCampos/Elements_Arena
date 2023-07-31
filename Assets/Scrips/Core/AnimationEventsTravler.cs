using System;
using UnityEngine;
using UnityEngine.Events;

namespace ElementsArena.Core
{
    public class AnimationEventsTravler : MonoBehaviour
    {
        public event Action OnHitEvent;

        //Animation Event
        private void Hit()
        {
            OnHitEvent.Invoke();
        }
    }
}