using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuardPatrol : EnemyGuard
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
        transform.LookAt(points[waypointIndex].position);
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
            else
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
    }
}

