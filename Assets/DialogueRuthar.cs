using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueRuthar : MonoBehaviour
{
    public static bool Conversation = false;
    public TextMeshProUGUI PNJDial;
    public TextMeshProUGUI TextFin;
    public TextMeshProUGUI SagesseInf;
    public TextMeshProUGUI SagesseSup;
    public TextMeshProUGUI PNJName;
    public GameObject Panel;
    public string lastAnswer;
    private bool buff1 = true;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && buff1 == true)
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            SagesseInf.GetComponent<TextMeshProUGUI>().enabled = false;
            SagesseSup.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if (other.gameObject.tag == "Player" && buff1 == false)
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            SagesseInf.GetComponent<TextMeshProUGUI>().enabled = false;
            SagesseSup.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
            SagesseInf.GetComponent<TextMeshProUGUI>().enabled = false;
            SagesseSup.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
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
                if (buff1 == true && UI.SagesseTotal >= 112)
                {
                    CharacterMotor.BuffDrood = 1;
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseSup.GetComponent<TextMeshProUGUI>().enabled = true;
                    buff1 = false;
                    Conversation = false;
                }
                if (buff1 == true && UI.SagesseTotal < 112)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseInf.GetComponent<TextMeshProUGUI>().enabled = true;
                    SagesseSup.GetComponent<TextMeshProUGUI>().enabled = false;
                }
                if (buff1 == false)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
                    PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseSup.GetComponent<TextMeshProUGUI>().enabled = false;
                }
            }
        }
    }
}
