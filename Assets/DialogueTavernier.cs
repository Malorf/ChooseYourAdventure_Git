using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueTavernier : MonoBehaviour
{
    public static int XpQuêteGobelin= 850;
    public static int XpQuêteLieutenant = 1300;
    public static bool QuestGobelin = false;
    public static bool QuestLieutenant = false;
    public static bool Conversation = false;
    public TextMeshProUGUI PNJDial;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI Info;
    public TextMeshProUGUI Reponse;
    public TextMeshProUGUI Service;
    public TextMeshProUGUI Accord;
    public TextMeshProUGUI EliminerNF;
    public TextMeshProUGUI EliminerF;
    public TextMeshProUGUI LieutenantNF;
    public TextMeshProUGUI LieutenantF;
    public TextMeshProUGUI Recompense;
    public TextMeshProUGUI Cimetiere;
    public TextMeshProUGUI ForceInf1;
    public TextMeshProUGUI ForceSup1;
    public TextMeshProUGUI ForceInf2;
    public TextMeshProUGUI ForceSup2;
    public TextMeshProUGUI TextFin;

    public GameObject CountGobelins;
    public GameObject CountLieutenants;
    public GameObject Panel;
    public string lastAnswer;
    public static int force2 = 0;
    public static int force3 = 0;
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
            ForceInf1.GetComponent<TextMeshProUGUI>().enabled = false;
            ForceSup1.GetComponent<TextMeshProUGUI>().enabled = false;
            ForceInf2.GetComponent<TextMeshProUGUI>().enabled = false;
            ForceSup2.GetComponent<TextMeshProUGUI>().enabled = false;
            EliminerNF.GetComponent<TextMeshProUGUI>().enabled = false;
            EliminerF.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
            LieutenantNF.GetComponent<TextMeshProUGUI>().enabled = false;
            LieutenantF.GetComponent<TextMeshProUGUI>().enabled = false;
            Info.GetComponent<TextMeshProUGUI>().enabled = false;
            Reponse.GetComponent<TextMeshProUGUI>().enabled = false;
            Service.GetComponent<TextMeshProUGUI>().enabled = false;
            Accord.GetComponent<TextMeshProUGUI>().enabled = false;
            Recompense.GetComponent<TextMeshProUGUI>().enabled = false;
            Cimetiere.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void GobelinsQuest()
    {
        CountGobelins.SetActive(true);
        CountGobelins.GetComponent<TextMeshProUGUI>().text = "Gobelins " + EnemyGobelin.GobNumber + "/20";
        if (XpQuêteGobelin == 0)
        {
            CountGobelins.SetActive(false);
        }
    }
    public void LieutenantsQuest()
    {
        CountLieutenants.SetActive(true);
        CountLieutenants.GetComponent<TextMeshProUGUI>().text = "Lieutenants " + (EnemyOrc.orc + EnemyGolem.golem) + "/2";
        if (XpQuêteLieutenant == 0)
        {
            CountLieutenants.SetActive(false);
        }
    }
    IEnumerator EndQuestGobelin()
    {
        yield return new WaitForSeconds(1f);
        XpQuêteGobelin = 0;
    }
    IEnumerator EndQuestLieutenant()
    {
        yield return new WaitForSeconds(1f);
        XpQuêteLieutenant = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestGobelin == true)
        {
            GobelinsQuest();
        }
        if (QuestLieutenant == true)
        {
            LieutenantsQuest();
        }
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;

            if (lastAnswer == Constructeur.NameCharacter + ": infos")
            {
                PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                EliminerNF.GetComponent<TextMeshProUGUI>().enabled = false;
                EliminerF.GetComponent<TextMeshProUGUI>().enabled = false;
                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                LieutenantNF.GetComponent<TextMeshProUGUI>().enabled = false;
                LieutenantF.GetComponent<TextMeshProUGUI>().enabled = false;
                Info.GetComponent<TextMeshProUGUI>().enabled = true;
                Reponse.GetComponent<TextMeshProUGUI>().enabled = false;
                Service.GetComponent<TextMeshProUGUI>().enabled = false;
                Accord.GetComponent<TextMeshProUGUI>().enabled = false;
                Recompense.GetComponent<TextMeshProUGUI>().enabled = false;
                Cimetiere.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": réponse")
            {
                PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                EliminerNF.GetComponent<TextMeshProUGUI>().enabled = false;
                EliminerF.GetComponent<TextMeshProUGUI>().enabled = false;
                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                LieutenantNF.GetComponent<TextMeshProUGUI>().enabled = false;
                LieutenantF.GetComponent<TextMeshProUGUI>().enabled = false;
                Info.GetComponent<TextMeshProUGUI>().enabled = false;
                Reponse.GetComponent<TextMeshProUGUI>().enabled = true;
                Service.GetComponent<TextMeshProUGUI>().enabled = false;
                Accord.GetComponent<TextMeshProUGUI>().enabled = false;
                Recompense.GetComponent<TextMeshProUGUI>().enabled = false;
                Cimetiere.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": service")
            {
                PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                EliminerNF.GetComponent<TextMeshProUGUI>().enabled = false;
                EliminerF.GetComponent<TextMeshProUGUI>().enabled = false;
                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                LieutenantNF.GetComponent<TextMeshProUGUI>().enabled = false;
                LieutenantF.GetComponent<TextMeshProUGUI>().enabled = false;
                Info.GetComponent<TextMeshProUGUI>().enabled = false;
                Reponse.GetComponent<TextMeshProUGUI>().enabled = false;
                Service.GetComponent<TextMeshProUGUI>().enabled = true;
                Accord.GetComponent<TextMeshProUGUI>().enabled = false;
                Recompense.GetComponent<TextMeshProUGUI>().enabled = false;
                Cimetiere.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if ((lastAnswer == (Constructeur.NameCharacter + ": oui")) || (lastAnswer == (Constructeur.NameCharacter + ": accord")))
            {
                if (QuestGobelin != true)
                {
                    QuestGobelin = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
                PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                ForceSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                EliminerNF.GetComponent<TextMeshProUGUI>().enabled = false;
                EliminerF.GetComponent<TextMeshProUGUI>().enabled = false;
                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                LieutenantNF.GetComponent<TextMeshProUGUI>().enabled = false;
                LieutenantF.GetComponent<TextMeshProUGUI>().enabled = false;
                Info.GetComponent<TextMeshProUGUI>().enabled = false;
                Reponse.GetComponent<TextMeshProUGUI>().enabled = false;
                Service.GetComponent<TextMeshProUGUI>().enabled = false;
                Accord.GetComponent<TextMeshProUGUI>().enabled = true;
                Recompense.GetComponent<TextMeshProUGUI>().enabled = false;
                Cimetiere.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": éliminer")
            {
                if (EnemyGobelin.GobNumber < 20)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                    EliminerNF.GetComponent<TextMeshProUGUI>().enabled = true;
                    EliminerF.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    LieutenantNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    LieutenantF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Info.GetComponent<TextMeshProUGUI>().enabled = false;
                    Reponse.GetComponent<TextMeshProUGUI>().enabled = false;
                    Service.GetComponent<TextMeshProUGUI>().enabled = false;
                    Accord.GetComponent<TextMeshProUGUI>().enabled = false;
                    Recompense.GetComponent<TextMeshProUGUI>().enabled = false;
                    Cimetiere.GetComponent<TextMeshProUGUI>().enabled = false;
                }
                if (EnemyGobelin.GobNumber >= 20)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                    EliminerNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    EliminerF.GetComponent<TextMeshProUGUI>().enabled = true;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    LieutenantNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    LieutenantF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Info.GetComponent<TextMeshProUGUI>().enabled = false;
                    Reponse.GetComponent<TextMeshProUGUI>().enabled = false;
                    Service.GetComponent<TextMeshProUGUI>().enabled = false;
                    Accord.GetComponent<TextMeshProUGUI>().enabled = false;
                    Recompense.GetComponent<TextMeshProUGUI>().enabled = false;
                    Cimetiere.GetComponent<TextMeshProUGUI>().enabled = false;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "QuestGobelinDone";
                    PlayerInventory.currentXp += XpQuêteGobelin;
                    StartCoroutine(EndQuestGobelin());
                }
            }
            if (lastAnswer == Constructeur.NameCharacter + ": lieutenants" && XpQuêteGobelin == 0)
            {
                if (QuestLieutenant != true)
                {
                    QuestLieutenant = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
                if (EnemyGolem.GolemDead == false || EnemyOrc.OrcDead == false)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                    EliminerNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    EliminerF.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    LieutenantNF.GetComponent<TextMeshProUGUI>().enabled = true;
                    LieutenantF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Info.GetComponent<TextMeshProUGUI>().enabled = false;
                    Reponse.GetComponent<TextMeshProUGUI>().enabled = false;
                    Service.GetComponent<TextMeshProUGUI>().enabled = false;
                    Accord.GetComponent<TextMeshProUGUI>().enabled = false;
                    Recompense.GetComponent<TextMeshProUGUI>().enabled = false;
                    Cimetiere.GetComponent<TextMeshProUGUI>().enabled = false;
                }
                if (EnemyGolem.GolemDead == true && EnemyOrc.OrcDead == true)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                    EliminerNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    EliminerF.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    LieutenantNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    LieutenantF.GetComponent<TextMeshProUGUI>().enabled = true;
                    Info.GetComponent<TextMeshProUGUI>().enabled = false;
                    Reponse.GetComponent<TextMeshProUGUI>().enabled = false;
                    Service.GetComponent<TextMeshProUGUI>().enabled = false;
                    Accord.GetComponent<TextMeshProUGUI>().enabled = false;
                    Recompense.GetComponent<TextMeshProUGUI>().enabled = false;
                    Cimetiere.GetComponent<TextMeshProUGUI>().enabled = false;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "QuestLieutenantDone";
                    PlayerInventory.currentXp += XpQuêteLieutenant;
                    StartCoroutine(EndQuestLieutenant());
                }
                if (lastAnswer == Constructeur.NameCharacter + ": récompense" && XpQuêteLieutenant == 0)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                    EliminerNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    EliminerF.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    LieutenantNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    LieutenantF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Info.GetComponent<TextMeshProUGUI>().enabled = false;
                    Reponse.GetComponent<TextMeshProUGUI>().enabled = false;
                    Service.GetComponent<TextMeshProUGUI>().enabled = false;
                    Accord.GetComponent<TextMeshProUGUI>().enabled = false;
                    Recompense.GetComponent<TextMeshProUGUI>().enabled = true;
                    Cimetiere.GetComponent<TextMeshProUGUI>().enabled = false;
                }
                if (lastAnswer == Constructeur.NameCharacter + ": cimetière" && XpQuêteLieutenant == 0)
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                    ForceSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                    EliminerNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    EliminerF.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                    LieutenantNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    LieutenantF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Info.GetComponent<TextMeshProUGUI>().enabled = false;
                    Reponse.GetComponent<TextMeshProUGUI>().enabled = false;
                    Service.GetComponent<TextMeshProUGUI>().enabled = false;
                    Accord.GetComponent<TextMeshProUGUI>().enabled = false;
                    Recompense.GetComponent<TextMeshProUGUI>().enabled = false;
                    Cimetiere.GetComponent<TextMeshProUGUI>().enabled = true;
                }
            }
            if ( lastAnswer == Constructeur.NameCharacter + ": apprendre")
            {

                if (force3 == 0)
                {
                    if (force2 == 0)
                    {
                        PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                        TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                        if (UI.ForceTotal >= 63)
                        {
                            PlayerInventory.currentDamage += 10;
                            ForceSup1.GetComponent<TextMeshProUGUI>().enabled = true;
                            force2 = 0;
                            Conversation = false;
                        }
                        else ForceInf1.GetComponent<TextMeshProUGUI>().enabled = true;
                        Conversation = false;
                    }
                    else
                    {
                        PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                        TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                        if (UI.ForceTotal >= 86)
                        {
                            PlayerInventory.currentDamage += 10;
                            ForceSup2.GetComponent<TextMeshProUGUI>().enabled = true;
                            force3 = 0;
                            Conversation = false;
                        }
                        else ForceInf2.GetComponent<TextMeshProUGUI>().enabled = true;
                        Conversation = false;
                    }
                }
                else
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
                    Conversation = false;
                }
            }
        }
    }
}
