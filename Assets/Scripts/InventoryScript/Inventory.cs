using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public System.Action OnInventoryChanged;
    private List<InventorySlot> slots;
    public int SlotCount => slots.Count;
    public InventorySlot GetSlot(int index) => slots[index];
    public Inventory(int initialSlotCount = 5)
    {
        slots = new List<InventorySlot>();
        for (int i = 0; i < initialSlotCount; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public void AddBlock(BlockType type)
    {
        foreach (var slot in slots)
        {
            if (slot.CanStack(type))
            {
                slot.Add(type, 1);
                Debug.Log($"Block {type} added to existing slot. Total: {slot.Count}");
                OnInventoryChanged?.Invoke();
                return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.Add(type, 1);
                Debug.Log($"Block {type} added to NEW slot. Total: {slot.Count}");
                OnInventoryChanged?.Invoke();
                return;
            }
        }

        Debug.Log($"Inventory FULL! Could not add block of type {type}.");

    }



    public void Clear()
    {
        foreach (var slot in slots)
        {
            slot.Type = null;
            slot.Count = 0;
        }
        OnInventoryChanged?.Invoke();
    }

    public int SellAll()
    {
        int total = 0;
        foreach (var slot in slots)
        {
            total += slot.SellAll();
        }

        Debug.Log($"Total value of sold items: {total} coins.");
        OnInventoryChanged?.Invoke();
        return total;
    }
}
