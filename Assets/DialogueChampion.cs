using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueChampion : EnemyGuard
{
    public static bool QuestChampion = false;
    public static bool Conversation = false;
    public bool challenged = false;
    public TextMeshProUGUI PNJDial;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI Défi;
    public TextMeshProUGUI DéfiF;
    public TextMeshProUGUI DexteriteSup1;
    public TextMeshProUGUI DexteriteInf1;
    public TextMeshProUGUI DexteriteSup2;
    public TextMeshProUGUI DexteriteInf2;
    public TextMeshProUGUI TextFin;

    public GameObject Panel;
    public string lastAnswer;
    public static int dexterite3 = 0;
    public static int dexterite4 = 0;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (DialogueDuchelvau.QuestChampion == true))
        {
            if (DialogueDuchelvau.XpQuêteChampion != 0)
            {
                Conversation = true;
                Panel.GetComponent<Image>().enabled = true;
                PNJDial.GetComponent<TextMeshProUGUI>().enabled = true;
                PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
        if ((other.gameObject.tag == "Player") && (DialogueDuchelvau.XpQuêteChampion == 0))
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
            DexteriteInf1.GetComponent<TextMeshProUGUI>().enabled = false;
            DexteriteSup1.GetComponent<TextMeshProUGUI>().enabled = false;
            DexteriteInf2.GetComponent<TextMeshProUGUI>().enabled = false;
            DexteriteSup2.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
            Défi.GetComponent<TextMeshProUGUI>().enabled = false;
            DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animator>();
        attackTime = Time.time;
        waypointIndex = 0;
        basePositions = transform.position;
        hpImage.enabled = false;
        backgroundHp.enabled = false;
        hpEnemy = hpMax;
        collider = GetComponent<CapsuleCollider>();
        collider.isTrigger = true;
    }
    IEnumerator TempsAvantCombat()
    {
        yield return new WaitForSeconds(4f);
        challenged = true;
        collider.isTrigger = false;
        collider.radius = 1;
        PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
    }
    IEnumerator EndQuest()
    {
        yield return new WaitForSeconds(1f);
        DialogueDuchelvau.XpQuêteChampion = 0;
        collider.isTrigger = true;
        collider.radius = 3;
    }
    // Update is called once per frame
    void Update()
    {
        float percentageHp = ((hpEnemy * 100) / hpMax) / 100; //barre de vie
        hpImage.fillAmount = percentageHp;

        if (!isDead)
        {

            // On cherche le joueur en permanence
            Target = GameObject.Find("Player").transform;

            // On calcule la distance entre le joueur et l'ennemi, en fonction de cette distance on effectue diverses actions
            Distance = Vector3.Distance(Target.position, transform.position);

            // On calcule la distance entre l'ennemi et sa position de base
            DistanceBase = Vector3.Distance(basePositions, transform.position);

            // Quand l'ennemi est loin = idle
            if ((challenged == true) && (hpEnemy > (hpMax / 3)))
            {
                // Quand l'ennemi est proche mais pas assez pour attaquer
                if (Distance < chaseRange && Distance > attackRange)
                {
                    backgroundHp.enabled = true;
                    hpImage.enabled = true;
                    chase();
                }

                // Quand l'ennemi est assez proche pour attaquer
                if (Distance < attackRange)
                {
                    backgroundHp.enabled = true;
                    hpImage.enabled = true;
                    attack();
                }

                //Quand le joueur s'est échappé
                if (Distance > 2 * chaseRange)
                {
                    backgroundHp.enabled = false;
                    hpImage.enabled = false;
                    hpEnemy = hpMax;
                    BackBase();
                }
                //quand le monstre se fait taper de loin
                if (Distance > chaseRange && Distance <= 2 * chaseRange)
                {
                    if (hpEnemy != hpMax)
                    {
                        chase();
                    }
                }
            }
        }
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;

            if ((lastAnswer == Constructeur.NameCharacter + ": défi") && DialogueDuchelvau.XpQuêteChampion != 0)
            {
                PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                DexteriteInf1.GetComponent<TextMeshProUGUI>().enabled = false;
                DexteriteSup1.GetComponent<TextMeshProUGUI>().enabled = false;
                DexteriteInf2.GetComponent<TextMeshProUGUI>().enabled = false;
                DexteriteSup2.GetComponent<TextMeshProUGUI>().enabled = false;
                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                Défi.GetComponent<TextMeshProUGUI>().enabled = true;
                DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
                StartCoroutine(TempsAvantCombat());
            }
            if ((lastAnswer == Constructeur.NameCharacter + ": apprendre") && DialogueDuchelvau.XpQuêteChampion == 0)
            {
                if (dexterite4 == 0)
                {
                    if (dexterite3 == 0)
                    {
                        PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                        TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                        Défi.GetComponent<TextMeshProUGUI>().enabled = false;
                        DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
                        if (UI.DexteriteTotal >= 64)
                        {
                            CharacterMotor.attackCooldown -= 0.2f;
                            DexteriteSup1.GetComponent<TextMeshProUGUI>().enabled = true;
                            dexterite3 = 1;
                            Conversation = false;
                        }
                        else DexteriteInf1.GetComponent<TextMeshProUGUI>().enabled = true;
                        Conversation = false;
                    }
                    else
                    {
                        PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                        TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                        Défi.GetComponent<TextMeshProUGUI>().enabled = false;
                        DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
                        if (UI.DexteriteTotal >= 89)
                        {
                            CharacterMotor.attackCooldown -= 0.2f;
                            DexteriteSup2.GetComponent<TextMeshProUGUI>().enabled = true;
                            dexterite4 = 1;
                            Conversation = false;
                        }
                        else DexteriteInf2.GetComponent<TextMeshProUGUI>().enabled = true;
                        Conversation = false;
                    }
                }
                else
                {
                    PNJDial.GetComponent<TextMeshProUGUI>().enabled = false;
                    Défi.GetComponent<TextMeshProUGUI>().enabled = false;
                    DéfiF.GetComponent<TextMeshProUGUI>().enabled = false;
                    TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
                    Conversation = false;
                }
            }
        } 
        if ((hpEnemy <= (hpMax/3)) && challenged == true)
        {
            challenged = false;
            backgroundHp.enabled = false;
            hpImage.enabled = false;
            QuestChampion = false;
            GameManager.messageList.Clear();
            GameManager.PlayerAnswer = "QuestChampionDone";
            PlayerInventory.currentXp += DialogueDuchelvau.XpQuêteChampion;
            idle();
            StartCoroutine(EndQuest());
        }
    }
}

