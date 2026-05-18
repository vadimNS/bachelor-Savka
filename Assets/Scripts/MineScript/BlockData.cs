using UnityEngine;

public enum BlockType { Coal, Stone, Dirt, Iron, Diamond }
public class BlockData
{
    public BlockType Type;
    public Vector2Int Position;
    public int Health;
    public int MaxHealth;

    public BlockData(BlockType type, Vector2Int pos, float healthMultiplier = 1f)
    {
        Type = type;
        Position = pos;
        MaxHealth = Mathf.Max(1, Mathf.CeilToInt(GetInitialHealth(type) * healthMultiplier));
        Health = MaxHealth;
    }

    private int GetInitialHealth(BlockType type)
    {
        return type switch
        {
            BlockType.Dirt => 2,
            BlockType.Stone => 5,
            BlockType.Coal => 25,
            BlockType.Iron => 50,
            BlockType.Diamond => 200,
            _ => 1
        };
    }
    public static int GetBlockPrice(BlockType type)
    {
        return type switch
        {
            BlockType.Dirt => 1,
            BlockType.Stone => 2,
            BlockType.Coal => 4,
            BlockType.Iron => 8,
            BlockType.Diamond => 20,
            _ => 0
        };
    }

}
