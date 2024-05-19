using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemSlotText;
    [SerializeField] Text ItemDescriptionText;
    [SerializeField] Text AmmoCountText;

    public void ShowTooltip(EquippableItem item)
    {
        ItemNameText.text = item.ItemName;
        ItemSlotText.text = item.EquipmentType.ToString();
        ItemDescriptionText.text = item.Description;
        gameObject.SetActive(true);

        if (item.EquipmentType == EquipmentType.Aspect)
        {
            int ammoCount = AspectInventory.Instance.GetAspectCount(item.aspectType);
            AmmoCountText.text = "Ammo: " + ammoCount;
            AmmoCountText.gameObject.SetActive(true);
        }
        else
        {
            AmmoCountText.gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}