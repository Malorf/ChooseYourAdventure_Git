using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueBlackSmith : MonoBehaviour
{
    public static int XpQuêteChienvalier = 420;
    public static bool QuestChienvalier = false;
    public static bool Conversation = false;
    public TextMeshProUGUI PNJDial;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI CommandeNF;
    public TextMeshProUGUI CommandeF;
    public TextMeshProUGUI EnduSup1;
    public TextMeshProUGUI EnduInf1;
    public TextMeshProUGUI EnduSup2;
    public TextMeshProUGUI EnduInf2;
    public TextMeshProUGUI TextFin;

    public GameObject CountArmor;
    public GameObject Panel;
    public string lastAnswer;
    public static int endurance2 = 0;
    public static int endurance3 = 0;
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
            EnduInf1.GetComponent<TextMeshProUGUI>().enabled = false;
            EnduSup1.GetComponent<TextMeshProUGUI>().enabled = false;
            EnduInf2.GetComponent<TextMeshProUGUI>().enabled = false;
            EnduSup2.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
            CommandeNF.GetComponent<TextMeshProUGUI>().enabled = false;
            CommandeF.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void ArmorQuest()
    {
        CountArmor.SetActive(true);
        CountArmor.GetComponent<TextMeshProUGUI>().text = "Armures " + EnemyKnightDog.NumberChienvalierQuest + "/10";
        if (XpQuêteChienvalier == 0)
        {
            CountArmor.SetActive(false);
        }
    }
    IEnumerator EndQuest()
    {
        yield return new WaitForSeconds(1f);
        XpQuêteChienvalier = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestChienvalier== true)
        {
            ArmorQuest();
        }
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;

            if (lastAnswer == Constructeur.NameCharacter + ": commande")
            {
                if (QuestChienvalier != true)
                {
                    QuestChienvalier = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
                if (EnemyKnightDog.NumberChienvalierQuest < 10)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    CommandeNF.GetComponent<TextMeshProUGUI>().enabled = true;
                    CommandeF.GetComponent<TextMeshProUGUI>().enabled = false;
                }
                if (EnemyKnightDog.NumberChienvalierQuest >= 10)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    CommandeNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    CommandeF.GetComponent<TextMeshProUGUI>().enabled = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest10ChienvaliersDone";
                    PlayerInventory.currentXp += XpQuêteChienvalier;
                    StartCoroutine(EndQuest());
                }
            }
            if ((lastAnswer == (Constructeur.NameCharacter + ": oui")) || (lastAnswer == (Constructeur.NameCharacter + ": apprendre")))
            {
                if (EnemyKnightDog.NumberChienvalierQuest >= 10)
                {
                    if (endurance3 == 0)
                    {
                        if (endurance2 == 0)
                        {
                            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                            CommandeNF.GetComponent<TextMeshProUGUI>().enabled = false;
                            CommandeF.GetComponent<TextMeshProUGUI>().enabled = false;
                            if (UI.EnduranceTotal >= 72)
                            {
                                PlayerInventory.maxHealth += 20;
                                EnduSup1.GetComponent<TextMeshProUGUI>().enabled = true;
                                endurance2 = 0;
                                Conversation = false;
                            }
                            else EnduInf1.GetComponent<TextMeshProUGUI>().enabled = true;
                            Conversation = false;
                        }
                        else
                        {
                            PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                            CommandeNF.GetComponent<TextMeshProUGUI>().enabled = false;
                            CommandeF.GetComponent<TextMeshProUGUI>().enabled = false;
                            if (UI.EnduranceTotal >= 118)
                            {
                                PlayerInventory.maxHealth += 30;
                                EnduSup2.GetComponent<TextMeshProUGUI>().enabled = true;
                                endurance3 = 0;
                                Conversation = false;
                            }
                            else EnduInf2.GetComponent<TextMeshProUGUI>().enabled = true;
                            Conversation = false;
                        }
                    }
                    else
                    {
                        PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                        TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
                        CommandeNF.GetComponent<TextMeshProUGUI>().enabled = false;
                        CommandeF.GetComponent<TextMeshProUGUI>().enabled = false;
                        Conversation = false;
                    }
                }
            }
        }
    }
}

