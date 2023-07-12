using ElementsArena.Damage;
using TMPro;
using UnityEngine;

public class AttributesDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthDisplay;

    IDamageable damageable;
    public bool alive { get; private set; }

    void Update()
    {
        if (!alive) return;

        healthDisplay.text = string.Format("Life: {0:0}", damageable.life);
    }

    public void SetUpAttributes(IDamageable damageable)
    {
        alive = true;

        this.damageable = damageable;
    }
}
