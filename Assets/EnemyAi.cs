using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAi : MonoBehaviour
{
    //Distance entre le joueur et l'ennemi
    protected float Distance;

    //Distance entre l'ennemi et sa position de base
    protected float DistanceBase;
    protected Vector3 basePositions;

    // Cible de l'ennemi
    public Transform Target;

    //Distance de poursuite
    public float chaseRange;

    // Portée des attaques
    public float attackRange;

    // Cooldown des attaques
    public float attackRepeatTime = 1;
    protected float attackTime;

    // Montant des dégâts infligés
    public float TheDammage;

    // Agent de navigation
    protected UnityEngine.AI.NavMeshAgent agent;

    // Barre de vie du monstre
    public Image hpImage;
    public Image backgroundHp;

    // fonction pour la patrol
    public Transform[] points;
    protected float dist;
    public float speed;
    protected int waypointIndex;

    //Respawn
    public GameObject PrefabToSpawn;

    // Animations de l'ennemi
    protected Animator animations;
    protected CapsuleCollider collider;

    // Vie de l'ennemi
    protected float hpEnemy;
    public float hpMax;
    public bool isDead = false;

    public int xp;



    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animator>();
        attackTime = Time.time;
        waypointIndex = 0;
        collider = GetComponent<CapsuleCollider>();
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
    protected virtual IEnumerator Respawn()
    {
        yield return new WaitForSeconds(60f);
        CreateEnnemy();
    }
    protected virtual IEnumerator TimeBeforeBase()
    {
        yield return new WaitForSeconds(5f);
        hpEnemy = hpMax;
        agent.enabled = false;
    }

    protected virtual void CreateEnnemy()
    {
        GameObject clone = Instantiate(PrefabToSpawn, transform.position, Quaternion.identity);
        clone.GetComponent<EnemyAi>().PrefabToSpawn = PrefabToSpawn;
        clone.GetComponent<EnemyAi>().points = points;
    }
    // patrouille
    protected virtual void Patrol()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
    protected virtual void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= points.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(points[waypointIndex].position);
    }
    // poursuite
    protected virtual void chase()
    {
        agent.enabled = true;
        animations.Play("run");
        agent.destination = Target.position;
    }

    protected virtual void idle()
    {
        animations.Play("idle");
    }
    // Combat
    protected virtual void attack()
    {
        // empeche l'ennemi de traverser le joueur
        agent.destination = transform.position;

        //Si pas de cooldown
        if (Time.time > attackTime)
        {
            animations.Play("attack1");
            Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage);
            attackTime = Time.time + attackRepeatTime;
        }
    }

    // walk
    protected virtual void walk()
    {
        animations.Play("walk");
    }

    public virtual void ApplyDammage(float TheDammage)
    {
        if (!isDead)
        {
            hpEnemy = hpEnemy - TheDammage;
            if (hpEnemy <= 0)
            {
                Dead();    
            }
        }
    }
    public virtual void ApplyDOTDammage(float TheDammage)
    {
        float DotDammage = TheDammage / 4;
        float DotDammageOver = TheDammage / 4;
        if (!isDead)
        {    
            StartCoroutine(DotTime());
            if (hpEnemy <= 0)
            {
                Dead();
            }
        }
        IEnumerator DotTime()
        {
            while (DotDammageOver <= TheDammage)
            {
                hpEnemy = hpEnemy - DotDammage;
                DotDammageOver += TheDammage / 4;
                yield return new WaitForSeconds(3f);
            }

        }
    }
    protected virtual void BackBase()
    {
        animations.Play("walk");
        agent.destination = basePositions;
        StartCoroutine(TimeBeforeBase());
    }
    protected virtual void Dead()
    {
        isDead = true;
        {
            StartCoroutine(Respawn());
        }
        backgroundHp.enabled = false;
        collider.enabled = false;
        animations.Play("die");
        Experience();
        Destroy(transform.gameObject, 61);
    }
    protected virtual void Experience()
    {
        PlayerInventory.currentXp += xp;
    }
}