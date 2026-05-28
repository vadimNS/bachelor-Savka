using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private BlockDiggingController diggingController;

    private string SaveFilePath => Path.Combine(Application.persistentDataPath, "save.json");

    private void Start()
    {

    }

    public void SaveGame()
    {
        if (diggingController == null)
        {
            Debug.LogError("SaveManager: diggingController is not assigned!");
            return;
        }

        SaveData data = new SaveData();

        
        Inventory inventory = diggingController.PlayerEconomy?.Inventory;
        if (inventory != null)
        {
            data.inventorySlots = new List<SlotSaveData>();
            for (int i = 0; i < inventory.SlotCount; i++)
            {
                InventorySlot slot = inventory.GetSlot(i);
                data.inventorySlots.Add(new SlotSaveData
                {
                    blockTypeName = slot.Type?.ToString(),
                    count = slot.Count
                });
            }
        }

        
        data.coins = diggingController.PlayerEconomy?.Coins ?? 0;

        
        data.pickaxeType = diggingController.CurrentPickaxeType.ToString();

        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SaveFilePath, json);
        Debug.Log($"Game saved to {SaveFilePath}");
    }

    public void LoadGame()
    {
        if (!File.Exists(SaveFilePath))
        {
            Debug.Log("No save file found. Starting new game.");
            return;
        }

        string json = File.ReadAllText(SaveFilePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log("Save file loaded.");

        if (diggingController == null)
        {
            Debug.LogError("SaveManager: diggingController is not assigned!");
            return;
        }

        
        Inventory inventory = diggingController.PlayerEconomy?.Inventory;
        if (inventory != null && data.inventorySlots != null)
        {
            inventory.Clear();
            int count = Mathf.Min(data.inventorySlots.Count, inventory.SlotCount);
            for (int i = 0; i < count; i++)
            {
                SlotSaveData savedSlot = data.inventorySlots[i];
                if (!string.IsNullOrEmpty(savedSlot.blockTypeName))
                {
                    if (System.Enum.TryParse(savedSlot.blockTypeName, out BlockType type))
                    {
                        InventorySlot slot = inventory.GetSlot(i);
                        slot.Type = type;
                        slot.Count = savedSlot.count;
                    }
                }
            }
            inventory.OnInventoryChanged?.Invoke();
        }

        
        Wallet wallet = diggingController.PlayerEconomy?.Wallet;
        wallet?.SetCoins(data.coins);

        
        if (!string.IsNullOrEmpty(data.pickaxeType))
        {
            if (System.Enum.TryParse(data.pickaxeType, out PickaxeType type))
            {
                diggingController.LoadPickaxe(type);
            }
        }
    }

    public void OnSaveButtonClick()
    {
        SaveGame();
    }
}