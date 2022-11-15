using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpider : EnemyAi
{
    public static int SpiderQuest;
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        attackTime = Time.time;
        waypointIndex = 0;
        transform.LookAt(points[waypointIndex].position);
        basePositions = transform.position;
        hpImage.enabled = false;
        backgroundHp.enabled = false;
        hpEnemy = hpMax;
    }



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
        animations.Play("walk");
        agent.destination = Target.position;
    }
    protected override void attack()
    {
        // empeche l'ennemi de traverser le joueur
        agent.destination = transform.position;

        //Si pas de cooldown
        if (Time.time > attackTime)
        {
            animations.Play("attack");
            Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage);
            attackTime = Time.time + attackRepeatTime;
        }
    }
    public override void ApplyDammage(float TheDammage)
    {
        if (!isDead)
        {
            hpEnemy = hpEnemy - TheDammage;

            if (hpEnemy <= 0)
            {
                Dead();
                if (Dialogue4.QuestSpiderIsUp)
                {
                    SpiderQuest += 1;
                }
            }
        }
    }
}