using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Dialogue4 : MonoBehaviour
{
    public static int XpQu�teSpider = 250;
    public static int XpQu�teSpiderQueen = 300;
    public static int XpQu�teJail = 700;
    public static bool Conversation = false;
    public TextMeshProUGUI PNJ4;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI AraigneesF;
    public TextMeshProUGUI AraigneesNF;
    public TextMeshProUGUI Penser;
    public TextMeshProUGUI ReineF;
    public TextMeshProUGUI ReineNF;
    public TextMeshProUGUI Chienvaliers;
    public TextMeshProUGUI Geoles;
    public TextMeshProUGUI GeolesNF;
    public TextMeshProUGUI Idrano;
    public GameObject CountSpider;
    public GameObject CountSpiderQueen;
    public GameObject CountJail;
    public static bool QuestSpiderIsUp = false;
    public static bool QuestSpiderQueenIsUp = false;
    public static bool QuestJailIsUp = false;
    public string lastAnswer;
    public GameObject Panel;
    private Animator AnimCam1;
    private Animator AnimDoorL;
    private Animator AnimDoorR;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJ4.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJ4.GetComponent<TextMeshProUGUI>().enabled = false;
            AraigneesNF.GetComponent<TextMeshProUGUI>().enabled = false;
            AraigneesF.GetComponent<TextMeshProUGUI>().enabled = false;
            Penser.GetComponent<TextMeshProUGUI>().enabled = false;
            ReineF.GetComponent<TextMeshProUGUI>().enabled = false;
            ReineNF.GetComponent<TextMeshProUGUI>().enabled = false;
            Chienvaliers.GetComponent<TextMeshProUGUI>().enabled = false;
            Geoles.GetComponent<TextMeshProUGUI>().enabled = false;
            Idrano.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
            GeolesNF.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        AnimDoorL = GameObject.Find("DoorL").GetComponent<Animator>();
        AnimDoorR = GameObject.Find("DoorR").GetComponent<Animator>();
        AnimCam1 = GameObject.Find("CamCineChien1").GetComponent<Animator>();
    }
    public void PrintSpiderQuest()
    {
        CountSpider.SetActive(true);
        CountSpider.GetComponent<TextMeshProUGUI>().text = "Araignees " + EnemySpider.SpiderQuest + "/10";
        if (XpQu�teSpider == 0)
        {
            CountSpider.SetActive(false);
        }
    }
    public void PrintSpiderQueenQuest()
    {
        CountSpiderQueen.SetActive(true);
        CountSpiderQueen.GetComponent<TextMeshProUGUI>().text = "Reine " + EnemySpiderQueen.SpiderQueenQuest + "/1";
        if (XpQu�teSpiderQueen == 0)
        {
            CountSpiderQueen.SetActive(false);
        }
    }
    public void PrintJailQuest()
    {
        CountJail.SetActive(true);
        CountJail.GetComponent<TextMeshProUGUI>().text = "Prison inspect�e " + Dialogue5.JailVisited + "/1";
        if (XpQu�teJail == 0)
        {
            CountJail.SetActive(false);
        }
    }
    IEnumerator Qu�teValide()
    {
        yield return new WaitForSeconds(1f);
        GameManager.PlayerAnswer = "a";
        lastAnswer = GameManager.PlayerAnswer;
    }
    IEnumerator EndQuestSpider()
    {
        yield return new WaitForSeconds(1f);
        XpQu�teSpider = 0;
    }
    IEnumerator EndQuestSpiderQueen()
    {
        yield return new WaitForSeconds(1f);
        XpQu�teSpiderQueen = 0;
    }
    IEnumerator EndQuestJail()
    {
        yield return new WaitForSeconds(1f);
        XpQu�teJail = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (QuestSpiderIsUp == true)
        {
            PrintSpiderQuest();
        }
        if (QuestSpiderQueenIsUp == true)
        {
            PrintSpiderQueenQuest();
        }
        if (QuestJailIsUp== true)
        {
            PrintJailQuest();
        }
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;
            if (lastAnswer == Constructeur.NameCharacter + ": araign�es")
            {
                if (QuestSpiderIsUp != true)
                {
                    QuestSpiderIsUp = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
                if (EnemySpider.SpiderQuest < 10)
                {
                    PNJ4.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesNF.GetComponent<TextMeshProUGUI>().enabled = true;
                    AraigneesF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Penser.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineF.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Chienvaliers.GetComponent<TextMeshProUGUI>().enabled = false;
                    Geoles.GetComponent<TextMeshProUGUI>().enabled = false;
                    GeolesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Idrano.GetComponent<TextMeshProUGUI>().enabled = false;
                    StartCoroutine(Qu�teValide());
                }
                if (EnemySpider.SpiderQuest >= 10)
                {
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest2Done";
                    PlayerInventory.currentXp += XpQu�teSpider;
                    PNJ4.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesF.GetComponent<TextMeshProUGUI>().enabled = true;
                    Penser.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineF.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Chienvaliers.GetComponent<TextMeshProUGUI>().enabled = false;
                    Geoles.GetComponent<TextMeshProUGUI>().enabled = false;
                    Idrano.GetComponent<TextMeshProUGUI>().enabled = false;
                    GeolesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    StartCoroutine(EndQuestSpider());
                }
            }
            if (lastAnswer == Constructeur.NameCharacter + ": reine")
            {
                if (QuestSpiderQueenIsUp != true)
                {
                    QuestSpiderQueenIsUp = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
                if (EnemySpiderQueen.SpiderQueenQuest < 1)
                {
                    PNJ4.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Penser.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineF.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineNF.GetComponent<TextMeshProUGUI>().enabled = true;
                    Chienvaliers.GetComponent<TextMeshProUGUI>().enabled = false;
                    Geoles.GetComponent<TextMeshProUGUI>().enabled = false;
                    Idrano.GetComponent<TextMeshProUGUI>().enabled = false;
                    GeolesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    StartCoroutine(Qu�teValide());
                }
                if (EnemySpiderQueen.SpiderQueenQuest >= 1)
                {
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest3Done";
                    PlayerInventory.currentXp += XpQu�teSpiderQueen;
                    PNJ4.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Penser.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineF.GetComponent<TextMeshProUGUI>().enabled = true;
                    ReineNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Chienvaliers.GetComponent<TextMeshProUGUI>().enabled = false;
                    Geoles.GetComponent<TextMeshProUGUI>().enabled = false;
                    Idrano.GetComponent<TextMeshProUGUI>().enabled = false;
                    GeolesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    StartCoroutine(EndQuestSpiderQueen());
                }
            }
            if (lastAnswer == Constructeur.NameCharacter + ": penser")
            {
                PNJ4.GetComponent<TextMeshProUGUI>().enabled = false;
                AraigneesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                AraigneesF.GetComponent<TextMeshProUGUI>().enabled = false;
                Penser.GetComponent<TextMeshProUGUI>().enabled = true;
                ReineF.GetComponent<TextMeshProUGUI>().enabled = false;
                ReineNF.GetComponent<TextMeshProUGUI>().enabled = false;
                Chienvaliers.GetComponent<TextMeshProUGUI>().enabled = false;
                Geoles.GetComponent<TextMeshProUGUI>().enabled = false;
                Idrano.GetComponent<TextMeshProUGUI>().enabled = false;
                GeolesNF.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": chienvaliers")
            {
                // cin�matique
                if (AnimCam1 != null)
                {
                    AnimCam1.SetBool("CamChien1Activate", true);
                }     
                AnimDoorL.SetBool("OpenDoorL", true);
                AnimDoorR.SetBool("OpenDoorR", true);
                if (QuestJailIsUp != true)
                {
                    QuestJailIsUp = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
                PNJ4.GetComponent<TextMeshProUGUI>().enabled = false;
                AraigneesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                AraigneesF.GetComponent<TextMeshProUGUI>().enabled = false;
                Penser.GetComponent<TextMeshProUGUI>().enabled = false;
                ReineF.GetComponent<TextMeshProUGUI>().enabled = false;
                ReineNF.GetComponent<TextMeshProUGUI>().enabled = false;
                Chienvaliers.GetComponent<TextMeshProUGUI>().enabled = true;
                Geoles.GetComponent<TextMeshProUGUI>().enabled = false;
                Idrano.GetComponent<TextMeshProUGUI>().enabled = false;
                GeolesNF.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": ge�les")
            {
                if (Dialogue5.QuestJailsIsEnd == true)
                {
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest3Done";
                    PlayerInventory.currentXp += XpQu�teJail;
                    PNJ4.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Penser.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineF.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Chienvaliers.GetComponent<TextMeshProUGUI>().enabled = false;
                    Geoles.GetComponent<TextMeshProUGUI>().enabled = true;
                    Idrano.GetComponent<TextMeshProUGUI>().enabled = false;
                    GeolesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    StartCoroutine(EndQuestJail());
                }
                if (Dialogue5.QuestJailsIsEnd == false)
                {
                    PNJ4.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Penser.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineF.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Chienvaliers.GetComponent<TextMeshProUGUI>().enabled = false;
                    Geoles.GetComponent<TextMeshProUGUI>().enabled = false;
                    Idrano.GetComponent<TextMeshProUGUI>().enabled = false;
                    GeolesNF.GetComponent<TextMeshProUGUI>().enabled = true;
                }
            }
            if (lastAnswer == Constructeur.NameCharacter + ": Idrano")
            {
                if (Dialogue5.QuestJailsIsEnd == true)
                {
                    PNJ4.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    AraigneesF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Penser.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineF.GetComponent<TextMeshProUGUI>().enabled = false;
                    ReineNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Chienvaliers.GetComponent<TextMeshProUGUI>().enabled = false;
                    Geoles.GetComponent<TextMeshProUGUI>().enabled = false;
                    Idrano.GetComponent<TextMeshProUGUI>().enabled = true;
                    GeolesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                }
            }
        }
    }
}