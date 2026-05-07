using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;          // компонент Image для спрайту кирки

    private PickaxeData currentData;                   // дані, які слот показує
    public System.Action<PickaxeData> OnSlotClicked;   // подія, яку слухатиме ShopUI

    // Викликається із ShopUI під час створення слота
    public void Initialize(PickaxeData data)
    {
        currentData = data;
        if (iconImage != null && data.sprite != null)
            iconImage.sprite = data.sprite;
    }

    // Автоматичний виклик Unity при кліку на слоті
    public void OnPointerClick(PointerEventData eventData)
    {
        OnSlotClicked?.Invoke(currentData);
    }
}