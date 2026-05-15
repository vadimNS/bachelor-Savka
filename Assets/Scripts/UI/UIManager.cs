using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Панелі UI")]
    [SerializeField] private GameObject playerInventoryPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject darkBackground;
    [SerializeField] private GameObject selectedPickaxeSlot;

    [Header("Гравець")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float teleportX = -3f;
    [SerializeField] private float teleportY = 0.3f;
    // Зручні властивості для перевірки стану
    public bool IsInventoryOpen => playerInventoryPanel && playerInventoryPanel.activeSelf;
    public bool IsShopOpen => shopPanel && shopPanel.activeSelf;
    public bool IsAnyPanelOpen => IsInventoryOpen || IsShopOpen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // Початковий стан: усе закрито, слот кирки видимий
        if (playerInventoryPanel) playerInventoryPanel.SetActive(false);
        if (shopPanel) shopPanel.SetActive(false);
        if (darkBackground) darkBackground.SetActive(false);
        if (selectedPickaxeSlot) selectedPickaxeSlot.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        // Якщо магазин відкрито, на I закриваємо магазин
        if (IsShopOpen)
        {
            CloseShop();
            return;
        }

        if (playerInventoryPanel == null) return;

        bool open = !playerInventoryPanel.activeSelf;
        playerInventoryPanel.SetActive(open);
        if (darkBackground) darkBackground.SetActive(open);
        if (selectedPickaxeSlot) selectedPickaxeSlot.SetActive(!open);
    }

    public void OpenShop()
    {
        // Закриваємо інвентар гравця, якщо той відкритий
        if (IsInventoryOpen)
            playerInventoryPanel.SetActive(false);

        if (shopPanel) shopPanel.SetActive(true);
        if (darkBackground) darkBackground.SetActive(true);
        if (selectedPickaxeSlot) selectedPickaxeSlot.SetActive(false);
    }

    public void CloseShop()
    {
        if (shopPanel) shopPanel.SetActive(false);
        if (darkBackground) darkBackground.SetActive(false);
        // Слот кирки показуємо тільки якщо інвентар гравця також закритий
        if (selectedPickaxeSlot && !IsInventoryOpen) selectedPickaxeSlot.SetActive(true);
    }


    // ... решта коду (Awake, Start, Update, ToggleInventory, OpenShop, CloseShop) ...

    // Новий метод для кнопки
    public void TeleportToHome()
    {
        if (playerTransform != null)
        {
            playerTransform.position = new Vector3(teleportX, teleportY, 0);
            Debug.Log($"Гравець телепортований на ({teleportX}, {teleportY}, 0)");
        }
        else
        {
            Debug.LogError("UIManager: playerTransform не призначено!");
        }
    }
}