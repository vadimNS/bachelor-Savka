using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ShopDetailsPanel : MonoBehaviour
{
    [Header("UI елементи")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;      
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;

    private PickaxeData currentData;                  

    
    public System.Action<PickaxeData> OnBuyClicked;

    private void Start()
    {
        
        if (buyButton != null)
            buyButton.onClick.AddListener(HandleBuyClick);
    }

    
    public void Show(PickaxeData data)
    {

        currentData = data;

        
        if (iconImage != null && data.sprite != null)
            iconImage.sprite = data.sprite;

        
        if (nameText != null)
            nameText.text = data.type.ToString();


        if (priceText != null)
            priceText.text = $"Price: {data.price} money\nPower: {data.power}\nSpeed: {1f / data.interval:F1} hits/sec";
    }

    
    public void Hide()
    {
        currentData = null;
    }

    
    private void HandleBuyClick()
    {
        if (currentData != null)
            OnBuyClicked?.Invoke(currentData);
        else
            Debug.LogWarning("Немає вибраної кирки для покупки");
    }
    public void SetBuyInteractable(bool interactable)
    {
        if (buyButton != null)
            buyButton.interactable = interactable;
    }
}