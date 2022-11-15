using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue3 : MonoBehaviour
{
    public static bool Conversation = false;
    public TextMeshProUGUI PNJ3;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI Aide;
    public TextMeshProUGUI Charles;
    public static bool QuestWolfIsUp = false;
    public string lastAnswer;
    public GameObject Panel;
    public GameObject CountWolf;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJ3.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJ3.GetComponent<TextMeshProUGUI>().enabled = false;
            Aide.GetComponent<TextMeshProUGUI>().enabled = false;
            Charles.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
    // Start is called before the first frame update

    IEnumerator QuêteValide()
    {
        yield return new WaitForSeconds(1f);
        GameManager.PlayerAnswer = "a";
        lastAnswer = GameManager.PlayerAnswer;
    }
    // Update is called once per frame
    void Update()
    {
        if (QuestWolfIsUp == true)
        {
            CountWolf.SetActive(true);
            CountWolf.GetComponent<TextMeshProUGUI>().text = ("Loups " + EnemyAiWolf.WolfQuest + "/8");
            if (Dialogue2.XpQuêteLoup == 0)
            {
                CountWolf.SetActive(false);
            }
        }
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;
            UI endurance = GameObject.Find("Stats").GetComponent<UI>();
            if (lastAnswer == Constructeur.NameCharacter + ": aide")
            { 
                if (QuestWolfIsUp != true)
                {
                    QuestWolfIsUp = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
                PNJ3.GetComponent<TextMeshProUGUI>().enabled = false;
                Charles.GetComponent<TextMeshProUGUI>().enabled = false;
                Aide.GetComponent<TextMeshProUGUI>().enabled = true;
                StartCoroutine(QuêteValide());
            }
            if (lastAnswer == Constructeur.NameCharacter + ": Charles")
            {
                Charles.GetComponent<TextMeshProUGUI>().enabled = true;
                PNJ3.GetComponent<TextMeshProUGUI>().enabled = false;
                Aide.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            
        }

    }
}
