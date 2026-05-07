using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (UIManager.Instance == null) return;

        // Якщо відкрито інвентар гравця – не даємо відкрити магазин
        if (UIManager.Instance.IsInventoryOpen)
            return;

        // Якщо магазин уже відкрито – закриваємо його, інакше відкриваємо
        if (UIManager.Instance.IsShopOpen)
            UIManager.Instance.CloseShop();
        else
            UIManager.Instance.OpenShop();
    }
}