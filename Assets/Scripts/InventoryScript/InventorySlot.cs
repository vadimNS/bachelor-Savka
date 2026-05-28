using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    public BlockType? Type;
    public int Count;

    public bool IsEmpty => Type == null || Count == 0;
    public bool IsFull => Count >= 64;
    public bool CanStack(BlockType type) => Type == type && !IsFull;

    public void Add(BlockType type, int amount)
    {
        if (IsEmpty)
        {
            Type = type;
            Count = amount;
        }
        else if (CanStack(type))
        {
            Count = Mathf.Min(64, Count + amount);
            
        }
    }

    public int SellAll()
    {
        if (IsEmpty) return 0;

        int pricePerItem = BlockData.GetBlockPrice(Type.Value);
        int totalValue = Count * pricePerItem;

        Debug.Log($"Sold {Count} x {Type} for {totalValue} coins.");

        Type = null;
        Count = 0;

        return totalValue;
    }
}


