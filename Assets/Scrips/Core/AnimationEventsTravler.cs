using UnityEngine;
using UnityEngine.Events;

namespace ElementsArena.Core
{
    public class AnimationEventsTravler : MonoBehaviour
    {
        [SerializeField] UnityEvent OnHitEvent;

        //Animation Event
        private void Hit()
        {
            OnHitEvent.Invoke();
        }
    }
}