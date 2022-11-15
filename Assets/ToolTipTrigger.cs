using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    public string content;
    public string header;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.name == "Force")
        {
            content = "Vos attaques de bases infligent " + PlayerInventory.currentDamage + " points de d�gats.";
        }
        if (gameObject.name == "Endurance")
        {
            content = "Vos points de vie max sont � " + PlayerInventory.maxHealth + ".";
        }
        if (gameObject.name == "Intelligence")
        {
            content = "Vos points de mana max sont � " + PlayerInventory.maxMana + ".";
        }
        if (gameObject.name == "Sagesse")
        {
            content = "Votre r�g�naration de mana est � " + PlayerInventory.pointManaIncreasePersec + " points par seconde.";
        }
        if (gameObject.name == "Courage")
        {
            content = "Votre r�g�naration de vie est � " + PlayerInventory.pointHPIncreasePersec + " points par seconde. Votre armure est � " + PlayerInventory.currentArmor + ".";
        }
        if (gameObject.name == "Dext�rit�")
        {
            content = "Votre vitesse d'attaque est � " + 1/CharacterMotor.attackCooldown + " par seconde.";
        }
        ToolTipSystem.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipSystem.Hide();
    }
}
