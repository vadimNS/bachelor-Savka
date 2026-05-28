using UnityEngine;
public interface IMineGenerator
{
    BlockData[,] Generate();
}
public class BasicMineGenerator : IMineGenerator
{
    private readonly MineGenerationSettings settings;

    public BasicMineGenerator(MineGenerationSettings settings)
    {
        this.settings = settings;
    }

    public BlockData[,] Generate()
    {
        var blocks = new BlockData[settings.width, settings.height];
        var caveMap = new bool[settings.width, settings.height];

        for (int y = 0; y < settings.height; y++)
        {
            for (int x = 0; x < settings.width; x++)
            {
                caveMap[x, y] = ShouldCreateCave(x, y, caveMap);
            }
        }

        for (int x = 0; x < settings.width; x++)
        {
            for (int y = 0; y < settings.height; y++)
            {
                if (caveMap[x, y])
                {
                    blocks[x, y] = null;
                    continue;
                }

                BlockType type;

                
                if (y < 2)
                {
                    type = BlockType.Dirt;
                }
                else
                {
                    type = settings.GetRandomBlockType();
                }

                float depthMultiplier = GetDepthMultiplier(y);
                blocks[x, y] = new BlockData(type, new Vector2Int(x, y), depthMultiplier);
            }
        }

        return blocks;
    }

    private bool ShouldCreateCave(int x, int y, bool[,] caveMap)
    {
        if (y < 50) return false;

        float baseChance = settings.caveChance;
        int adjacentCaves = 0;

        if (x > 0 && caveMap[x - 1, y]) adjacentCaves++;
        if (y > 0 && caveMap[x, y - 1]) adjacentCaves++;

        float localChance = baseChance + adjacentCaves * settings.caveSpread;
        return Random.value < localChance;
    }

    private float GetDepthMultiplier(int depth)
    {
        int layerIndex = depth / settings.layerHeight;
        return Mathf.Pow(2f, layerIndex);
    }
}
