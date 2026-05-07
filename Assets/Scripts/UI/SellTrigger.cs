using UnityEngine;

public class SellTrigger : MonoBehaviour
{
    [SerializeField] private BlockDiggingController diggingController;

    private void OnMouseDown()
    {
        if (diggingController != null && diggingController.PlayerEconomy != null)
            diggingController.PlayerEconomy.SellAllInventory();
        else
            Debug.LogWarning("SellTrigger: DiggingController or PlayerEconomy missing!");
    }
}