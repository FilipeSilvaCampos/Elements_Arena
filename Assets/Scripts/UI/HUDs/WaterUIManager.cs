using ElementsArena.Combat;
using UnityEngine;
using UnityEngine.UI;

public class WaterUIManager : UIManager
{
    [SerializeField] Image stateImage;

    WaterStateManager waterStateManager;

    private void Update()
    {
        stateImage.sprite = waterStateManager.GetCurrentState().iconState;
    }

    public override void Initialize(GameObject fighter)
    {
        base.Initialize(fighter);
        waterStateManager = fighter.GetComponent<WaterStateManager>();
    }
}
