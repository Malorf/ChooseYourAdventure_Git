using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreenSpider : EnemyAi
{
    protected Animation anim;
    public static int NumberGreenSpiders;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = gameObject.GetComponent<Animation>();
        collider = GetComponent<CapsuleCollider>();
        attackTime = Time.time;
        waypointIndex = 0;
        transform.LookAt(points[waypointIndex].position);
        basePositions = transform.position;
        hpImage.enabled = false;
        backgroundHp.enabled = false;
        hpEnemy = hpMax;
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
            if (Distance > chaseRange * 2)
            {
                if (hpEnemy != hpMax)
                {
                    backgroundHp.enabled = false;
                    hpImage.enabled = false;
                    BackBase();
                }
            }
            if (Distance > chaseRange)
            {
                if (hpEnemy == hpMax)
                {
                    transform.LookAt(points[waypointIndex].position);
                    dist = Vector3.Distance(transform.position, points[waypointIndex].position);
                    walk();
                    if (dist < 1f)
                    {
                        IncreaseIndex();
                    }
                    Patrol();
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
    protected override void chase()
    {
        agent.enabled = true;
        anim.Play("Run");
        agent.destination = Target.position;
    }
    protected override void idle()
    {
        anim.Play("Idle");
    }
    protected override void attack()
    {
        // empeche l'ennemi de traverser le joueur
        agent.destination = transform.position;

        //Si pas de cooldown
        if (Time.time > attackTime)
        {
            anim.Play("Attack");
            Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage);
            attackTime = Time.time + attackRepeatTime;
        }
    }
    protected override void walk()
    {
        anim.Play("Walk");
    }
    protected override void BackBase()
    {
        anim.Play("Walk");
        agent.destination = basePositions;
        StartCoroutine(TimeBeforeBase());
    }
    protected override void Dead()
    {
        isDead = true;
        {
            StartCoroutine(Respawn());
        }
        backgroundHp.enabled = false;
        collider.enabled = false;
        anim.Play("Death");
        Experience();
        if (DialogueMerlinramix.QuestPotion == true)
        {
            NumberGreenSpiders += 1;
        }
        Destroy(transform.gameObject, 61);
    }
}
