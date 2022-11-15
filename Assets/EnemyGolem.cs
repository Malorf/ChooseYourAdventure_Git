using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyGolem : EnemyAi
{
    public static bool GolemDead = false;
    public static int golem = 0;
    private bool CineDone = false;
    private bool jumping = false;
    private bool flying = false;
    private bool landing = false;
    private bool hptrigger = false;
    AudioSource audios;
    public AudioClip onGroundHit;
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
        animations.Play("Sleep");
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
            if (CineGolem.fall == true && CineDone == false)
            {
                StartCoroutine(SleepEnd());
            }
            // Quand l'ennemi est loin = idle
            if (CineDone == true)
            {
                if (hpEnemy == hpMax && Distance > chaseRange)
                {
                    idle();
                }
            }
            if (hpEnemy <= hpMax/2 && hptrigger == false)
            {
                ability();
            }
            if (hpEnemy > hpMax/2 && hpEnemy <= hpMax)
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
                if (Distance > 2 * chaseRange && DistanceBase > 15)
                {
                    jumping = false;
                    flying = false;
                    landing = false;
                    hptrigger = false;
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
            if (hpEnemy <= hpMax / 2 && hptrigger == true)
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
                if (Distance > 2 * chaseRange && DistanceBase > 15)
                {
                    jumping = false;
                    flying = false;
                    landing = false;
                    hptrigger = false;
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
    }
    IEnumerator SleepEnd()
    {
        yield return new WaitForSeconds(3f);
        animations.Play("Rage");
        StartCoroutine(CineFin());
    }
    IEnumerator CineFin()
    {
        yield return new WaitForSeconds(2f);
        CineDone = true;
    }
    protected override void chase()
    {
        animations.Play("Walk");
        agent.destination = Target.position;
    }
    protected override void idle()
    {
        animations.Play("Idle");
    }
    protected override void attack()
    {
        // empeche l'ennemi de traverser le joueur
        agent.destination = transform.position;


        if (Time.time > attackTime)
        {
            if (hptrigger == false)
            {
                animations.Play("Hit2");
                Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage);
                attackTime = Time.time + attackRepeatTime;
            }
            if (hptrigger == true)
            {
                animations.Play("Hit");
                Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage+2);
                attackTime = Time.time + attackRepeatTime;
            }
        }
    }
    void ability()
    {
        if (jumping == false)
        {
            StartCoroutine(Jump());
            jumping = true;
        }
    }
    IEnumerator Jump()
    {
        yield return new WaitForSeconds(1f);
        agent.transform.position += Vector3.up * 3;
        animations.Play("Jump");
        if (flying == false)
        {
            StartCoroutine(Fly());
            flying = true;
        }
    }
    IEnumerator Fly()
    {
        yield return new WaitForSeconds(3f);
        agent.destination = Target.position;
        animations.Play("Fly");
        if (landing == false)
        {
            StartCoroutine(Land());
            landing = true;
        }
    }
    IEnumerator Land()
    {
        yield return new WaitForSeconds(4f);
        agent.destination = Target.position;
        agent.transform.position -= Vector3.up * 3;
        audios.clip = onGroundHit;
        audios.Play();
        if (Distance <= attackRange)
        {
            Target.transform.position += Vector3.up;
            Target.GetComponent<PlayerInventory>().ApplyDamage(40);
        }
        animations.Play("Land");
        hptrigger = true;
    }
    public override void ApplyDammage(float TheDammage)
    {
        if (!isDead)
        {
            animations.Play("Damage");
            hpEnemy = hpEnemy - TheDammage;
            if (hpEnemy <= 0)
            {
                Dead();
            }
        }
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
            golem = 1;
            GolemDead = true;
        }
        GolemDoor.golemDoor = true;
        animations.Play("Die");
        Experience();
        Destroy(transform.gameObject, 61);
    }
}

