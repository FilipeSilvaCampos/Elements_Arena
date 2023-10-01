using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ElementsArena.UI
{
    public class TextFadeEffect : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textToFade;
        [SerializeField] float fadeTime = 0.1f;

        private void Start()
        {
            FadeIn();
        }

        void FadeIn()
        {
            textToFade.DOFade(1, fadeTime).OnComplete(() => FadeOut());
        }

        void FadeOut()
        {
            textToFade.DOFade(0, fadeTime).OnComplete(() => FadeIn());
        }
    }
}