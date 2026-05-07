using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Transform slotsParent; // Де будуть слоти
    [SerializeField] private GameObject slotPrefab; // Prefab слота з ShopSlotUI
    private BlockDiggingController diggingController;
    [SerializeField] private PickaxeDatabase pickaxeDatabase;
    [SerializeField] private ShopDetailsPanel detailsPanel;
    private List<ShopSlotUI> slotUIs = new List<ShopSlotUI>();

    public void Initialize(PickaxeDatabase database)
    {
        pickaxeDatabase = database;
        RefreshShop();
    }
    private void Start()
    {
        if (detailsPanel != null)
            detailsPanel.OnBuyClicked += HandleBuyClicked;
        // Тимчасово – для тесту
        Initialize(pickaxeDatabase);
        diggingController = FindFirstObjectByType<BlockDiggingController>();
    }
    private void RefreshShop()
    {
        // Очистити старі слоти
        foreach (var ui in slotUIs)
            Destroy(ui.gameObject);
        slotUIs.Clear();

        // Створити слоти для кожної кирки
        foreach (var pickaxeData in pickaxeDatabase.pickaxes)
        {
            var go = Instantiate(slotPrefab, slotsParent);
            var slotUI = go.GetComponent<ShopSlotUI>();
            slotUI.Initialize(pickaxeData);
            slotUI.OnSlotClicked += HandleSlotClicked;
            slotUIs.Add(slotUI);
            slotUI.OnSlotClicked += HandleSlotClicked;
        }

    }
    private void HandleSlotClicked(PickaxeData data)
    {
        if (detailsPanel != null)
        {
            detailsPanel.Show(data);

            // Перевіряємо, чи це поточна кирка гравця
            if (diggingController != null)
            {
                // Тобі потрібно отримати поточний тип. Можна додати публічну властивість у BlockDiggingController:
                // public PickaxeType CurrentPickaxeType => currentPickaxe.Type;
                bool isCurrent = diggingController.CurrentPickaxeType == data.type;
                detailsPanel.SetBuyInteractable(!isCurrent);
            }
        }
    }
    private void HandleBuyClicked(PickaxeData data)
    {
        if (diggingController != null)
        {
            // Викликаємо покупку (цей метод вже все перевіряє)
            diggingController.TryBuyPickaxe(data.type);

            // Після покупки ховаємо панель опису (опціонально)
            if (detailsPanel != null)
                detailsPanel.Hide();
        }
        else
        {
            Debug.LogError("DiggingController не призначено в ShopUI!");
        }
    }
}