using ElementsArena.Combat;
using UnityEngine;
using UnityEngine.UI;

public class WaterUIManager : UIManager
{
    [SerializeField] Image stateImage;
    [SerializeField] WaterDisplay waterDisplay;

    WaterStateManager waterStateManager;

    private void Update()
    {
        if (waterStateManager == null) return;
        stateImage.sprite = waterStateManager.GetCurrentState().iconState;
    }

    public override void Initialize(GameObject fighter)
    {
        base.Initialize(fighter);
        waterStateManager = fighter.GetComponent<WaterStateManager>();
        waterDisplay.stateManager = waterStateManager;
    }
}
