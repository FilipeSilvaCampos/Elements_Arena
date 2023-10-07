using ElementsArena.Combat;
using TMPro;
using UnityEngine;

public class WaterDisplay : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI display;

    public WaterStateManager stateManager;

    private void Update()
    {
        if (stateManager == null) return;

        display.text = stateManager.totalAmount.ToString();
    }
}
