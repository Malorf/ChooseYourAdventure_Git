using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : EnemyAi
{
    // Start is called before the first frame update
    void Start()
    {
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
            if (hpEnemy != hpMax)
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
                if (Distance > chaseRange && Distance < 2 * chaseRange)
                {
                    if (hpEnemy != hpMax)
                    {
                        chase();
                    }
                }
            }
            else idle();  
        }
    }
    protected override void idle()
    {
        animations.Play("Male Idle");
    }
    protected override void chase()
    {
        animations.Play("Male_Sword_Walk");
        agent.destination = Target.position;
    }
    protected override void walk()
    {
        animations.Play("Male_Sword_Walk");
    }
    protected override void BackBase()
    {
        animations.Play("Male_Sword_Walk");
        agent.destination = basePositions;
    }
    protected override void attack()
    {
        // empeche l'ennemi de traverser le joueur
        agent.destination = transform.position;

        //Si pas de cooldown
        if (Time.time > attackTime)
        {
            animations.Play("Male Attack 1");
            Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage);
            attackTime = Time.time + attackRepeatTime;
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
        animations.Play("Male Die");
        Experience();
        Destroy(transform.gameObject, 61);
    }
}

