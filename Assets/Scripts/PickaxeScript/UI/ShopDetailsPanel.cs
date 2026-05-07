using UnityEngine;
using UnityEngine.UI;
using TMPro; // якщо використовуєш TextMeshPro, інакше прибери

public class ShopDetailsPanel : MonoBehaviour
{
    [Header("UI елементи")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;      // або просто Text
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;

    private PickaxeData currentData;                  // зберігаємо, яку кирку зараз показуємо

    // Подія, яку слухатиме ShopUI, щоб обробити покупку
    public System.Action<PickaxeData> OnBuyClicked;

    private void Start()
    {
        // Підписуємося на кнопку заздалегідь
        if (buyButton != null)
            buyButton.onClick.AddListener(HandleBuyClick);
    }

    // Викликається, коли вибрали нову кирку в магазині
    public void Show(PickaxeData data)
    {

        currentData = data;

        // Іконка
        if (iconImage != null && data.sprite != null)
            iconImage.sprite = data.sprite;

        // Назва (якщо у PickaxeType є ToString)
        if (nameText != null)
            nameText.text = data.type.ToString();


        if (priceText != null)
            priceText.text = $"Ціна: {data.price} монет\nСила: {data.power}\nШвидкість: {1f / data.interval:F1} уд/сек";
    }

    // Викликається ззовні (наприклад, коли закриваєте вікно або нічого не вибрано)
    public void Hide()
    {
        currentData = null;
    }

    // Внутрішній обробник кнопки
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