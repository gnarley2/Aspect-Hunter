using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MonsterSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image monsterImage;

    public event Action<MonsterSlot> OnPointerEnterEvent;
    public event Action<MonsterSlot> OnPointerExitEvent;

    private MonsterData _monsterData;

    public MonsterData MonsterData
    {
        get { return _monsterData; }
        set
        {
            _monsterData = value;

            if (_monsterData == null)
            {
                monsterImage.color = new Color(1, 1, 1, 0);
            }
            else
            {
                // Use the icon from the MonsterDetails
                monsterImage.sprite = _monsterData.monsterDetails.icon;
                monsterImage.color = Color.white;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered slot"); // Debug log
        if (OnPointerEnterEvent != null)
            OnPointerEnterEvent(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited slot"); // Debug log
        if (OnPointerExitEvent != null)
            OnPointerExitEvent(this);
    }
}