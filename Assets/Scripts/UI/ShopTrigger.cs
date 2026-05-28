using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (UIManager.Instance == null) return;

        
        if (UIManager.Instance.IsInventoryOpen)
            return;

        
        if (UIManager.Instance.IsShopOpen)
            UIManager.Instance.CloseShop();
        else
            UIManager.Instance.OpenShop();
    }
}