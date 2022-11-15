using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrc : EnemyAi
{
    public static bool OrcDead = false;
    public static int orc = 0;
    AudioSource audios;
    public AudioClip gethit;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        attackTime = Time.time;
        basePositions = transform.position;
        hpImage.enabled = false;
        backgroundHp.enabled = false;
        hpEnemy = hpMax;
        audios = GetComponent<AudioSource>();
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
            if (Distance > 2 * chaseRange && DistanceBase > 15)
            {
                backgroundHp.enabled = false;
                hpImage.enabled = false;
                hpEnemy = hpMax;
                BackBase();
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
    }
    protected override void chase()
    {
        animations.Play("Monster_anim|Run");
        agent.destination = Target.position;
    }
    protected override void idle()
    {
        animations.Play("Monster_anim|Idle_1");
    }
    protected override void attack()
    {
        // empeche l'ennemi de traverser le joueur
        agent.destination = transform.position;

        if (hpEnemy >= hpMax/3)
        {
            if (Time.time > attackTime)
            {
                animations.Play("Monster_anim|Atack");
                Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage);
                attackTime = Time.time + attackRepeatTime;
            }
        }
        if (hpEnemy < hpMax / 3 && hpEnemy >= hpMax/8)
        {
            if (Time.time > attackTime)
            {
                animations.Play("Monster_anim|Atack_3");
                Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage+5);
                attackTime = Time.time + attackRepeatTime;
            }
        }
        if (hpEnemy < hpMax / 8)
        {
            animations.Play("Monster_anim|Block");
        }
    }
    public override void ApplyDammage(float TheDammage)
    {
        if (!isDead)
        {
            if (hpEnemy >= hpMax / 8 && hpEnemy <= hpMax)
            {
                hpEnemy = hpEnemy - TheDammage;
                animations.Play("Monster_anim|Get_hit");
                audios.clip = gethit;
                audios.Play();
            }
            if (hpEnemy < hpMax / 8)
            {
                hpEnemy = hpEnemy - (TheDammage-10);
            }
                if (hpEnemy <= 0)
            {
                Dead();
            }
        }
    }
    protected override void walk()
    {
        animations.Play("Monster_anim|Walk");
    }
    protected override void BackBase()
    {
        animations.Play("Monster_anim|Walk");
        agent.destination = basePositions;
    }
    protected override void Dead()
    {
        isDead = true;
        {
            StartCoroutine(Respawn());
        }
        backgroundHp.enabled = false;
        collider.enabled = false;
        if (DialogueTavernier.QuestLieutenant == true)
        {
            orc = 1;
            OrcDead = true;
        }
        animations.Play("Monster_anim|Death");
        Experience();
        Destroy(transform.gameObject, 61);
    }
}
