using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue1 : MonoBehaviour
{
    public static bool Conversation = false;
    public TextMeshProUGUI PNJ1;
    public TextMeshProUGUI PNJTuto;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI Apprendre;
    public TextMeshProUGUI ForceSup;
    public TextMeshProUGUI ForceInf;
    public TextMeshProUGUI TextFin;
    public GameObject Panel;
    public string lastAnswer;
    public static float force1 = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJ1.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJ1.GetComponent<TextMeshProUGUI>().enabled = false;
            Apprendre.GetComponent<TextMeshProUGUI>().enabled = false;
            ForceInf.GetComponent<TextMeshProUGUI>().enabled = false;
            ForceSup.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJTuto.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;
            if (lastAnswer == Constructeur.NameCharacter + ": mots-clés")
            {
                PNJ1.GetComponent<TextMeshProUGUI>().enabled = false;
                PNJTuto.GetComponent<TextMeshProUGUI>().enabled = true;
                Apprendre.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": apprendre")
            {
                PNJ1.GetComponent<TextMeshProUGUI>().enabled = false;
                PNJTuto.GetComponent<TextMeshProUGUI>().enabled = false;
                Apprendre.GetComponent<TextMeshProUGUI>().enabled = true;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": prêt")
            {
                if (force1 == 0)
                {
                    PNJ1.GetComponent<TextMeshProUGUI>().enabled = false;
                    Apprendre.GetComponent<TextMeshProUGUI>().enabled = false;
                    PNJTuto.GetComponent<TextMeshProUGUI>().enabled = false;
                    if (UI.ForceTotal >= 30)
                    {
                        PlayerInventory.currentDamage += 5;
                        ForceSup.GetComponent<TextMeshProUGUI>().enabled = true;
                        Debug.Log("les dégats sont à " + PlayerInventory.currentDamage);
                        force1 = 1;
                        Conversation = false;
                    }
                    else ForceInf.GetComponent<TextMeshProUGUI>().enabled = true;
                    Conversation = false;
                }
                else
                {
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
                    PNJ1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    Apprendre.GetComponent<TextMeshProUGUI>().enabled = false;
                    PNJTuto.GetComponent<TextMeshProUGUI>().enabled = false;
                }
            }
        }

    }
}
