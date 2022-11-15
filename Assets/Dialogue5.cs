using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue5 : EnemyAi
{
    public static int XpQuêteJail = 375;
    public static bool Conversation = false;
    public TextMeshProUGUI PNJ5;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI Sortir;
    public TextMeshProUGUI GeôlierNF;
    public TextMeshProUGUI GeôlierF;
    public TextMeshProUGUI Chef;
    public TextMeshProUGUI Escorte;
    public TextMeshProUGUI Cellule;
    public TextMeshProUGUI Remerciement;
    public TextMeshProUGUI DexSup;
    public TextMeshProUGUI DexInf;
    public TextMeshProUGUI DexSup2;
    public TextMeshProUGUI DexInf2;
    public TextMeshProUGUI TextFin;

    public GameObject CountJailer;

    public static int dex1 = 0;
    public static int dex2 = 0;
    public static bool Ally = false;
    public static bool TextCombat = true;
    public static bool CineEvasion = false;
    public static bool QuestGeôlier = false;
    public static bool QuestChief = false;
    public static bool QuestJailsIsEnd = false;
    public static int JailVisited = 0;
    public string lastAnswer;
    public GameObject Panel;
    private Animator AnimCam2;
    private Animator AnimDoorL;
    private Animator AnimDoorR;
    private Animator JailDoorL;
    private Animator JailDoorR;
    private Animator AnimCam3;
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (Ally == false))
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJ5.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            if (Dialogue4.QuestJailIsUp == true)
            {
                QuestJailsIsEnd = true;
                JailVisited = 1;
            }
        }
        if ((other.gameObject.tag == "Player") && (Ally == true))
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            Remerciement.GetComponent<TextMeshProUGUI>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (Ally == false))
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJ5.GetComponent<TextMeshProUGUI>().enabled = false;
            Sortir.GetComponent<TextMeshProUGUI>().enabled = false;
            GeôlierNF.GetComponent<TextMeshProUGUI>().enabled = false;
            GeôlierF.GetComponent<TextMeshProUGUI>().enabled = false;
            Chef.GetComponent<TextMeshProUGUI>().enabled = false;
            Escorte.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if ((other.gameObject.tag == "Player") && (Ally == true))
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            Remerciement.GetComponent<TextMeshProUGUI>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
            DexInf.GetComponent<TextMeshProUGUI>().enabled = false;
            DexInf2.GetComponent<TextMeshProUGUI>().enabled = false;
            DexSup.GetComponent<TextMeshProUGUI>().enabled = false;
            DexSup2.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        AnimDoorL = GameObject.Find("DoorL").GetComponent<Animator>();
        AnimDoorR = GameObject.Find("DoorR").GetComponent<Animator>();
        JailDoorL = GameObject.Find("JailDoor").GetComponent<Animator>();
        JailDoorR = GameObject.Find("JailDoorPNJ").GetComponent<Animator>();
        AnimCam2 = GameObject.Find("CamCineChien2").GetComponent<Animator>();
        AnimCam3 = GameObject.Find("CamCineChien3").GetComponent<Animator>();
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animator>();
        attackTime = Time.time;
        waypointIndex = 0;
        collider = GetComponent<CapsuleCollider>();
        basePositions = transform.position;
        hpImage.enabled = false;
        backgroundHp.enabled = false;
        hpEnemy = hpMax;
    }

    public void JailerQuest()
    {
        CountJailer.SetActive(true);
        CountJailer.GetComponent<TextMeshProUGUI>().text = "Geôlier " + EnemyDogJail.Jailer + "/1";
        if (XpQuêteJail == 0)
        {
            CountJailer.SetActive(false);
        }
    }
    IEnumerator QuêteValide()
    {
        yield return new WaitForSeconds(1f);
        GameManager.PlayerAnswer = "a";
        lastAnswer = GameManager.PlayerAnswer;
    }
    IEnumerator TempsAvantEscorte()
    {
        yield return new WaitForSeconds(5f);
        QuestChief = true;
        Ally = true;
        PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
        Escorte.GetComponent<TextMeshProUGUI>().enabled = false;
        Panel.GetComponent<Image>().enabled = false;
        PNJ5.GetComponent<TextMeshProUGUI>().enabled = false;
    }
    IEnumerator TempsAvantCombat()
    {
        yield return new WaitForSeconds(8f);
        TextCombat = false;
    }
    IEnumerator EndQuestJail()
    {
        yield return new WaitForSeconds(1f);
        XpQuêteJail = 0;
    }
    protected override void chase()
    {
        animations.Play("RunForward");
        if (!((Ally == true) && (QuestChief == false)))
        {
            agent.destination = Target.position;
        }
    }

    protected override void idle()
    {
        animations.Play("Idle");
    }
    protected override void Dead()
    {
        isDead = true;
        backgroundHp.enabled = false;
        hpImage.enabled = false;
        collider.enabled = false;
        animations.Play("Death");
        if (QuestChief == true)
        {
            QuestChief = false;
        }
        if (AnimCam3 != null)
        {
            AnimCam3.SetBool("CamChien3Activate", true);
        }
        AnimDoorL.SetBool("OpenDoorL", true);
        AnimDoorR.SetBool("OpenDoorR", true);
        Experience();
        Destroy(transform.gameObject, 61);
    }
    protected override void attack()
    {
        // empeche l'ennemi de traverser le joueur
        agent.destination = transform.position;

        //Si pas de cooldown
        if (Time.time > attackTime)
        {
            animations.Play("human_male_punch_mirrored_01");
            Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage);
            attackTime = Time.time + attackRepeatTime;
        }
    }
    protected override void BackBase()
    {
        animations.Play("RunForward");
        agent.destination = basePositions;
    }
    void follow ()
    {
        collider.center = Vector3.up;
        collider.radius = 1;
        collider.isTrigger = false;
        hpEnemy = hpMax;
        agent.stoppingDistance = 5;
        if (Distance >= chaseRange-4)
        {
            backgroundHp.enabled = false;
            hpImage.enabled = false;
            chase();
        }

        if (Distance < chaseRange-4 && Distance > attackRange)
        {
            backgroundHp.enabled = false;
            hpImage.enabled = false;
            idle();
        }
        if (Distance <= attackRange)
        {
            backgroundHp.enabled = false;
            hpImage.enabled = false;
            idle();
        }
        if (CharacterMotor.isDead == true)
        {
            Ally = false;
            QuestChief = false;
            reset();
        }
    }
    void reset()
    {
        collider.isTrigger = true;
        collider.radius = 3;
        AnimDoorL.SetBool("OpenDoorL", true);
        AnimDoorR.SetBool("OpenDoorR", true);
    }
    void fight()
    {
        collider.center = Vector3.up;
        collider.radius = 1;
        collider.isTrigger = false;
        agent.stoppingDistance = 1;
        if (Distance >= 2* chaseRange)
        {
            backgroundHp.enabled = false;
            hpImage.enabled = false;
            idle();
            hpEnemy = hpMax;
        }
        if (Distance < chaseRange && Distance > attackRange)
        {
            backgroundHp.enabled = true;
            hpImage.enabled = true;
            chase();
        }

        if (Distance <= attackRange)
        {
            backgroundHp.enabled = true;
            hpImage.enabled = true;
            attack();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (QuestGeôlier == true)
        {
            JailerQuest();
        }
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
            if ((Ally == false) && (QuestChief == false))
            {
                BackBase();
            }

            if ((Ally == true) && (QuestChief == true))
            {
                follow();
            }
            if ((Ally == false) && (QuestChief == true) && (TextCombat == true))
            {
                PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
                Panel.GetComponent<Image>().enabled = true;
                Cellule.GetComponent<TextMeshProUGUI>().enabled = true;
                StartCoroutine(TempsAvantCombat());
            }
            if ((Ally == false) && (QuestChief == true) && (TextCombat == false))
            {
                Panel.GetComponent<Image>().enabled = false;
                Cellule.GetComponent<TextMeshProUGUI>().enabled = false;
                fight();
            }
            if ((Ally == true) && (QuestChief == false))
            {
                agent.speed = 0;
                collider.isTrigger = true;
                collider.radius = 3;
                idle();
                if (Conversation && Ally == true)
                {
                    lastAnswer = GameManager.PlayerAnswer;
                    if (lastAnswer == Constructeur.NameCharacter + ": apprendre")
                    {
                        if (dex2 == 0)
                        {
                            if (dex1 == 0)
                            {
                                Remerciement.GetComponent<TextMeshProUGUI>().enabled = false;
                                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                                if (UI.DexteriteTotal >= 27)
                                {
                                    CharacterMotor.attackCooldown -= 0.2f;
                                    DexSup.GetComponent<TextMeshProUGUI>().enabled = true;
                                    Debug.Log("la vitesse d'attaque est à" + CharacterMotor.attackCooldown);
                                    dex1 = 1;
                                    Conversation = false;
                                }
                                else DexInf.GetComponent<TextMeshProUGUI>().enabled = true;
                                Conversation = false;
                            }
                            else
                            {
                                Remerciement.GetComponent<TextMeshProUGUI>().enabled = false;
                                TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                                if (UI.DexteriteTotal >= 49)
                                {
                                    CharacterMotor.attackCooldown -= 0.2f;
                                    DexSup2.GetComponent<TextMeshProUGUI>().enabled = true;
                                    Debug.Log("la vitesse d'attaque est à" + CharacterMotor.attackCooldown);
                                    dex2 = 1;
                                    Conversation = false;
                                }
                                else DexInf2.GetComponent<TextMeshProUGUI>().enabled = true;
                                Conversation = false;
                            }
                        }
                        else
                        {
                            Remerciement.GetComponent<TextMeshProUGUI>().enabled = false;
                            TextFin.GetComponent<TextMeshProUGUI>().enabled = true;
                            Conversation = false;
                        }
                    }
                }
            }
        }
        
        if (Conversation && Ally == false)
        {
            lastAnswer = GameManager.PlayerAnswer;
            if (lastAnswer == Constructeur.NameCharacter + ": sortir")
            {
                PNJ5.GetComponent<TextMeshProUGUI>().enabled = false;
                Sortir.GetComponent<TextMeshProUGUI>().enabled = true;
                GeôlierNF.GetComponent<TextMeshProUGUI>().enabled = false;
                GeôlierF.GetComponent<TextMeshProUGUI>().enabled = false;
                Chef.GetComponent<TextMeshProUGUI>().enabled = false;
                Escorte.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": geôlier")
            {
                if (QuestGeôlier != true)
                {
                    QuestGeôlier = true;
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
                if (EnemyDogJail.JailQuest == false)
                {
                    PNJ5.GetComponent<TextMeshProUGUI>().enabled = false;
                    Sortir.GetComponent<TextMeshProUGUI>().enabled = false;
                    GeôlierNF.GetComponent<TextMeshProUGUI>().enabled = true;
                    GeôlierF.GetComponent<TextMeshProUGUI>().enabled = false;
                    Chef.GetComponent<TextMeshProUGUI>().enabled = false;
                    Escorte.GetComponent<TextMeshProUGUI>().enabled = false;
                    StartCoroutine(QuêteValide());
                }
                if (EnemyDogJail.JailQuest == true)
                {
                    //porte prison s'ouvre
                    JailDoorL.SetBool("OpenJailDoor", true);
                    JailDoorR.SetBool("OpenJailDoorPNJ", true);
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest4Done";
                    PlayerInventory.currentXp += XpQuêteJail;
                    PNJ5.GetComponent<TextMeshProUGUI>().enabled = false;
                    Sortir.GetComponent<TextMeshProUGUI>().enabled = false;
                    GeôlierNF.GetComponent<TextMeshProUGUI>().enabled = false;
                    GeôlierF.GetComponent<TextMeshProUGUI>().enabled = true;
                    Chef.GetComponent<TextMeshProUGUI>().enabled = false;
                    Escorte.GetComponent<TextMeshProUGUI>().enabled = false;
                    StartCoroutine(EndQuestJail());
                }
            }
            if (lastAnswer == Constructeur.NameCharacter + ": évader")
            {
                //cinématique
                CineEvasion = true;
                if (AnimCam2 != null)
                {
                    AnimCam2.SetBool("CamChien2Activate", true);
                }
                AnimDoorL.SetBool("OpenDoorL", false);
                AnimDoorR.SetBool("OpenDoorR", false);
                PNJ5.GetComponent<TextMeshProUGUI>().enabled = false;
                Sortir.GetComponent<TextMeshProUGUI>().enabled = false;
                GeôlierNF.GetComponent<TextMeshProUGUI>().enabled = false;
                GeôlierF.GetComponent<TextMeshProUGUI>().enabled = false;
                Chef.GetComponent<TextMeshProUGUI>().enabled = true;
                Escorte.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": chef")
            {
                if (QuestChief != true)
                {
                    StartCoroutine(TempsAvantEscorte());
                    GameManager.messageList.Clear();
                    GameManager.PlayerAnswer = "Quest1Activate";
                }
                PNJ5.GetComponent<TextMeshProUGUI>().enabled = false;
                Sortir.GetComponent<TextMeshProUGUI>().enabled = false;
                GeôlierNF.GetComponent<TextMeshProUGUI>().enabled = false;
                GeôlierF.GetComponent<TextMeshProUGUI>().enabled = false;
                Chef.GetComponent<TextMeshProUGUI>().enabled = false;
                Escorte.GetComponent<TextMeshProUGUI>().enabled = true;
                EnemyDogChief.AllyDog = true;
                StartCoroutine(QuêteValide());
            }
        }
    }
}