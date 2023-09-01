using ElementsArena.Combat;
using System.Collections;
using UnityEngine;

namespace ElementsArena.Attributes
{
    public class BreathBar : MonoBehaviour
    {
        [SerializeField] RectTransform foreground;
        [SerializeField] FireBreath breath;
        [SerializeField] float lerpSpeed = 3;

        void Update()
        {
            float nextAmount = Mathf.Lerp(foreground.localScale.x, breath.GetFraction(), lerpSpeed * Time.deltaTime);
            foreground.localScale = new Vector3(nextAmount, 1, 1);
        }
    }
}