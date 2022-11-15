using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpiderQueen : EnemyAi
{
    public static int SpiderQueenQuest;
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
    protected override void CreateEnnemy()
    {
        GameObject clone = Instantiate(PrefabToSpawn, basePositions, Quaternion.identity);
        clone.GetComponent<EnemyAi>().PrefabToSpawn = PrefabToSpawn;
    }
    protected override void chase()
    {
        animations.Play("Walk");
        agent.destination = Target.position;
    }

    // Combat
    protected override void attack()
    {
        // empeche l'ennemi de traverser le joueur
        agent.destination = transform.position;

        //Si pas de cooldown
        if (Time.time > attackTime)
        {
            animations.Play("Attack_Left");
            Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage);
            attackTime = Time.time + attackRepeatTime;
        }
    }

    // idle
    protected override void idle()
    {
        animations.Play("Idle");
    }
    protected override void walk()
    {
        animations.Play("Walk");
    }
    protected override void BackBase()
    {
        animations.Play("Walk");
        agent.destination = basePositions;
    }
    public override void ApplyDammage(float TheDammage)
    {
        if (!isDead)
        {
            hpEnemy = hpEnemy - TheDammage;
            animations.Play("GetHit");
            if (hpEnemy <= 0)
            {
                Dead();
                if (Dialogue4.QuestSpiderQueenIsUp)
                {
                    SpiderQueenQuest += 1;
                }
            }
        }
    }
    protected override void Dead()
    {
        isDead = true;
        {
            StartCoroutine(Respawn());
        }
        backgroundHp.enabled = false;
        collider.enabled = false;
        animations.Play("Die");
        Experience();
        Destroy(transform.gameObject, 61);
    }
}
