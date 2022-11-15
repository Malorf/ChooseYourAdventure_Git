using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueLadyRuid : MonoBehaviour
{
    public static bool Conversation = false;
    public TextMeshProUGUI PNJDial;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI IntelSup1;
    public TextMeshProUGUI IntelInf1;
    public TextMeshProUGUI IntelSup2;
    public TextMeshProUGUI IntelInf2;
    public TextMeshProUGUI TextFin;

    public GameObject Panel;
    public string lastAnswer;
    public static int intelligence2 = 0;
    public static int intelligence3 = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
            IntelInf1.GetComponent<TextMeshProUGUI>().enabled = false;
            IntelSup1.GetComponent<TextMeshProUGUI>().enabled = false;
            IntelInf2.GetComponent<TextMeshProUGUI>().enabled = false;
            IntelSup2.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
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
            if ((lastAnswer == (Constructeur.NameCharacter + ": oui")) || (lastAnswer == (Constructeur.NameCharacter + ": apprendre")))
            {
                if (intelligence3 == 0)
                {
                    if (intelligence2 == 0)
                    {
                        PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                        TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                        if (UI.IntelligenceTotal >= 57)
                        {
                            PlayerInventory.maxMana += 20;
                            IntelSup1.GetComponent<TextMeshProUGUI>().enabled = true;
                            intelligence2 = 0;
                            Conversation = false;
                        }
                        else IntelInf1.GetComponent<TextMeshProUGUI>().enabled = true;
                        Conversation = false;
                    }
                    else
                    {
                        PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                        TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                        if (UI.IntelligenceTotal >= 98)
                        {
                            PlayerInventory.maxMana += 30;
                            IntelSup2.GetComponent<TextMeshProUGUI>().enabled = true;
                            intelligence3 = 0;
                            Conversation = false;
                        }
                        else IntelInf2.GetComponent<TextMeshProUGUI>().enabled = true;
                        Conversation = false;
                    }
                }
                else
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
                    Conversation = false;
                }
                
            }
        }
    }
}
