using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
    [SerializeField] Text descriptionText;
    [SerializeField] Text itemName;

    public void SetDescription(string description, string name)
    {
        descriptionText.text = description;
        itemName.text = name;
    }
}
