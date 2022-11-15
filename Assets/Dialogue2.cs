using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue2 : MonoBehaviour
{
    public static int XpQuêteLoup = 150;
    public static bool Conversation = false;
    public TextMeshProUGUI PNJ2;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI Utile;
    public TextMeshProUGUI EnduSup;
    public TextMeshProUGUI EnduInf;
    public TextMeshProUGUI TextFin;
    public TextMeshProUGUI TextServiceNF;
    public TextMeshProUGUI TextServiceF;
    public GameObject Panel;
    public string lastAnswer;
    public static int endurance1 = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJ2.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJ2.GetComponent<TextMeshProUGUI>().enabled = false;
            Utile.GetComponent<TextMeshProUGUI>().enabled = false;
            EnduInf.GetComponent<TextMeshProUGUI>().enabled = false;
            EnduSup.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
            TextServiceNF.GetComponent<TextMeshProUGUI>().enabled = false;
            TextServiceF.GetComponent<TextMeshProUGUI>().enabled = false;
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
        XpQuêteLoup = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;

            if (lastAnswer == Constructeur.NameCharacter + ": utile")
            {
                PNJ2.GetComponent<TextMeshProUGUI>().enabled = false;
                EnduInf.GetComponent<TextMeshProUGUI>().enabled = false;
                EnduSup.GetComponent<TextMeshProUGUI>().enabled = false;
                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                TextServiceNF.GetComponent<TextMeshProUGUI>().enabled = false;
                TextServiceF.GetComponent<TextMeshProUGUI>().enabled = false;
                Utile.GetComponent<TextMeshProUGUI>().enabled = true;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": service")
            {
                if (EnemyAiWolf.WolfQuest >= 8)
                {
                    TextServiceF.GetComponent<TextMeshProUGUI>().enabled = true;
                    TextServiceNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    PNJ2.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduSup.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    Utile.GetComponent<TextMeshProUGUI>().enabled = false;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Done";
                    PlayerInventory.currentXp += XpQuêteLoup;
                    StartCoroutine(EndQuest());
                }
                if (EnemyAiWolf.WolfQuest < 8)
                {
                    TextServiceNF.GetComponent<TextMeshProUGUI>().enabled = true;
                    TextServiceF.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    PNJ2.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduSup.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    Utile.GetComponent<TextMeshProUGUI>().enabled = false;
                }
            }
            if (lastAnswer == Constructeur.NameCharacter + ": apprendre")
            {
                Utile.GetComponent<TextMeshProUGUI>().enabled = false;
                TextServiceNF.GetComponent<TextMeshProUGUI>().enabled = false;
                TextServiceF.GetComponent<TextMeshProUGUI>().enabled = false;
                if (endurance1 == 0)
                {
                    PNJ2.GetComponent<TextMeshProUGUI>().enabled = false;
                    if (UI.EnduranceTotal >= 36)
                    {
                        PlayerInventory.maxHealth += 10;
                        EnduSup.GetComponent<TextMeshProUGUI>().enabled = true;
                        Debug.Log("les points de vie sont à " + PlayerInventory.maxHealth);
                        endurance1 = 1;
                        Conversation = false;
                    }
                    else EnduInf.GetComponent<TextMeshProUGUI>().enabled = true;
                    Conversation = false;
                }
                else
                {
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
                    PNJ2.GetComponent<TextMeshProUGUI>().enabled = false;
                    EnduInf.GetComponent<TextMeshProUGUI>().enabled = false;
                    Utile.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextServiceNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextServiceF.GetComponent<TextMeshProUGUI>().enabled = false;
                }
            }
        }

    }
}
