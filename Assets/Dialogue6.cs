using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue6 : MonoBehaviour
{
    public static bool Conversation = false;
    public TextMeshProUGUI PNJ6;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI Apprendre;
    public TextMeshProUGUI IntelSup;
    public TextMeshProUGUI IntelInf;
    public TextMeshProUGUI TextFin;
    public TextMeshProUGUI SagesseSup;
    public TextMeshProUGUI SagesseInf;
    public GameObject Panel;
    public static int sagesse1 = 0;
    public static int intelligence1 = 0;
    public string lastAnswer;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJ6.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJ6.GetComponent<TextMeshProUGUI>().enabled = false;
            Apprendre.GetComponent<TextMeshProUGUI>().enabled = false;
            IntelInf.GetComponent<TextMeshProUGUI>().enabled = false;
            IntelSup.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
            SagesseInf.GetComponent<TextMeshProUGUI>().enabled = false;
            SagesseSup.GetComponent<TextMeshProUGUI>().enabled = false;
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
            if (lastAnswer == Constructeur.NameCharacter + ": apprendre")
            {
                if (sagesse1 == 1 && intelligence1 == 1)
                {
                    PNJ6.GetComponent<TextMeshProUGUI>().enabled = false;
                    Apprendre.GetComponent<TextMeshProUGUI>().enabled = false;
                    IntelInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    IntelSup.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
                    SagesseInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseSup.GetComponent<TextMeshProUGUI>().enabled = false;
                }
                else
                {
                    Apprendre.GetComponent<TextMeshProUGUI>().enabled = true;
                    PNJ6.GetComponent<TextMeshProUGUI>().enabled = false;
                    IntelInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    IntelSup.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseSup.GetComponent<TextMeshProUGUI>().enabled = false;
                }
            }
            if (lastAnswer == Constructeur.NameCharacter + ": mana")
            {
                if (intelligence1 == 0)
                {
                    PNJ6.GetComponent<TextMeshProUGUI>().enabled = false;
                    Apprendre.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseSup.GetComponent<TextMeshProUGUI>().enabled = false;
                    if (UI.IntelligenceTotal >= 29)
                    {
                        PlayerInventory.maxMana += 10;
                        IntelSup.GetComponent<TextMeshProUGUI>().enabled = true;
                        intelligence1 = 1;
                        Conversation = false;
                    }
                    else IntelInf.GetComponent<TextMeshProUGUI>().enabled = true;
                    Conversation = false;
                }
            }
            if (lastAnswer == Constructeur.NameCharacter + ": sort")
            {
                if (sagesse1 == 0)
                {
                    PNJ6.GetComponent<TextMeshProUGUI>().enabled = false;
                    Apprendre.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    IntelInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    IntelSup.GetComponent<TextMeshProUGUI>().enabled = false;
                    if (UI.SagesseTotal >= 34)
                    {
                        CharacterMotor.eclair = 1;
                        SagesseSup.GetComponent<TextMeshProUGUI>().enabled = true;
                        sagesse1 = 1;
                        Conversation = false;
                    }
                    else SagesseInf.GetComponent<TextMeshProUGUI>().enabled = true;
                    Conversation = false;
                }
            }
        }
    }
}