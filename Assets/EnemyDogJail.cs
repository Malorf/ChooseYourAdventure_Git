using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDogJail : EnemyKnightDog
{
    public static bool JailQuest = false;
    public static int Jailer = 0;
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        attackTime = Time.time;
        waypointIndex = 0;
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
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(120f);
        CreateEnnemy();
    }
    protected override void CreateEnnemy()
    {
        GameObject clone = Instantiate(PrefabToSpawn, basePositions, Quaternion.identity);
        clone.GetComponent<EnemyAi>().PrefabToSpawn = PrefabToSpawn;
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
        {
            StartCoroutine(Respawn());
        }
        backgroundHp.enabled = false;
        collider.enabled = false;
        animations.Play("Die");
        Experience();
        Destroy(transform.gameObject, 121);

    }
    public override void ApplyDammage(float TheDammage)
    {
        if (!isDead)
        {
            hpEnemy = hpEnemy - TheDammage;
            animations.Play("GetHit");
            if (hpEnemy <= 0)
            {
                if (Dialogue5.QuestGeôlier == true)
                {
                    Jailer = 1;
                    JailQuest = true;
                }
                Dead();
            }
        }
    }
}
