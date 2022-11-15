using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueChief : MonoBehaviour
{
    public static bool AllyDog = false;
    public static bool Conversation = false;
    public TextMeshProUGUI PNJChief;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI Menace;
    public TextMeshProUGUI Justice;
    public TextMeshProUGUI ChoixHumain;
    public TextMeshProUGUI ChoixChien;
    public TextMeshProUGUI Choix;


    public string lastAnswer;
    public GameObject Panel;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            Menace.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            Justice.GetComponent<TextMeshProUGUI>().enabled = false;
            ChoixHumain.GetComponent<TextMeshProUGUI>().enabled = false;
            ChoixChien.GetComponent<TextMeshProUGUI>().enabled = false;
            Choix.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            Menace.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            Justice.GetComponent<TextMeshProUGUI>().enabled = false;
            ChoixHumain.GetComponent<TextMeshProUGUI>().enabled = false;
            ChoixChien.GetComponent<TextMeshProUGUI>().enabled = false;
            Choix.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    IEnumerator DialogueCineChien()
    {
        yield return new WaitForSeconds(8f);
        Panel.GetComponent<Image>().enabled = false;
        PNJChief.GetComponent<TextMeshProUGUI>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Dialogue5.CineEvasion == true)
        {
            Panel.GetComponent<Image>().enabled = true;
            PNJChief.GetComponent<TextMeshProUGUI>().enabled = true;
            StartCoroutine(DialogueCineChien());
        }
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;
            if (lastAnswer == Constructeur.NameCharacter + ": fourrures")
            {
                Menace.GetComponent<TextMeshProUGUI>().enabled = false;
                Justice.GetComponent<TextMeshProUGUI>().enabled = true;
                ChoixHumain.GetComponent<TextMeshProUGUI>().enabled = false;
                ChoixChien.GetComponent<TextMeshProUGUI>().enabled = false;
                Choix.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": justice")
            {
                Menace.GetComponent<TextMeshProUGUI>().enabled = false;
                Justice.GetComponent<TextMeshProUGUI>().enabled = false;
                ChoixHumain.GetComponent<TextMeshProUGUI>().enabled = false;
                ChoixChien.GetComponent<TextMeshProUGUI>().enabled = false;
                Choix.GetComponent<TextMeshProUGUI>().enabled = true;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": cellule")
            {
                Menace.GetComponent<TextMeshProUGUI>().enabled = false;
                Justice.GetComponent<TextMeshProUGUI>().enabled = false;
                ChoixHumain.GetComponent<TextMeshProUGUI>().enabled = false;
                ChoixChien.GetComponent<TextMeshProUGUI>().enabled = true;
                Choix.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": corps")
            {
                Menace.GetComponent<TextMeshProUGUI>().enabled = false;
                Justice.GetComponent<TextMeshProUGUI>().enabled = false;
                ChoixHumain.GetComponent<TextMeshProUGUI>().enabled = true;
                ChoixChien.GetComponent<TextMeshProUGUI>().enabled = false;
                Choix.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
    }
}