using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueDuchelvau : MonoBehaviour
{
    public static int XpQuêteChampion = 600;
    public static bool QuestChampion = false;
    public static bool QuestInterrogation = false;
    public static bool QuestCitizen = false;
    public static bool Conversation = false;
    public TextMeshProUGUI PNJDial;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI Valeur;
    public TextMeshProUGUI DéfiF;
    public TextMeshProUGUI Debusquer;
    public TextMeshProUGUI Cacher;
    public TextMeshProUGUI Maire;
    public TextMeshProUGUI Rapport;
    public TextMeshProUGUI Benediction;
    public TextMeshProUGUI TextFin;

    public GameObject Panel;
    public string lastAnswer;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && XpQuêteChampion !=0)
        {
            if (DialogueMayor.QuestMayor == true)
            {
                Conversation = true;
                Panel.GetComponent<Image>().enabled = true;
                PNJDial.GetComponent<TextMeshProUGUI>().enabled = true;
                PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
        if (other.gameObject.tag == "Player" && XpQuêteChampion == 0)
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            DéfiF.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
            DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
            Debusquer.GetComponent<TextMeshProUGUI>().enabled = false;
            Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
            Maire.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
            Valeur.GetComponent<TextMeshProUGUI>().enabled = false;
            Benediction.GetComponent<TextMeshProUGUI>().enabled = false;
            Rapport.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    IEnumerator EndQuest()
    {
        yield return new WaitForSeconds(1f);
        DialogueMayor.XpQuêteMayor = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;

            if (lastAnswer == Constructeur.NameCharacter + ": valeur")
            {
                PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
                Debusquer.GetComponent<TextMeshProUGUI>().enabled = false;
                Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
                Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                Valeur.GetComponent<TextMeshProUGUI>().enabled = true;
                Benediction.GetComponent<TextMeshProUGUI>().enabled = false;
                Rapport.GetComponent<TextMeshProUGUI>().enabled = false;
                if (QuestChampion != true && XpQuêteChampion != 0)
                {
                    QuestChampion = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
            }
            if ((lastAnswer == Constructeur.NameCharacter + ": débusquer") && (QuestCitizen == false))
            {
                QuestInterrogation = true;
                if (QuestInterrogation == true)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Debusquer.GetComponent<TextMeshProUGUI>().enabled = true;
                    Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
                    Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    Valeur.GetComponent<TextMeshProUGUI>().enabled = false;
                    Benediction.GetComponent<TextMeshProUGUI>().enabled = false;
                    Rapport.GetComponent<TextMeshProUGUI>().enabled = false;
                }
            }
            if ((lastAnswer == Constructeur.NameCharacter + ": cacher") && (QuestCitizen == false))
            {
                if (QuestInterrogation == true)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Debusquer.GetComponent<TextMeshProUGUI>().enabled = false;
                    Cacher.GetComponent<TextMeshProUGUI>().enabled = true;
                    Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    Valeur.GetComponent<TextMeshProUGUI>().enabled = false;
                    Benediction.GetComponent<TextMeshProUGUI>().enabled = false;
                    Rapport.GetComponent<TextMeshProUGUI>().enabled = false;
                }
            }
            if ((lastAnswer == Constructeur.NameCharacter + ": rapport") && (QuestCitizen == false))
            {
                if (QuestInterrogation == true && DialogueMayor.interroge == true)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Debusquer.GetComponent<TextMeshProUGUI>().enabled = false;
                    Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
                    Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    Valeur.GetComponent<TextMeshProUGUI>().enabled = false;
                    Benediction.GetComponent<TextMeshProUGUI>().enabled = false;
                    Rapport.GetComponent<TextMeshProUGUI>().enabled = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "QuestMayorDone";
                    PlayerInventory.currentXp += DialogueMayor.XpQuêteMayor;
                    StartCoroutine(EndQuest());
                }
            }
            if ((lastAnswer == Constructeur.NameCharacter + ": apprendrez") && (DialogueMayor.interroge == true))
            {
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Debusquer.GetComponent<TextMeshProUGUI>().enabled = false;
                    Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
                    Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    Valeur.GetComponent<TextMeshProUGUI>().enabled = false;
                    Benediction.GetComponent<TextMeshProUGUI>().enabled = true;
                    Rapport.GetComponent<TextMeshProUGUI>().enabled = false;
                    CharacterMotor.BuffHp =1;
                }
            }
            if ((lastAnswer == Constructeur.NameCharacter + ": maire") && (QuestInterrogation == false))
            {
                QuestCitizen = true;
                if (QuestCitizen == true)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Debusquer.GetComponent<TextMeshProUGUI>().enabled = false;
                    Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
                    Maire.GetComponent<TextMeshProUGUI>().enabled = true;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    Valeur.GetComponent<TextMeshProUGUI>().enabled = false;
                    Benediction.GetComponent<TextMeshProUGUI>().enabled = false;
                    Rapport.GetComponent<TextMeshProUGUI>().enabled = false;
                }
            }
        }
    }
}

