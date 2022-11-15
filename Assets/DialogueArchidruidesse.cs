using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueArchidruidesse : MonoBehaviour
{
    public static bool Conversation = false;
    public TextMeshProUGUI PNJDial;
    public TextMeshProUGUI Maire;
    public TextMeshProUGUI Benediction;
    public TextMeshProUGUI PNJName;
    public GameObject Panel;
    public string lastAnswer;
    private bool buff1 = true;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            Benediction.GetComponent<TextMeshProUGUI>().enabled = false;
            Maire.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
            Benediction.GetComponent<TextMeshProUGUI>().enabled = false;
            Maire.GetComponent<TextMeshProUGUI>().enabled = false;
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

            if ((lastAnswer == (Constructeur.NameCharacter + ": oui")) || (lastAnswer == (Constructeur.NameCharacter + ": apprendre")))
            {
                if (buff1 == true && DialogueMayor.XpQuêteMayor == 0)
                {

                    CharacterMotor.BuffMana = 1;
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
                    Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                    Benediction.GetComponent<TextMeshProUGUI>().enabled = true;
                    buff1 = false;
                    Conversation = false;
                }
            }
            if (lastAnswer == Constructeur.NameCharacter + ": maire")
            {
                if (buff1 == true && DialogueMayor.XpQuêteMayor == 0)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
                    Benediction.GetComponent<TextMeshProUGUI>().enabled = false;
                    Maire.GetComponent<TextMeshProUGUI>().enabled = true;
                }
            }
        }
    }
}
