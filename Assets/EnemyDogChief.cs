using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDogChief : EnemyKnightDog
{
    public static bool AllyDog = true;
    public static bool Conversation = false;
    public TextMeshProUGUI PNJChief;
    public TextMeshProUGUI PNJName;
    public TextMeshProUGUI Menace;
    public TextMeshProUGUI Justice;
    public TextMeshProUGUI ChoixHumain;
    public TextMeshProUGUI ChoixChien;
    public TextMeshProUGUI Choix;
    public TextMeshProUGUI Remerciement;
    public TextMeshProUGUI CourageSup;
    public TextMeshProUGUI CourageInf;
    public TextMeshProUGUI CourageSup2;
    public TextMeshProUGUI CourageInf2;
    public TextMeshProUGUI TextFin;

    public static int courage2 = 0;
    public static int courage1 = 0;
    private Animator AnimDoorL;
    private Animator AnimDoorR;
    private Animator AnimCam3;
    public string lastAnswer;
    public GameObject PanelCine;
    public GameObject Panel;
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        AnimCam3 = GameObject.Find("CamCineChien3").GetComponent<Animator>();
        AnimDoorL = GameObject.Find("DoorL").GetComponent<Animator>();
        AnimDoorR = GameObject.Find("DoorR").GetComponent<Animator>();
        animations = gameObject.GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        attackTime = Time.time;
        waypointIndex = 0;
        basePositions = transform.position;
        hpImage.enabled = false;
        backgroundHp.enabled = false;
        hpEnemy = hpMax;
    }
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (Dialogue5.QuestChief == true))
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
        if ((other.gameObject.tag == "Player") && (Dialogue5.QuestChief == false))
        {
            Conversation = true;
            Panel.GetComponent<Image>().enabled = true;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = false;
            Remerciement.GetComponent<TextMeshProUGUI>().enabled = true;
            CourageSup.GetComponent<TextMeshProUGUI>().enabled = false;
            CourageInf.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "Player") &&(Dialogue5.QuestChief == true))
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
        if ((other.gameObject.tag == "Player") && (Dialogue5.QuestChief == false))
        {
            Conversation = false;
            Panel.GetComponent<Image>().enabled = false;
            PNJName.GetComponent<TextMeshProUGUI>().enabled = true;
            Remerciement.GetComponent<TextMeshProUGUI>().enabled = false;
            ChoixChien.GetComponent<TextMeshProUGUI>().enabled = false;
            CourageSup.GetComponent<TextMeshProUGUI>().enabled = false;
            CourageInf.GetComponent<TextMeshProUGUI>().enabled = false;
            CourageSup2.GetComponent<TextMeshProUGUI>().enabled = false;
            CourageInf2.GetComponent<TextMeshProUGUI>().enabled = false;
            TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
    IEnumerator DialogueCineChien()
    {
        yield return new WaitForSeconds(8f);
        Dialogue5.CineEvasion = false;
        PanelCine.GetComponent<Image>().enabled = false;
        PNJChief.GetComponent<TextMeshProUGUI>().enabled = false;
    }
    IEnumerator TempsAvantCombat()
    {
        yield return new WaitForSeconds(5f);
        ChoixHumain.GetComponent<TextMeshProUGUI>().enabled = false;
        Panel.GetComponent<Image>().enabled = false;
        AllyDog = false;
    }
    void Update()
    {
        if (AllyDog == true)
        {
            collider.radius = 4;
            collider.isTrigger = true;
        }
        if (AllyDog == false)
        {
            collider.radius = 1;
            collider.isTrigger = false;
        }
        float percentageHp = ((hpEnemy * 100) / hpMax) / 100; //barre de vie
        hpImage.fillAmount = percentageHp;

        if ((!isDead) && (AllyDog ==false))
        {

            // On cherche le joueur en permanence
            Target = GameObject.Find("Player").transform;

            // On calcule la distance entre le joueur et l'ennemi, en fonction de cette distance on effectue diverses actions
            Distance = Vector3.Distance(Target.position, transform.position);

            // On calcule la distance entre l'ennemi et sa position de base
            DistanceBase = Vector3.Distance(basePositions, transform.position);

            // Quand l'ennemi est loin = idle
            if (Distance > chaseRange && DistanceBase <= 15)
            {
                if (hpEnemy == hpMax)
                {
                    idle();
                }
            }

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
            if (Distance > 2*chaseRange && DistanceBase > 15)
            {
                backgroundHp.enabled = false;
                BackBase();
                hpEnemy = hpMax;
            }
            //quand le monstre se fait taper de loin
            if (Distance > chaseRange && Distance < 2 * chaseRange)
            {
                if (hpEnemy != hpMax)
                {
                    chase();
                }
            }
        }
        if (Dialogue5.CineEvasion == true)
        {
            PanelCine.GetComponent<Image>().enabled = true;
            PNJChief.GetComponent<TextMeshProUGUI>().enabled = true;
            StartCoroutine(DialogueCineChien());
        }
        if ((Conversation) && (Dialogue5.QuestChief == true))
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
                Dialogue5.Ally = false;
            }
            if (lastAnswer == Constructeur.NameCharacter + ": corps")
            {
                Menace.GetComponent<TextMeshProUGUI>().enabled = false;
                Justice.GetComponent<TextMeshProUGUI>().enabled = false;
                ChoixHumain.GetComponent<TextMeshProUGUI>().enabled = true;
                ChoixChien.GetComponent<TextMeshProUGUI>().enabled = false;
                Choix.GetComponent<TextMeshProUGUI>().enabled = false;
                StartCoroutine(TempsAvantCombat());
            }
        }
        if ((Conversation) && (Dialogue5.QuestChief == false))
        {
            lastAnswer = GameManager.PlayerAnswer;
            if (lastAnswer == Constructeur.NameCharacter + ": apprendre")
            {
                if (courage2 == 0)
                {
                    if (courage1 == 0)
                    {
                        Remerciement.GetComponent<TextMeshProUGUI>().enabled = false;
                        TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                        if (UI.CourageTotal >= 32)
                        {
                            PlayerInventory.currentArmor += 10;
                            CourageSup.GetComponent<TextMeshProUGUI>().enabled = true;
                            Debug.Log("l'armure est à " + PlayerInventory.currentArmor);
                            courage1 = 1;
                            Conversation = false;
                        }
                        else CourageInf.GetComponent<TextMeshProUGUI>().enabled = true;
                        Conversation = false;
                    }
                    else
                    {
                        Remerciement.GetComponent<TextMeshProUGUI>().enabled = false;
                        TextFin.GetComponent<TextMeshProUGUI>().enabled = false;
                        if (UI.CourageTotal >= 54)
                        {
                            PlayerInventory.currentArmor += 10;
                            CourageSup2.GetComponent<TextMeshProUGUI>().enabled = true;
                            Debug.Log("l'armure est à " + PlayerInventory.currentArmor);
                            courage2 = 1;
                            Conversation = false;
                        }
                        else CourageInf2.GetComponent<TextMeshProUGUI>().enabled = true;
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
    protected override void idle()
    {
        animations.Play("Idle_Battle");
    }
    protected override void attack()
    {
        agent.destination = transform.position;

        if (Time.time > attackTime)
        {
            animations.Play("Attack02");
            Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage);
            attackTime = Time.time + attackRepeatTime;
        }
    }
    protected override void Dead()
    {
        isDead = true;
        backgroundHp.enabled = false;
        collider.enabled = false;
        animations.Play("Die");
        Experience();
        if(Dialogue5.QuestChief == true)
        {
           Dialogue5.QuestChief = false;
        }
        if (AnimCam3 != null)
        {
            AnimCam3.SetBool("CamChien3Activate", true);
        }
        AnimDoorL.SetBool("OpenDoorL", true);
        AnimDoorR.SetBool("OpenDoorR", true);
        GameObject.Find("PNJ5Prison").transform.position = basePositions;
        Destroy(transform.gameObject);

    }
    public override void ApplyDammage(float TheDammage)
    {
        if (!isDead)
        {
            hpEnemy = hpEnemy - (TheDammage-3);
            animations.Play("Defend");
            if (hpEnemy <= 0)
            {
                Dead();
            }
        }
    }
}
