using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private BlockSprites blockSprites; 
    private InventorySlot slot;

    public void Initialize(InventorySlot slot)
    {
        this.slot = slot;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (slot.IsEmpty)
        {
            icon.gameObject.SetActive(false);
            countText.text = "";
        }
        else
        {
            icon.gameObject.SetActive(true);
            
            icon.sprite = blockSprites.GetSprite(slot.Type.Value);
            countText.text = slot.Count.ToString();
        }
    }
}