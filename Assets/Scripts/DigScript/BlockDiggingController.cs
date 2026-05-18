using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockDiggingController : MonoBehaviour
{
    private PlayerEconomy playerEconomy;
    public PlayerEconomy PlayerEconomy => playerEconomy;
    [SerializeField] private Tilemap tilemap;
    public PickaxeType CurrentPickaxeType => currentPickaxe.Type;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform digPoint; // Точка взаємодії (наприклад, руки)

    [SerializeField] private float breakDistance = 1.5f; // Максимальна дистанція для руйнування

    [SerializeField] private BreakingEffectController breakingEffect;

    [SerializeField] private int slotCount = 5;

    [SerializeField] private PickaxeDatabase pickaxeDatabase; // Database кирок
    [SerializeField] private SelectedPickaxeSlotUI selectedPickaxeSlotUI; // Відображення поточної кирки

    private Inventory playerInventory;
    private Pickaxe currentPickaxe;
    private Dictionary<PickaxeType, Pickaxe> pickaxes = new();

    private float damageTimer = 0f;
    private BlockData[,] mineData;

    public void SetMineData(BlockData[,] data)
    {
        mineData = data;
    }

    private void Awake()
    {
        playerInventory = new Inventory(slotCount); // Передаємо slotCount
        Wallet wallet = new Wallet();
        var coinsUI = FindAnyObjectByType<CoinsUI>();
        if (coinsUI != null)
            coinsUI.Initialize(wallet);
        playerEconomy = new PlayerEconomy(playerInventory, wallet);

        var inventoryUI = FindAnyObjectByType<InventoryUI>();
        if (inventoryUI != null)
            inventoryUI.Initialize(playerInventory, wallet);

        InitializePickaxes();
        SetPickaxe(PickaxeType.Wooden); // Стартова
    }
    private void InitializePickaxes()
    {
        foreach (PickaxeType type in System.Enum.GetValues(typeof(PickaxeType)))
        {
            PickaxeData data = pickaxeDatabase.GetPickaxeData(type);
            if (data != null)
                pickaxes[type] = new Pickaxe(data);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= currentPickaxe.Interval)
            {
                TryDamageBlockUnderCursor();
                damageTimer = 0f;
            }
        }
        else
        {
            damageTimer = 0f;
        }
    }


    private void TryDamageBlockUnderCursor()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);
        int x = cellPos.x;
        int y = -cellPos.y;

        if (mineData == null) return;
        if (x < 0 || y < 0 || x >= mineData.GetLength(0) || y >= mineData.GetLength(1)) return;

        // Прямокутна перевірка відстані
        Vector3 blockWorldCenter = tilemap.GetCellCenterWorld(cellPos);
        Vector2 delta = blockWorldCenter - digPoint.position;

        float maxX = breakDistance;
        float maxY = breakDistance * 2f;

        float normalizedX = delta.x / maxX;
        float normalizedY = delta.y / maxY;

        if ((normalizedX * normalizedX + normalizedY * normalizedY) > 1f)
        {
            Debug.Log("Block is too far to dig (elliptical check).");
            return;
        }

        BlockData block = mineData[x, y];
        if (block == null) return;

        block.Health -= Mathf.CeilToInt(currentPickaxe.Power);

        Debug.Log($"Block {block.Type} at {cellPos} damaged. Health: {block.Health}");

        float progress = 1f - (float)block.Health / block.MaxHealth;
        breakingEffect.Show(blockWorldCenter); // Телепортуємо ефект
        breakingEffect.SetProgress(progress);  // Ставимо кадр

        if (block.Health <= 0)
        {
            tilemap.SetTile(cellPos, null);
            mineData[x, y] = null;

            playerInventory.AddBlock(block.Type);


            breakingEffect.Hide();
        }
    }
    public enum PurchaseResult
    {
        Success,
        AlreadyOwned,
        NotEnoughMoney
    }

    public PurchaseResult TryBuyPickaxe(PickaxeType type)
    {
        Pickaxe newPick = pickaxes[type];
        if (currentPickaxe.Type == type)
        {
            Debug.Log($"You already own and use the {type} pickaxe.");
            return PurchaseResult.AlreadyOwned;
        }

        if (playerEconomy.TryBuyPickaxe(newPick))  // новий метод в PlayerEconomy
        {
            SetPickaxe(type);
            Debug.Log($"Bought and equipped {type} pickaxe.");
            return PurchaseResult.Success;
        }
        else
        {
            Debug.Log($"Not enough coins to buy {type} pickaxe. Need: {newPick.Price}, Have: {playerEconomy.Coins}");
            return PurchaseResult.NotEnoughMoney;
        }
    }
    public void LoadPickaxe(PickaxeType type)
    {
        SetPickaxe(type);
    }

    private void SetPickaxe(PickaxeType type)
    {
        currentPickaxe = pickaxes[type];

        if (selectedPickaxeSlotUI != null)
        {
            PickaxeData data = pickaxeDatabase.GetPickaxeData(type);
            selectedPickaxeSlotUI.SetPickaxe(data);
        }
    }
}
