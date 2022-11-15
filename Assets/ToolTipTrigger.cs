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
            content = "Vos attaques de bases infligent " + PlayerInventory.currentDamage + " points de dégats.";
        }
        if (gameObject.name == "Endurance")
        {
            content = "Vos points de vie max sont à " + PlayerInventory.maxHealth + ".";
        }
        if (gameObject.name == "Intelligence")
        {
            content = "Vos points de mana max sont à " + PlayerInventory.maxMana + ".";
        }
        if (gameObject.name == "Sagesse")
        {
            content = "Votre régénaration de mana est à " + PlayerInventory.pointManaIncreasePersec + " points par seconde.";
        }
        if (gameObject.name == "Courage")
        {
            content = "Votre régénaration de vie est à " + PlayerInventory.pointHPIncreasePersec + " points par seconde. Votre armure est à " + PlayerInventory.currentArmor + ".";
        }
        if (gameObject.name == "Dextérité")
        {
            content = "Votre vitesse d'attaque est à " + 1/CharacterMotor.attackCooldown + " par seconde.";
        }
        ToolTipSystem.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipSystem.Hide();
    }
}
