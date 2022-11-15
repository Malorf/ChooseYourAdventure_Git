using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterMotor : MonoBehaviour
{
    // Animations du perso
    Animation animations;

    // Scripts playerinventory
    PlayerInventory playerInv;

    //Vitesse de déplacement du perso
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    // Variables concernant l'attaque
    public static float attackCooldown = 1.5f;
    private bool isAttacking;
    private double currentCooldown;
    public float attackRange;
    public GameObject rayHit;
    public GameObject raySpell;

    AudioSource audios;
    public AudioClip attackPlayer;

    //sorts
    //eclair
    public float eclairSpellCost;
    public GameObject eclairSpellGameObj;
    public float eclairSpeed;
    public GameObject eclairSpellActivation; //spell disponible et activé
    public static bool eclairIsUp = false;
    public static float eclair = 0;
    //maladie
    public float maladieSpellCost;
    public GameObject maladieSpellGameObj;
    public float maladieSpeed;
    public GameObject maladieSpellActivation; //spell disponible et activé
    public static bool maladieIsUp = false;
    public static float maladie = 0;
    //buff hp
    public float BuffHPCost;
    public GameObject BuffHPGameObj;
    public GameObject BuffHPActivation; //spell disponible et activé
    public static bool BuffHPIsUp = false;
    public static float BuffHp = 0;
    //buff mana
    public float BuffManaCost;
    public GameObject BuffManaGameObj;
    public GameObject BuffManaActivation; //spell disponible et activé
    public static bool BuffManaIsUp = false;
    public static float BuffMana = 0;
    //buff des druides
    public float BuffDroodCost;
    public GameObject BuffDroodGameObj;
    public GameObject BuffDroodActivation; //spell disponible et activé
    public static bool BuffDroodIsUp = false;
    public static float BuffDrood = 0;
    //Inputs
    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;

    private GameObject Background;
    public Vector3 jumpSpeed; //vitesse saut

    public GameObject player; // le GO du joueur

    public static bool isDead = false;

    CapsuleCollider playerCollider; //Collision
    void Start()
    {
        audios = GetComponent<AudioSource>();
        animations = gameObject.GetComponent<Animation>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
        playerInv = gameObject.GetComponent<PlayerInventory>();
        rayHit = GameObject.Find("RayHit");
        if (PlayerPrefs.GetInt("load") == 1) // Si on reprend la partie
        {
            float x = PlayerPrefs.GetFloat("x");
            float y = PlayerPrefs.GetFloat("y");
            float z = PlayerPrefs.GetFloat("z");
            PlayerPrefs.GetFloat("force", UI.ForceTotal);
            PlayerPrefs.GetFloat("Endurance", UI.EnduranceTotal);
            PlayerPrefs.GetFloat("intelligence", UI.IntelligenceTotal);
            PlayerPrefs.GetFloat("courage", UI.CourageTotal);
            PlayerPrefs.GetFloat("sagesse", UI.SagesseTotal);
            PlayerPrefs.GetFloat("dextérité", UI.DexteriteTotal);
            PlayerPrefs.GetFloat("pointsDispo", UI.PointsValue);
            PlayerPrefs.GetFloat("force", UI.ForceTotal);
            PlayerPrefs.GetFloat("Lvl", PlayerInventory.playerLevel);
            PlayerPrefs.GetFloat("XP", PlayerInventory.currentXp);
            PlayerPrefs.GetFloat("maxXp", PlayerInventory.maxXp);
            PlayerPrefs.GetFloat("Vie", PlayerInventory.maxHealth);
            PlayerPrefs.GetFloat("Mana", PlayerInventory.maxMana);
            PlayerPrefs.GetFloat("Dégats", PlayerInventory.currentDamage);
            PlayerPrefs.GetFloat("Armure", PlayerInventory.currentArmor);
            PlayerPrefs.GetFloat("Cooldown", attackCooldown);
            PlayerPrefs.GetFloat("EclairSpeel", eclair);
            PlayerPrefs.GetFloat("MaladieSpeel", maladie);
            PlayerPrefs.GetFloat("BuffHp", BuffHp);
            PlayerPrefs.GetFloat("BuffMana", BuffMana);
            PlayerPrefs.GetFloat("BuffDrood", BuffDrood);
            PlayerPrefs.GetFloat("Force1", Dialogue1.force1);
            PlayerPrefs.GetFloat("Endurance1", Dialogue2.endurance1);
            PlayerPrefs.GetFloat("Dex1", Dialogue5.dex1);
            PlayerPrefs.GetFloat("Dex2", Dialogue5.dex2);
            PlayerPrefs.GetFloat("Courage1", EnemyDogChief.courage1);
            PlayerPrefs.GetFloat("Courage2", EnemyDogChief.courage2);
            PlayerPrefs.GetFloat("Intelligence1", Dialogue6.intelligence1);
            PlayerPrefs.GetFloat("Sagesse1", Dialogue6.sagesse1);
            PlayerPrefs.GetFloat("Endurance2", DialogueBlackSmith.endurance2);
            PlayerPrefs.GetFloat("Endurance3", DialogueBlackSmith.endurance3);
            PlayerPrefs.GetFloat("Dex3", DialogueChampion.dexterite3);
            PlayerPrefs.GetFloat("Dex4", DialogueChampion.dexterite4);
            PlayerPrefs.GetFloat("Intelligence2", DialogueLadyRuid.intelligence2);
            PlayerPrefs.GetFloat("Intelligence3", DialogueLadyRuid.intelligence3);
            PlayerPrefs.GetFloat("Courage3", DialogueMayor.courage3);
            PlayerPrefs.GetFloat("Courage4", DialogueMayor.courage4);
            PlayerPrefs.GetFloat("Force2", DialogueTavernier.force2);
            PlayerPrefs.GetFloat("Force3", DialogueTavernier.force3);
            PlayerPrefs.GetFloat("WolfQuest", Dialogue2.XpQuêteLoup);
            PlayerPrefs.GetFloat("SpiderQuest", Dialogue4.XpQuêteSpider);
            PlayerPrefs.GetFloat("SpiderQueen", Dialogue4.XpQuêteSpiderQueen);
            PlayerPrefs.GetFloat("Jail", Dialogue4.XpQuêteJail);
            PlayerPrefs.GetFloat("Herbes", DialogueMerlinramix.XpQuêteHerbes);
            PlayerPrefs.GetFloat("Chancres", DialogueMerlinramix.XpQuêtePotion);
            PlayerPrefs.GetFloat("Armures", DialogueBlackSmith.XpQuêteChienvalier);
            PlayerPrefs.GetFloat("Maire", DialogueMayor.XpQuêteMayor);
            PlayerPrefs.GetFloat("Champion", DialogueDuchelvau.XpQuêteChampion);
            PlayerPrefs.GetFloat("Gobelins", DialogueTavernier.XpQuêteGobelin);
            PlayerPrefs.GetFloat("Lieutenant", DialogueTavernier.XpQuêteLieutenant);

            player.transform.position = new Vector3(x, y, z);
        }
    }
    bool IsGrounded()
        {
            Vector3 dwn = transform.TransformDirection(Vector3.down);

            return (Physics.Raycast(transform.position, dwn, 0.5f));
        }  

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKey(inputBack))// si on recule
            {
                transform.Translate(0, 0, -(walkSpeed) * Time.deltaTime);
                animations.Play("walk");
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    attack();
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    attackEclairSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    attackMaladieSpell();
                }
            }
            if (Input.GetKey(inputLeft))// si on tourne à gauche
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);

            }
            if (Input.GetKey(inputRight))// si on tourne à droite
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);

            }
            if (Input.GetKeyDown(KeyCode.Escape)) //si on veut retourner au menu
            {
                save();
                SceneManager.LoadScene(0);
            }
            if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift)) //si on avance
            {
                transform.Translate(0, 0, walkSpeed * Time.deltaTime);
                if (!isAttacking)
                {
                    animations.Play("walk");
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    attack();
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    attackEclairSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    attackMaladieSpell();
                }
            }
            if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))// si on veut sprint
            {
                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                if (!isAttacking)
                {
                    animations.Play("run");
                }
            }
            if (!Input.GetKey(inputFront) && !Input.GetKey(inputBack))// si on n'avance ni ne recule
            {
                if (!isAttacking)
                {
                    animations.Play("idle");
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    attack();
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    attackEclairSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    BuffHpSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    BuffManaSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    attackMaladieSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    BuffDroodSpell();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) // si on saute
            {
                // Préparation du saut
                Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                v.y = jumpSpeed.y;

                //Saut
                gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
            }
            if (isAttacking)
            {
                currentCooldown -= Time.deltaTime;
            }
            if (currentCooldown <= 0)
            {
                currentCooldown = attackCooldown;
                isAttacking = false;
            }
            if (eclair == 1)
            {
                eclairIsUp = true;
                if (eclairIsUp == true)
                {
                    eclairSpellActivation.SetActive(true);
                }
            }
            if (BuffHp == 1)
            {
                BuffHPIsUp = true;
                if (BuffHPIsUp == true)
                {
                    BuffHPActivation.SetActive(true);
                }
            }
            if (BuffMana ==1)
            {
                BuffManaIsUp = true;
                if (BuffManaIsUp == true)
                {
                    BuffManaActivation.SetActive(true);
                }
            }
            if (BuffDrood ==1)
            {
                BuffDroodIsUp = true;
                if (BuffDroodIsUp == true)
                {
                    BuffDroodActivation.SetActive(true);
                }
            }
            if (maladie ==1)
            {
                maladieIsUp = true;
                if (maladieIsUp == true)
                {
                    maladieSpellActivation.SetActive(true);
                }
            }
        }

    }
    //fonction d'attaque
    public void attack()
    {
        if (!isAttacking)
        {
            animations.Play("attack");
            audios.clip = attackPlayer;
            audios.Play();

            RaycastHit hit;

            if (Physics.Raycast(rayHit.transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
            {
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<EnemyAi>().ApplyDammage(PlayerInventory.currentDamage);
                }
                if (hit.transform.tag == "EnemyWolf")
                {
                    hit.transform.GetComponent<EnemyAiWolf>().ApplyDammage(PlayerInventory.currentDamage);
                }
                if (hit.transform.tag == "EnemySpider")
                {
                    hit.transform.GetComponent<EnemySpider>().ApplyDammage(PlayerInventory.currentDamage);
                }
            }
            isAttacking = true;
        }
    }
    //fonction d'attaque sorts
    public void attackEclairSpell()
    {
        if (!isAttacking && playerInv.currentMana >= eclairSpellCost) //Sort Eclair
        {
            if (eclairIsUp)
            {
                animations.Play("resist");
                GameObject theSpell = Instantiate(eclairSpellGameObj, raySpell.transform.position, transform.rotation);
                theSpell.GetComponent<Rigidbody>().AddForce(transform.forward * eclairSpeed);
                playerInv.currentMana -= eclairSpellCost;
                isAttacking = true;
            }
        }
    }
    public void BuffHpSpell()
    {
        if (!isAttacking && playerInv.currentMana >= BuffHPCost) 
        {
            if (BuffHPIsUp)
            {
                BuffHPIsUp = false;
                animations.Play("victory");
                PlayerInventory.maxHealth = PlayerInventory.maxHealth * 1.3f;
                StartCoroutine(BuffHpEnd());
                Instantiate(BuffHPGameObj, raySpell.transform.position, transform.rotation);
                playerInv.currentMana -= BuffHPCost;
            }
        }
    }
    public void BuffManaSpell()
    {
        if (!isAttacking && playerInv.currentMana >= BuffManaCost)
        {
            if (BuffManaIsUp)
            {
                BuffManaIsUp = false;
                animations.Play("victory");
                PlayerInventory.maxMana = PlayerInventory.maxMana * 1.3f;
                StartCoroutine(BuffManaEnd());
                Instantiate(BuffManaGameObj, raySpell.transform.position, transform.rotation);
                playerInv.currentMana -= BuffManaCost;
            }
        }
    }
    public void BuffDroodSpell()
    {
        if (!isAttacking && playerInv.currentMana >= BuffDroodCost)
        {
            if (BuffDroodIsUp)
            {
                BuffDroodIsUp = false;
                animations.Play("victory");
                PlayerInventory.currentDamage += 10;
                PlayerInventory.currentArmor += 10;
                StartCoroutine(BuffDroodEnd());
                Instantiate(BuffDroodGameObj, raySpell.transform.position, transform.rotation);
                playerInv.currentMana -= BuffDroodCost;
            }
        }
    }
    public void attackMaladieSpell()
    {
        if (!isAttacking && playerInv.currentMana >= maladieSpellCost) 
        {
            if (maladieIsUp)
            {
                maladieIsUp = false;
                animations.Play("resist");
                GameObject theSpell = Instantiate(maladieSpellGameObj, raySpell.transform.position, transform.rotation);
                theSpell.GetComponent<Rigidbody>().AddForce(transform.forward * maladieSpeed);
                playerInv.currentMana -= maladieSpellCost;
                StartCoroutine(MaladieCDEnd());
                isAttacking = true;
            }
        }
    }
    IEnumerator BuffHpEnd()
    {
        yield return new WaitForSeconds(300f);
        BuffHPIsUp = true;
        PlayerInventory.maxHealth = PlayerInventory.maxHealth / 1.3f;
    }
    IEnumerator BuffManaEnd()
    {
        yield return new WaitForSeconds(300f);
        BuffManaIsUp = true;
        PlayerInventory.maxMana = PlayerInventory.maxMana / 1.3f;
    }
    IEnumerator BuffDroodEnd()
    {
        yield return new WaitForSeconds(300f);
        BuffDroodIsUp = true;
        PlayerInventory.currentDamage -= 10;
        PlayerInventory.currentArmor -= 10;
    }
    IEnumerator MaladieCDEnd()
    {
        yield return new WaitForSeconds(12f);
        maladieIsUp = true;
    }
    //sauvegarde
    public void save()
    {
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("x", playerCollider.transform.position.x);
        PlayerPrefs.SetFloat("y", playerCollider.transform.position.y);
        PlayerPrefs.SetFloat("z", playerCollider.transform.position.z);
        PlayerPrefs.SetFloat("force", UI.ForceTotal);
        PlayerPrefs.SetFloat("Endurance", UI.EnduranceTotal);
        PlayerPrefs.SetFloat("intelligence", UI.IntelligenceTotal);
        PlayerPrefs.SetFloat("courage", UI.CourageTotal);
        PlayerPrefs.SetFloat("sagesse", UI.SagesseTotal);
        PlayerPrefs.SetFloat("dextérité", UI.DexteriteTotal);
        PlayerPrefs.SetFloat("pointsDispo", UI.PointsValue);
        PlayerPrefs.SetFloat("force", UI.ForceTotal);
        PlayerPrefs.SetFloat("Lvl", PlayerInventory.playerLevel);
        PlayerPrefs.SetFloat("XP", PlayerInventory.currentXp);
        PlayerPrefs.SetFloat("maxXp", PlayerInventory.maxXp);
        PlayerPrefs.SetFloat("Vie", PlayerInventory.maxHealth);
        PlayerPrefs.SetFloat("Mana", PlayerInventory.maxMana);
        PlayerPrefs.SetFloat("Dégats", PlayerInventory.currentDamage);
        PlayerPrefs.SetFloat("Armure", PlayerInventory.currentArmor);
        PlayerPrefs.SetFloat("Cooldown", attackCooldown);
        PlayerPrefs.SetFloat("EclairSpeel", eclair);
        PlayerPrefs.SetFloat("MaladieSpeel", maladie);
        PlayerPrefs.SetFloat("BuffHp", BuffHp);
        PlayerPrefs.SetFloat("BuffMana", BuffMana);
        PlayerPrefs.SetFloat("BuffDrood", BuffDrood);
        PlayerPrefs.SetFloat("Force1", Dialogue1.force1);
        PlayerPrefs.SetFloat("Endurance1", Dialogue2.endurance1);
        PlayerPrefs.SetFloat("Dex1", Dialogue5.dex1);
        PlayerPrefs.SetFloat("Dex2", Dialogue5.dex2);
        PlayerPrefs.SetFloat("Courage1", EnemyDogChief.courage1);
        PlayerPrefs.SetFloat("Courage2", EnemyDogChief.courage2);
        PlayerPrefs.SetFloat("Intelligence1", Dialogue6.intelligence1);
        PlayerPrefs.SetFloat("Sagesse1", Dialogue6.sagesse1);
        PlayerPrefs.SetFloat("Endurance2", DialogueBlackSmith.endurance2);
        PlayerPrefs.SetFloat("Endurance3", DialogueBlackSmith.endurance3);
        PlayerPrefs.SetFloat("Dex3", DialogueChampion.dexterite3);
        PlayerPrefs.SetFloat("Dex4", DialogueChampion.dexterite4);
        PlayerPrefs.SetFloat("Intelligence2", DialogueLadyRuid.intelligence2);
        PlayerPrefs.SetFloat("Intelligence3", DialogueLadyRuid.intelligence3);
        PlayerPrefs.SetFloat("Courage3", DialogueMayor.courage3);
        PlayerPrefs.SetFloat("Courage4", DialogueMayor.courage4);
        PlayerPrefs.SetFloat("Force2", DialogueTavernier.force2);
        PlayerPrefs.SetFloat("Force3", DialogueTavernier.force3);
        PlayerPrefs.SetFloat("WolfQuest", Dialogue2.XpQuêteLoup);
        PlayerPrefs.SetFloat("SpiderQuest", Dialogue4.XpQuêteSpider);
        PlayerPrefs.SetFloat("SpiderQueen", Dialogue4.XpQuêteSpiderQueen);
        PlayerPrefs.SetFloat("Jail", Dialogue4.XpQuêteJail);
        PlayerPrefs.SetFloat("Herbes", DialogueMerlinramix.XpQuêteHerbes);
        PlayerPrefs.SetFloat("Chancres", DialogueMerlinramix.XpQuêtePotion);
        PlayerPrefs.SetFloat("Armures", DialogueBlackSmith.XpQuêteChienvalier);
        PlayerPrefs.SetFloat("Maire", DialogueMayor.XpQuêteMayor);
        PlayerPrefs.SetFloat("Champion", DialogueDuchelvau.XpQuêteChampion);
        PlayerPrefs.SetFloat("Gobelins", DialogueTavernier.XpQuêteGobelin);
        PlayerPrefs.SetFloat("Lieutenant", DialogueTavernier.XpQuêteLieutenant);
    }
}