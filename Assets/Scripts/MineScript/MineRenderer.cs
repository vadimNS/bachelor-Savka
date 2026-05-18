using UnityEngine;
using UnityEngine.Tilemaps;

public class MineRenderer : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase coalTile;
    [SerializeField] private TileBase stoneTile;
    [SerializeField] private TileBase dirtTile;
    [SerializeField] private TileBase ironTile;
    [SerializeField] private TileBase DiamondTile;

    [SerializeField] private float maxDepthTint = 0.05f;

    public void Render(BlockData[,] mineData)
    {
        tilemap.ClearAllTiles();

        int width = mineData.GetLength(0);
        int height = mineData.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                BlockData block = mineData[x, y];
                TileBase tile = block != null ? GetTileForBlock(block.Type) : null;
                Vector3Int cellPos = new Vector3Int(x, -y, 0);

                tilemap.SetTile(cellPos, tile);
                if (tile != null)
                {
                    float depthRatio = height > 1 ? (float)y / (height - 1) : 0f;
                    float gray = depthRatio * maxDepthTint;
                    Color tint = new Color(1f - gray, 1f - gray, 1f - gray, 1f);
                    tilemap.SetColor(cellPos, tint);
                }
            }
        }

    }

    private TileBase GetTileForBlock(BlockType type)
    {
        return type switch
        {
            BlockType.Coal => coalTile,
            BlockType.Dirt => dirtTile,
            BlockType.Stone => stoneTile,
            BlockType.Iron => ironTile,
            BlockType.Diamond => DiamondTile,
            _ => null,
        };
    }
}
