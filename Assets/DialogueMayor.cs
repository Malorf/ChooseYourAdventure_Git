using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueMayor : MonoBehaviour
{
    public static int XpQuêteMayor = 750;
    public static bool QuestMayor = false;
    public static bool Conversation = false;
    public static bool interroge = false;
    public TextMeshProUGUI PNJDial;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI Methodes;
    public TextMeshProUGUI Campagne;
    public TextMeshProUGUI Cacher;
    public TextMeshProUGUI Maire;
    public TextMeshProUGUI Druide;
    public TextMeshProUGUI CourageSup1;
    public TextMeshProUGUI CourageInf1;
    public TextMeshProUGUI CourageSup2;
    public TextMeshProUGUI CourageInf2;
    public TextMeshProUGUI TextFin;

    public GameObject Panel;
    public string lastAnswer;
    public static int courage3 = 0;
    public static int courage4 = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if ((other.gameObject.tag == "Player") && (DialogueDuchelvau.QuestInterrogation == true))
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            Cacher.GetComponent<TextMeshProUGUI>().enabled = true;
            interroge = true;
        }
        if ((other.gameObject.tag == "Player") && (DialogueDuchelvau.QuestCitizen == true))
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
            Maire.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
            CourageInf1.GetComponent<TextMeshProUGUI>().enabled = false;
            CourageSup1.GetComponent<TextMeshProUGUI>().enabled = false;
            CourageInf2.GetComponent<TextMeshProUGUI>().enabled = false;
            CourageSup2.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
            Methodes.GetComponent<TextMeshProUGUI>().enabled = false;
            Campagne.GetComponent<TextMeshProUGUI>().enabled = false;
            Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
            Maire.GetComponent<TextMeshProUGUI>().enabled = false;
            Druide.GetComponent<TextMeshProUGUI>().enabled = false;
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
        XpQuêteMayor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;

            if (lastAnswer == Constructeur.NameCharacter + ": méthodes")
            {
                PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                CourageInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                CourageSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                CourageInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                CourageSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                Methodes.GetComponent<TextMeshProUGUI>().enabled = true;
                Campagne.GetComponent<TextMeshProUGUI>().enabled = false;
                Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
                Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                Druide.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": campagne")
            {
                CourageInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                CourageSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                CourageInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                CourageSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                Methodes.GetComponent<TextMeshProUGUI>().enabled = false;
                Campagne.GetComponent<TextMeshProUGUI>().enabled = true;
                Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
                Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                Druide.GetComponent<TextMeshProUGUI>().enabled = false;
                PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                if (QuestMayor != true)
                {
                    QuestMayor = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
            }
            if (lastAnswer == Constructeur.NameCharacter + ": druides")
            {
                PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                CourageInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                CourageSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                CourageInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                CourageSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                Methodes.GetComponent<TextMeshProUGUI>().enabled = false;
                Campagne.GetComponent<TextMeshProUGUI>().enabled = false;
                Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
                Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                Druide.GetComponent<TextMeshProUGUI>().enabled = true;
                GameManager.messageList.Clear();
                GameManager.PlayerAnswer = "QuestMayorDone";
                PlayerInventory.currentXp += XpQuêteMayor;
                StartCoroutine(EndQuest());
            }
            if (lastAnswer == (Constructeur.NameCharacter + ": apprendre"))
            {
                if (courage4 == 0)
                {
                    if (courage3 == 0)
                    {
                        PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                        TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                        Methodes.GetComponent<TextMeshProUGUI>().enabled = false;
                        Campagne.GetComponent<TextMeshProUGUI>().enabled = false;
                        Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
                        Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                        Druide.GetComponent<TextMeshProUGUI>().enabled = false;
                        if (UI.CourageTotal >= 66)
                        {
                            PlayerInventory.currentArmor += 10;
                            CourageSup1.GetComponent<TextMeshProUGUI>().enabled = true;
                            courage3 = 1;
                            Conversation = false;
                        }
                        else CourageInf1.GetComponent<TextMeshProUGUI>().enabled = true;
                        Conversation = false;
                    }
                    else
                    {
                        PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                        TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                        Methodes.GetComponent<TextMeshProUGUI>().enabled = false;
                        Campagne.GetComponent<TextMeshProUGUI>().enabled = false;
                        Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
                        Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                        Druide.GetComponent<TextMeshProUGUI>().enabled = false;
                        if (UI.CourageTotal >= 82)
                        {
                            PlayerInventory.currentArmor += 10;
                            CourageSup2.GetComponent<TextMeshProUGUI>().enabled = true;
                            courage4 = 1;
                            Conversation = false;
                        }
                        else CourageInf2.GetComponent<TextMeshProUGUI>().enabled = true;
                        Conversation = false;
                    }
                }
                else
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
                    Methodes.GetComponent<TextMeshProUGUI>().enabled = false;
                    Campagne.GetComponent<TextMeshProUGUI>().enabled = false;
                    Cacher.GetComponent<TextMeshProUGUI>().enabled = false;
                    Maire.GetComponent<TextMeshProUGUI>().enabled = false;
                    Druide.GetComponent<TextMeshProUGUI>().enabled = false;
                    Conversation = false;
                }
            }
        }
    }
}
