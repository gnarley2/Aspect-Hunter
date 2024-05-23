using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterTooltip : MonoBehaviour
{
    [SerializeField] Text MonsterNameText;
    [SerializeField] Text TamedText;
    [SerializeField] Text HealthText;
    [SerializeField] Text DamageText;
    [SerializeField] Text AspectText;
    [SerializeField] Text DetailsText;

    public void ShowTooltip(MonsterDetails monsterDetails, int currentHealth)
    {
        Debug.Log("Showing tooltip"); // Debug log
        MonsterNameText.text = monsterDetails.name.ToString();
        TamedText.text = "Tamed: Yes"; // Assume the monster is tamed if it is in the bestiary
        HealthText.text = "Health: " + currentHealth.ToString();
        DamageText.text = "Damage: " + monsterDetails.damage.ToString();
        AspectText.text = "Aspect: " + monsterDetails.type.ToString();
        DetailsText.text = monsterDetails.description;
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        Debug.Log("Hiding tooltip"); // Debug log
        gameObject.SetActive(false);
    }
}