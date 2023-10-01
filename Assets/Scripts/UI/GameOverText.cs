using TMPro;
using UnityEngine;

namespace ElementsArena.UI
{
    public class GameOverText : MonoBehaviour
    {
        TextMeshProUGUI text;
        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }
    }
}