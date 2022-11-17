using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueMerlinramix : MonoBehaviour
{
    public static int XpQuêteHerbes = 300;
    public static int XpQuêtePotion = 850;
    public static bool QuestHerbes = false;
    public static bool QuestPotion = false;
    public static bool Conversation = false;
    public TextMeshProUGUI PNJDial;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI HerbesNF;
    public TextMeshProUGUI HerbesF;
    public TextMeshProUGUI ChancreNF;
    public TextMeshProUGUI ChancreF;
    public TextMeshProUGUI SagesseSup1;
    public TextMeshProUGUI SagesseInf1;
    public TextMeshProUGUI TextFin;

    public GameObject CountHerbes;
    public GameObject CountChancres;
    public GameObject Panel;
    public string lastAnswer;
    private bool sagesse1 = true;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            SagesseInf1.GetComponent<TextMeshProUGUI>().enabled = false;
            SagesseSup1.GetComponent<TextMeshProUGUI>().enabled = false;
            HerbesNF.GetComponent<TextMeshProUGUI>().enabled = false;
            HerbesF.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
            ChancreNF.GetComponent<TextMeshProUGUI>().enabled = false;
            ChancreF.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
            SagesseInf1.GetComponent<TextMeshProUGUI>().enabled = false;
            SagesseSup1.GetComponent<TextMeshProUGUI>().enabled = false;
            HerbesNF.GetComponent<TextMeshProUGUI>().enabled = false;
            HerbesF.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
            ChancreNF.GetComponent<TextMeshProUGUI>().enabled = false;
            ChancreF.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void HerbesQuest()
    {
        CountHerbes.SetActive(true);
        CountHerbes.GetComponent<TextMeshProUGUI>().text = "Herbes " + Flowers.NumberFlowers + "/12";
        if (XpQuêteHerbes == 0)
        {
            CountHerbes.SetActive(false);
        }
    }
    public void ChancresQuest()
    {
        CountChancres.SetActive(true);
        CountChancres.GetComponent<TextMeshProUGUI>().text = "Chancres " + EnemyGreenSpider.NumberGreenSpiders + "/11";
        if (XpQuêtePotion == 0)
        {
            CountChancres.SetActive(false);
        }
    }
    IEnumerator EndQuestHerbe()
    {
        yield return new WaitForSeconds(1f);
        XpQuêteHerbes = 0;
    }
    IEnumerator EndQuestChancre()
    {
        yield return new WaitForSeconds(1f);
        XpQuêtePotion = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestHerbes==true)
        {
            HerbesQuest();
        }
        if (QuestPotion == true)
        {
            ChancresQuest();
        }
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;

            if (lastAnswer == Constructeur.NameCharacter + ": herbes")
            {
                if (QuestHerbes != true)
                {
                    QuestHerbes = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
                if (Flowers.NumberFlowers < 12)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    HerbesNF.GetComponent<TextMeshProUGUI>().enabled = true;
                    HerbesF.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    ChancreNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    ChancreF.GetComponent<TextMeshProUGUI>().enabled = false;
                }
                if (Flowers.NumberFlowers >= 12)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    HerbesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    HerbesF.GetComponent<TextMeshProUGUI>().enabled = true;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    ChancreNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    ChancreF.GetComponent<TextMeshProUGUI>().enabled = false;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "QuestFlowersDone";
                    PlayerInventory.currentXp += XpQuêteHerbes;
                    StartCoroutine(EndQuestHerbe());
                }
            }
            if (lastAnswer == Constructeur.NameCharacter + ": chancres")
            {
                if (QuestPotion != true)
                {
                    QuestPotion = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
                if (EnemyGreenSpider.NumberGreenSpiders < 11)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    HerbesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    HerbesF.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    ChancreNF.GetComponent<TextMeshProUGUI>().enabled = true;
                    ChancreF.GetComponent<TextMeshProUGUI>().enabled = false;
                }
                if (EnemyGreenSpider.NumberGreenSpiders >= 11)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    HerbesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    HerbesF.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    ChancreNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    ChancreF.GetComponent<TextMeshProUGUI>().enabled = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "QuestPotionDone";
                    PlayerInventory.currentXp += XpQuêtePotion;
                    StartCoroutine(EndQuestChancre());
                }
            }
            if (lastAnswer == (Constructeur.NameCharacter + ": apprendre"))
            {
                if (CharacterMotor.maladieIsUp == false)
                {
                    if (sagesse1 == true)
                    {
                        if (UI.SagesseTotal >= 69)
                        {
                            CharacterMotor.maladie =1;
                            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                            SagesseInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                            SagesseSup1.GetComponent<TextMeshProUGUI>().enabled = true;
                            HerbesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                            HerbesF.GetComponent<TextMeshProUGUI>().enabled = false;
                            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                            ChancreNF.GetComponent<TextMeshProUGUI>().enabled = false;
                            ChancreF.GetComponent<TextMeshProUGUI>().enabled = false;
                            sagesse1 = false;
                            Conversation = false;
                        }
                        if (UI.SagesseTotal < 69)
                        {
                            SagesseInf1.GetComponent<TextMeshProUGUI>().enabled = true;
                            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                            SagesseSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                            HerbesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                            HerbesF.GetComponent<TextMeshProUGUI>().enabled = false;
                            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                            ChancreNF.GetComponent<TextMeshProUGUI>().enabled = false;
                            ChancreF.GetComponent<TextMeshProUGUI>().enabled = false;   
                        }
                    }
                }
                if (CharacterMotor.maladieIsUp == true)
                {
                    SagesseInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    SagesseSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    HerbesNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    HerbesF.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
                    ChancreNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    ChancreF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Conversation = false;
                }
            }
        }
    }
}
