using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
	[SerializeField] Text ItemNameText;
	[SerializeField] Text ItemSlotText;
	[SerializeField] Text ItemStatsText;

	private StringBuilder sb = new StringBuilder();

	public void ShowTooltip(EquippableItem item)
	{
		ItemNameText.text = item.ItemName;
		ItemSlotText.text = item.EquipmentType.ToString();

		sb.Length = 0;
		
		ItemStatsText.text = sb.ToString();

		gameObject.SetActive(true);
	}

	public void HideTooltip()
	{
		gameObject.SetActive(false);
	}
}
