using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Панелі UI Shop")]
    [SerializeField] private GameObject playerInventoryPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject darkBackground;
    [SerializeField] private GameObject selectedPickaxeSlot;

    [Header("Панелі UI ECS")]
    [SerializeField] private GameObject escPanel;

    [SerializeField] private GameObject SubmitPanel;

    [SerializeField] private GameObject EnoughMoneyPanel;
    [Header("Гравець")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float teleportX = -3f;
    [SerializeField] private float teleportY = 0.3f;
    
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
        
        if (escPanel) escPanel.SetActive(false);
        if (playerInventoryPanel) playerInventoryPanel.SetActive(false);
        if (shopPanel) shopPanel.SetActive(false);
        if (darkBackground) darkBackground.SetActive(false);
        if (selectedPickaxeSlot) selectedPickaxeSlot.SetActive(true);
        if (SubmitPanel) SubmitPanel.SetActive(false);
        if (EnoughMoneyPanel) EnoughMoneyPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escPanel.SetActive(true);
        }
    }

    public void ToggleInventory()
    {
        
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
        
        if (selectedPickaxeSlot && !IsInventoryOpen) selectedPickaxeSlot.SetActive(true);
    }


    

    
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
    public void ContinueButton()
    {
        if (escPanel) escPanel.SetActive(false);
    }
    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }
    public void BuyButton()
    {
        if (SubmitPanel) SubmitPanel.SetActive(true);
    }

    public void NoButton()
    {
        if (SubmitPanel) SubmitPanel.SetActive(false);
    }

    public void ShowEnoughMoneyPanel()
    {
        if (EnoughMoneyPanel) EnoughMoneyPanel.SetActive(true);
    }

    public void HideEnoughMoneyPanel()
    {
        if (EnoughMoneyPanel) EnoughMoneyPanel.SetActive(false);
    }

    public void OkButton()
    {
        if (SubmitPanel) EnoughMoneyPanel.SetActive(false);
    }
}