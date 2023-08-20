using ElementsArena.Damage;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] RectTransform foreground = null;
    [SerializeField] OnTakeDamage characterDamageable;
    [SerializeField] float lerpSpeed = 3;

    private void Update()
    {
        if (characterDamageable == null) return;

        UpdateAmount();
    }

    private void UpdateAmount()
    {
        float nextAmount = Mathf.Lerp(foreground.localScale.x, characterDamageable.GetFraction(), lerpSpeed * Time.deltaTime);
        foreground.localScale = new Vector3(nextAmount, 1, 1);
    }
}
