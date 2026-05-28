using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform slotsParent; 
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private TMP_Text coinsText;

    private Inventory inventory;
    private Wallet wallet;
    private List<InventorySlotUI> slotUIs = new List<InventorySlotUI>();

    public void Initialize(Inventory inventory, Wallet wallet)
    {
        this.inventory = inventory;
        this.wallet = wallet;

        inventory.OnInventoryChanged += RefreshInventory;
        wallet.OnCoinsChanged += RefreshCoins;

        RefreshInventory();
        RefreshCoins(wallet.Coins);
    }

    private void RefreshInventory()
    {
        
        foreach (var ui in slotUIs)
            Destroy(ui.gameObject);
        slotUIs.Clear();

        
        for (int i = 0; i < inventory.SlotCount; i++)
        {
            var go = Instantiate(slotPrefab, slotsParent);
            var slotUI = go.GetComponent<InventorySlotUI>();
            slotUI.Initialize(inventory.GetSlot(i));
            slotUIs.Add(slotUI);
        }
    }

    private void RefreshCoins(int newCoins)
    {
        coinsText.text = newCoins.ToString();
    }

    private void OnDestroy()
    {
        if (inventory != null) inventory.OnInventoryChanged -= RefreshInventory;
        if (wallet != null) wallet.OnCoinsChanged -= RefreshCoins;
    }
}