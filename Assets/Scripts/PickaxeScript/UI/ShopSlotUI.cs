using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;          

    private PickaxeData currentData;                   
    public System.Action<PickaxeData> OnSlotClicked;   

    
    public void Initialize(PickaxeData data)
    {
        currentData = data;
        if (iconImage != null && data.sprite != null)
            iconImage.sprite = data.sprite;
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnSlotClicked?.Invoke(currentData);
    }
}